using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : EnemyBase
{
    [Header("Move")]
    [SerializeField, Tooltip("ƒvƒŒƒCƒ„[‚ð’Ç”ö‚·‚é‘¬“x")] float _followSpeed;
    [SerializeField, Tooltip("’Ç”ö‚ªŽ~‚Ü‚é‹——£")] float _stopDis;
    [SerializeField, Tooltip("‰ñ“]‚Ì”¼Œa")] float _amplitude = 1f;
    [SerializeField, Tooltip("XŽ²‚Ì‰ñ“]‚Ì‘¬“x")] float _speedX = 3f;
    [SerializeField, Tooltip("’Ç”ö‚ð‚·‚é‚©‚Ç‚¤‚©")] bool _isFollow;

    [Header("Shoot")]
    [SerializeField, Tooltip("’e‚ÌƒvƒŒƒnƒu")] GameObject _bullet;
    [SerializeField, Tooltip("ŽËŒ‚‚ðs‚¤ŠÔŠu")] float _shootInterval;

    float _timer;
    float _moveTimer;

    Vector3 _startPos;
    Vector3 _defaultScale;


    void Start()
    {
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
            if (Vector2.Distance(transform.position, _player.transform.position) > _stopDis)  //–Ú•W‚Ì’n“_‚É“ž’B‚·‚é‚Ü‚Å
            {
                Vector2 dir = _player.transform.position - transform.position;  //ˆÚ“®‚ÌŒü‚«
                transform.Translate(dir * _followSpeed * Time.deltaTime);       //ˆÚ“®‚³‚¹‚éˆ—
            }
        }
        else
        {
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
                Instantiate(_bullet, transform.position, transform.rotation); //ˆê’èŠÔŠu‚Å’e‚ð¶¬
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
        }
    }
}
