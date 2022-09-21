using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField, Tooltip("�U�����s���C���^�[�o��")] float _attackInterval = 10f;
    [SerializeField, Tooltip("�^����_���[�W")] int _damage = 10;
    [Tooltip("�U�������Ă��邩")] bool _isAttack;
    [Tooltip("�U�������Ă��邩")] bool _isUpAttack;
    [Tooltip("��������̃A�N�V������")] bool _isDamage;
    /// <summary>�^����_���[�W�̃v���p�e�B</summary>
    public int Damage => _damage;
    /// <summary>��������̃A�N�V�������ł��邱�Ƃ�\���v���p�e�B</summary>
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


    /// <summary>�{�X�̍U���S�ʂ��Ǘ����郁�\�b�h</summary>
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


    /// <summary>�U���̏���</summary>
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
