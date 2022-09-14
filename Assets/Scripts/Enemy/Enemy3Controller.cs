using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : EnemyBase
{
    [Header("Move")]
    [SerializeField, Tooltip("プレイヤーを追尾する速度")] float _followSpeed;
    [SerializeField, Tooltip("追尾が止まる距離")] float _stopDis;
    [SerializeField, Tooltip("回転の半径")] float _amplitude = 1f;
    [SerializeField, Tooltip("X軸の回転の速度")] float _speedX = 3f;
    [SerializeField, Tooltip("追尾をするかどうか")] bool _isFollow;

    [Header("Shoot")]
    [SerializeField, Tooltip("弾のプレハブ")] GameObject _bullet;
    [SerializeField, Tooltip("射撃を行う間隔")] float _shootInterval;

    float _timer;
    float _moveTimer;

    Vector3 _startPos;
    Vector3 _defaultScale;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _startPos = transform.position;
        _defaultScale = transform.localScale;
    }


    void Update()
    {
        Warning();
    }


    public override void Move()
    {
        Enemy3Flip();

        if (_isFollow)
        {
            //目標の地点に到達するまで
            if (Vector2.Distance(transform.position, _player.transform.position) > _stopDis)  
            {
                Vector2 dir = _player.transform.position - transform.position;  //移動の向き
                transform.Translate(dir * _followSpeed * Time.deltaTime);       //移動させる処理
            }
        }
        else
        {
            //上下に波打つように移動させる
            _moveTimer += Time.deltaTime;
            float posY = Mathf.Sin(_moveTimer * _speedX) * _amplitude;
            transform.position = _startPos + new Vector3(0, posY); 
        }
    }


    public override void Attack()
    {
        if (_isWarning)
        {
            _timer += Time.deltaTime;

            if (_timer > _shootInterval)
            {
                Instantiate(_bullet, transform.position, transform.rotation); //一定間隔で弾を生成
                _timer = 0;
            }
        }
    }


    void Enemy3Flip()
    {
        if (_player.transform.position.x > transform.position.x)
        {
            transform.localScale = _defaultScale;
        }
        else
        {
            transform.localScale = new Vector3(_defaultScale.x * -1, transform.localScale.y);
        }
    }


    void Warning()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _warningDis)
        {
            _isWarning = true;
            _isFollow = true;
        }
    }

    public override void Damage()
    {
        _rb.velocity = Vector2.zero;
    }
}
