using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃を行うインターバル")] float _attackInterval = 10f;
    [SerializeField, Tooltip("与えるダメージ")] int _damage = 10;
    [Tooltip("攻撃をしているか")] bool _isAttack;

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
