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
    [SerializeField, Tooltip("���S���ɏo���v���n�u")] GameObject _deathPrefab;
    [SerializeField, Tooltip("���S���ɏo���A�C�e��")] GameObject _deathItem;
    [SerializeField, Tooltip("���S���ɏo���A�C�e���̍ŏ���")] int _itemValueMin = 1;
    [SerializeField, Tooltip("���S���ɏo���A�C�e���̍ő吔")] int _itemValueMax = 3;

    [Header("Warning")]
    [Tooltip("�v���C���[�𔭌�������")] protected bool _isWarning = false;
    [SerializeField, Tooltip("�v���C���[�𔭌����鋗��")] protected float _warningDis = 10f;

    [Tooltip("�s���̊J�n")] bool _isActive;

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
        if (_renderer.isVisible)�@//�J�����Ɏʂ��Ă�����
        {
            _isActive = true;
        }

        if (_isWarning)
        {
            EnemyWarning();
        }

        if (_isActive)
        {
            Move();
            Attack();
        }

        if (_hp.Value <= 0)
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
        if (_deathPrefab)
        {
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
    }
}
