using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : EnemyBase
{
    [Header("Gravity")]
    [SerializeField, Tooltip("�d�͂̑傫��")] float _gravityLevel = 20;
    
    [Header("Shoot")]
    [SerializeField, Tooltip("�e�̃v���n�u")] GameObject _bullet;
    [SerializeField, Tooltip("�e���̈ʒu")] Transform _muzzle;
    [SerializeField, Tooltip("�ˌ����s���Ԋu")] float _shootInterval;

    float _timer;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }


    public override void Move()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);
    }
        

    /// <summary>�G�Q�̎ˌ��̏���</summary>
    public override void Attack()
    {
        _timer += Time.deltaTime;

        if (_timer > _shootInterval)
        {
            Instantiate(_bullet, _muzzle.position, transform.rotation);�@//���Ԋu�Œe�𐶐�
            _timer = 0;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;�@//�ڒn�������炻�̏ꂩ�瓮���Ȃ��悤�ɂ���
        }
    }
}
