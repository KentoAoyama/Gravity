using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class EnemyBase : MonoBehaviour, IAddDamage
{
    [Header("Status")]
    [SerializeField, Tooltip("敵のHP")] int _maxHp = 3;
    /// <summary>敵のHPを表すReactiveProperty</summary>
    readonly IntReactiveProperty _hp = new ();

    [Header("Death")]
    [SerializeField, Tooltip("死亡時に出すプレハブ")] GameObject _deathPrefab;
    [SerializeField, Tooltip("死亡時に出すアイテム")] GameObject _deathItem;
    [SerializeField, Tooltip("死亡時に出すアイテムの最小数")] int _itemValueMin = 1;
    [SerializeField, Tooltip("死亡時に出すアイテムの最大数")] int _itemValueMax = 3;

    [Header("Warning")]
    [Tooltip("プレイヤーを発見したか")] protected bool _isWarning = false;
    [SerializeField, Tooltip("プレイヤーを発見する距離")] protected float _warningDis = 10f;

    [Tooltip("行動の開始")] bool _isActive;

    protected GameObject _player;

    protected SpriteRenderer _renderer;
    protected Animator _animator;


    /// <summary>敵ごとの動きの処理</summary>
    public abstract void Move();

    /// <summary>敵ごとの攻撃の処理</summary>
    public virtual void Attack() { }

    /// <summary>敵ごとのダメージの処理</summary>
    public abstract void Damage();



    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _hp.Value = _maxHp;
        //ライフが減った際に処理を実行する
        _hp.Skip(1).Subscribe(_ => Damage());
    }


    void Update()
    {
        if (_renderer.isVisible)　//カメラに写っていたら
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


    /// <summary>Warningがtrueになった時の動き</summary>
    void EnemyWarning()
    {
        _animator.Play("EnemyWarning");
    }
    

    /// <summary>敵がダメージを受けてやられた時の処理</summary>
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
