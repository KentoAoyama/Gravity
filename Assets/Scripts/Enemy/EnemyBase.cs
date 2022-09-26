using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class EnemyBase : MonoBehaviour, IAddDamage
{
    [Header("Status")]
    [SerializeField, Tooltip("�G��HP")] int _maxHp = 3;
    /// <summary>�G��HP��\��ReactiveProperty</summary>
    readonly IntReactiveProperty _hp = new ();


    [Header("Death")]
    [SerializeField, Tooltip("���S���ɏo��Effect")] GameObject _deathEffect;
    [SerializeField, Tooltip("���S���ɏo���v���n�u")] GameObject _deathPrefab;
    [SerializeField, Tooltip("���S���ɏo���A�C�e��")] GameObject _deathItem;
    [SerializeField, Tooltip("���S���ɏo���A�C�e���̍ŏ���")] int _itemValueMin = 1;
    [SerializeField, Tooltip("���S���ɏo���A�C�e���̍ő吔")] int _itemValueMax = 3;
    [Tooltip("���S���Ă��炻�̏�Ɏc�鎞��")] float _deathInterval = 0.3f;
    [Tooltip("GodMode�̃��C���[")] const int DEATH_LAYER = 12;
    [Tooltip("���S���Ă��邩�ǂ���")] bool _isDeath;


    [Header("Warning")]
    [Tooltip("�v���C���[�𔭌�������")] protected bool _isWarning = false;
    [SerializeField, Tooltip("�v���C���[�𔭌����鋗��")] protected float _warningDis = 10f;

    [Tooltip("�s���̊J�n")] bool _isActive;
    [Tooltip("�_���[�W��������Ă���")] protected bool _isDamage;
    [SerializeField] bool _isGameOver;
    /// <summary>�_���[�W���󂯂Ă��邱�Ƃ�\���v���p�e�B</summary>
    public bool IsDamage => _isDamage;

    protected GameObject _player;

    protected SpriteRenderer _renderer;
    protected Animator _animator;


    /// <summary>�G���Ƃ̓����̏���</summary>
    public abstract void Move();

    /// <summary>�G���Ƃ̍U���̏���</summary>
    public virtual void Attack() { }

    /// <summary>�G���Ƃ̃_���[�W�̏���</summary>
    public abstract void Damage();



    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _hp.Value = _maxHp;
        //���C�t���������ۂɏ��������s����
        _hp.Skip(1).Subscribe(_ => Damage());
    }


    void Update()
    {
        _isActive = _renderer.isVisible;�@//�J�����Ɏʂ��Ă�����

        if (_isDamage)
        {
            _isWarning = true;
        }

        if (_isWarning)
        {
            EnemyWarning();
        }

        if (_isActive && !_isDamage && _renderer.isVisible && !PlayerHPStatus._isGameOver)
        {
            Move();
            Attack();
        }

        if (_hp.Value <= 0 && !_isDeath)
        {
            EnemyDeath();
        }
    }


    /// <summary>Warning��true�ɂȂ������̓���</summary>
    void EnemyWarning()
    {
        _animator.Play("EnemyWarning");
    }
    

    /// <summary>�G���_���[�W���󂯂Ă��ꂽ���̏���</summary>
    void EnemyDeath()
    {
        _isDeath = true;
        gameObject.layer = DEATH_LAYER;

        StartCoroutine(DeathDelay());
    }


    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(_deathInterval);

        if (_deathEffect && _deathPrefab)
        {
            Instantiate(_deathEffect, transform.position, transform.rotation);
            Instantiate(_deathPrefab, transform.position, transform.rotation);
        }

        if (_deathItem)
        {
            int itemNumber = Random.Range(_itemValueMin, _itemValueMax);
            for (int i = 0; i < itemNumber; i++)
            {
                Instantiate(_deathItem, transform.position, transform.rotation);
            }
        }

        Destroy(gameObject);
    }


    public void AddDamage(int damage)
    {
        _hp.Value -= damage;
        StartCoroutine(DamegeDelay());
    }


    IEnumerator DamegeDelay()
    {
        _isDamage = true;
        yield return new WaitForSeconds(0.45f);
        _isDamage = false;
    }


    void OnDestroy()
    {
        _hp.Dispose();
    }
}
