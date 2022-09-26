using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃を行うインターバル")] float _attackInterval = 5f;
    [SerializeField, Tooltip("与えるダメージ")] int _damage = 10;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Transform _spawnPos;
    [Tooltip("攻撃をしているか")] bool _isAttack;
    [Tooltip("攻撃をしているか")] bool _isUpAttack;
    [Tooltip("何かしらのアクション中")] bool _isDamage;
    /// <summary>攻撃中であることを表すプロパティ</summary>
    public bool IsAttack => _isAttack || _isUpAttack;
    /// <summary>与えるダメージのプロパティ</summary>
    public int Damage => _damage;
    /// <summary>ダメージを食らい移動中であることを表すプロパティ</summary>
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


    /// <summary>ボスの攻撃全般を管理するメソッド</summary>
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
