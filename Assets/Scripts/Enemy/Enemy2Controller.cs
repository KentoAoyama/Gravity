using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : EnemyBase
{
    [Header("Gravity")]
    [SerializeField, Tooltip("重力の大きさ")] float _gravityLevel = 20;
    
    [Header("Shoot")]
    [SerializeField, Tooltip("弾のプレハブ")] GameObject _bullet;
    [SerializeField, Tooltip("銃口の位置")] Transform _muzzle;
    [SerializeField, Tooltip("射撃を行う間隔")] float _shootInterval;

    [Header("Warning")]
    [Tooltip("発見したかのフラグ")] bool _isWarning = false;
    [SerializeField, Tooltip("プレイヤーを発見する距離")] float _warningDis = 10f;


    float _timer;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.gravityScale = 0;
    }


    void Update()
    {
        Warning();
    }


    public override void Move()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);
    }
        

    /// <summary>敵２の射撃の処理</summary>
    public override void Attack()
    {
        if (_isWarning)           
        {
            _timer += Time.deltaTime;

            if (_timer > _shootInterval)
            {
                Instantiate(_bullet, _muzzle.position, transform.rotation); //一定間隔で弾を生成
                _timer = 0;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;　//接地をしたらその場から動かないようにする
        }
    }


    void Warning()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _warningDis && !_isWarning)
        {
            _isWarning = true;
        }
    }
}
