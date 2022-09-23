using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class BossHealth : MonoBehaviour, IAddDamage
{
    [Header("HP")]
    [SerializeField, Tooltip("ボスのライフ")] int _maxHp = 10;
    /// <summary>ReactivePropertyとして参照可能なボスのHP</summary>
    readonly IntReactiveProperty _hp = new();
    /// <summary>ReactivePropertyとして参照可能なボスのHP</summary>
    readonly IntReactiveProperty _bossLevel = new();

    [Header("Damage")]
    [SerializeField, Tooltip("ダメージのエフェクト")] GameObject _damagePrefab;
    [SerializeField, Tooltip("ダメージのエフェクトを出す場所")] Transform _effectPos;

    [Header("Level")]
    [SerializeField, Tooltip("ボスが移動する座標")] Transform[] _bossPos;
    [SerializeField, Tooltip("移動にかかる時間")] float _moveTime = 2f;

    [Tooltip("敗北を表すbool")] bool _isLose;
    /// <summary>敗北を表すプロパティ</summary>
    public bool IsLose => _isLose;

    Animator _animator;
    BossAttack _bossAttack;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _bossAttack = GetComponent<BossAttack>();

        // UniRx の準備
        _hp.Value = _maxHp;
        _bossLevel.Value = 1;
        // ライフが減った際の処理を設定
        _hp.Skip(1).Subscribe(_ => BossDamage());
        //レベルが上がった際の処理を設定
        _bossLevel.Skip(1).Subscribe(x => BossLevel(x));
    }


    /// <summary>ボスがダメージをくらった時の処理</summary>
    void BossDamage()
    {
        if (_bossLevel.Value > 3)
        {
            _isLose = true;
            _animator.SetBool("IsLose", true);
        }
    }


    /// <summary>ボスのレベルが上がった時の処理</summary>
    void BossLevel(int level)
    {
        if (_damagePrefab)
        {
            //Instantiate();
        }

        if (level < 4)
        {
            StartCoroutine(DamageDelayCoroutine(level));
        }

        Debug.Log("ボスのレベルは" + _bossLevel + "　残り体力は" + _hp.Value);
    }


    IEnumerator DamageDelayCoroutine(int level)
    {
        _bossAttack.IsDamage = true;

        yield return new WaitForSeconds(1f);

        //レベルが上がるたびに配列の座標の位置に移動
        transform.DOMove(_bossPos[level - 2].position, _moveTime)
            //向きを反転
            .OnComplete(() =>
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1));

        yield return new WaitForSeconds(_moveTime);

        _bossAttack.IsDamage = false;
    }


    public void AddDamage(int damage)
    {
        if (_hp.Value > 1)
        {
            _hp.Value--;
            Debug.Log("ボスの残り体力は" + _hp.Value);
        }
        else
        {
            _bossLevel.Value++;
            _hp.Value = _maxHp;
        }
    }


    void OnDestroy()
    {
        //いらなくなったら適宜破棄する
        _hp.Dispose();
        _bossLevel.Dispose();
    }
}
