using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField, Tooltip("重力の大きさ")] float _gravityLevel = 20;
    
    [Header("Shoot")]
    [SerializeField, Tooltip("弾のプレハブ")] GameObject _bullet;
    [SerializeField, Tooltip("銃口の位置")] Transform _muzzle;
    [SerializeField, Tooltip("射撃を行う間隔")] float _shootInterval;

    float _timer;

    SpriteRenderer _targetRenderer;
    Rigidbody2D _rb;


    void Start()
    {
        _targetRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }


    void FixedUpdate()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);

        if (_muzzle && _targetRenderer.isVisible)
        {
            Enemy2Shoot();
        }
    }


    /// <summary>敵２の射撃の処理</summary>
    void Enemy2Shoot()
    {
        _timer += Time.deltaTime;

        if (_timer > _shootInterval)
        {
            Instantiate(_bullet, _muzzle);　//一定間隔で弾を生成
            _timer = 0;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;　//接地をしたらその場から動かないようにする
        }
    }
}
