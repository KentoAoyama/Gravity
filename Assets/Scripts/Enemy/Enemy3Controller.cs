using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : EnemyBase
{
    [Header("Move")]
    [SerializeField, Tooltip("�v���C���[��ǔ����鑬�x")] float _followSpeed;
    [SerializeField, Tooltip("�ǔ����~�܂鋗��")] float _stopDis;
    [SerializeField, Tooltip("��]�̔��a")] float _amplitude = 1f;
    [SerializeField, Tooltip("X���̉�]�̑��x")] float _speedX = 3f;
    [SerializeField, Tooltip("�ǔ������邩�ǂ���")] bool _isFollow;

    [Header("Shoot")]
    [SerializeField, Tooltip("�e�̃v���n�u")] GameObject _bullet;
    [SerializeField, Tooltip("�ˌ����s���Ԋu")] float _shootInterval;

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
            if (Vector2.Distance(transform.position, _player.transform.position) > _stopDis)  //�ڕW�̒n�_�ɓ��B����܂�
            {
                Vector2 dir = _player.transform.position - transform.position;  //�ړ��̌���
                transform.Translate(dir * _followSpeed * Time.deltaTime);       //�ړ������鏈��
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
                Instantiate(_bullet, transform.position, transform.rotation); //���Ԋu�Œe�𐶐�
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
