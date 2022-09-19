using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField, Tooltip("�U�����s���C���^�[�o��")] float _attackInterval = 10f;
    [SerializeField, Tooltip("�^����_���[�W")] int _damage = 10;
    [Tooltip("�U�������Ă��邩")] bool _isAttack;

    float _timer;

    Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        BossAttackMove();
    }


    void BossAttackMove()
    {
        _timer += Time.deltaTime;

        if (_timer > _attackInterval )
        {
            _isAttack = true;
            _timer = 0;
        }
        else
        {
            _isAttack = false;
        }

        _animator.SetBool("IsAttack", _isAttack);
    }
}
