using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField, Tooltip("�U�����s���C���^�[�o��")] float _attackInterval = 5f;
    [SerializeField, Tooltip("�^����_���[�W")] int _damage = 10;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Transform _spawnPos;
    [Tooltip("�U�������Ă��邩")] bool _isAttack;
    [Tooltip("�U�������Ă��邩")] bool _isUpAttack;
    [Tooltip("��������̃A�N�V������")] bool _isDamage;
    /// <summary>�U�����ł��邱�Ƃ�\���v���p�e�B</summary>
    public bool IsAttack => _isAttack || _isUpAttack;
    /// <summary>�^����_���[�W�̃v���p�e�B</summary>
    public int Damage => _damage;
    /// <summary>�_���[�W��H�炢�ړ����ł��邱�Ƃ�\���v���p�e�B</summary>
    public bool IsDamage { get => _isDamage; set => _isDamage = value;}

    float _timer;
    float _spawnTimer;

    BoxCollider2D _collider;
    Animator _animator;

    GameObject _player;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _sound;


    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player");

        _timer = 2.5f;

        _audioSource.clip = _sound;
    }


    void FixedUpdate()
    {
        BossAttackMove();
        BossCollider();
    }


    /// <summary>�{�X�̍U���S�ʂ��Ǘ����郁�\�b�h</summary>
    void BossAttackMove()
    {
        if (!_isDamage)
        {
            _timer += Time.deltaTime;
            _spawnTimer += Time.deltaTime;

            if (_timer > _attackInterval)
            {
                _audioSource.Play();

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

            if (_spawnTimer > _attackInterval * 3)
            {
                Instantiate(_enemyPrefab, _spawnPos.position, transform.rotation);
                _spawnTimer = 0;
            }
        }
        else
        {
            _timer = 0;
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


    void BossCollider()
    {
        if (_isDamage)
        {
            _collider.enabled = false;
        }
        else
        {
            _collider.enabled = true;
        }

        _animator.SetBool("IsDamage", _isDamage);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage))
        {
            addDamage.AddDamage(_damage);
        }
    }
}
