using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃を行うインターバル")] float _attackInterval = 10f;
    [SerializeField, Tooltip("与えるダメージ")] int _damage = 10;
    [Tooltip("攻撃をしているか")] bool _isAttack;
    [Tooltip("攻撃をしているか")] bool _isUpAttack;
    [Tooltip("何かしらのアクション中")] bool _isDamage;
    /// <summary>与えるダメージのプロパティ</summary>
    public int Damage => _damage;
    /// <summary>何かしらのアクション中であることを表すプロパティ</summary>
    public bool IsDamage { get => _isDamage; set => _isDamage = value;}

    float _timer;

    Animator _animator;

    GameObject _player;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player");
    }


    void FixedUpdate()
    {
        BossAttackMove();
    }


    /// <summary>ボスの攻撃全般を管理するメソッド</summary>
    void BossAttackMove()
    {
        if (!_isDamage)
        {
            _timer += Time.deltaTime;

            if (_timer > _attackInterval)
            {
                Attack();
                _timer = 0;
            }
            else
            {
                _isAttack = false;
                _isUpAttack = false;
            }

            _animator.SetBool("IsAttack", _isAttack);
            _animator.SetBool("IsUpAttack", _isUpAttack);
        }
        else
        {
            _timer = 0;
            _animator.Play("BossDamage");
        }
    }


    /// <summary>攻撃の処理</summary>
    void Attack()
    {
        if (transform.position.y >= _player.transform.position.y)
        {
            _isAttack = true;
        }
        else
        {
            _isUpAttack = true;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage))
        {
            addDamage.AddDamage(_damage);
        }
    }
}
