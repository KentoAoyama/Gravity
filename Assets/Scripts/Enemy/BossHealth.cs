using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BossHealth : MonoBehaviour
{
    [SerializeField, Tooltip("ボスのライフ")] int _maxHp = 10;
    /// <summary>ReactivePropertyとして参照可能なボスのHP</summary>
    readonly IntReactiveProperty _hp = new();

    int _bossLevel = 0;

    [SerializeField, Tooltip("ダメージのエフェクト")] GameObject _damagePrefab;
    [SerializeField, Tooltip("ダメージのエフェクトを出す場所")] Transform _effectPos;

    BossController _bossController;


    void Start()
    {
        _bossController = GetComponent<BossController>();

        // UniRx の準備
        _hp.Value = _maxHp;
        // ライフが減った時に実行する処理を設定する
        _hp.Skip(1).Subscribe(_ => BossDamage());
    }


    void FixedUpdate()
    {

    }


    /// <summary>ボスがダメージをくらった時の処理</summary>
    void BossDamage()
    {
        if (_hp.Value > 0)
        {
            _hp.Value--;
            Debug.Log("ボスの残り体力は" + _hp.Value);
        }
        else
        {
            _bossLevel++;
            _hp.Value = _maxHp;
            //_bossController.IsAction = true;

            if (_bossLevel > 3 && _damagePrefab)
            {
                //Instantiate
            }
            Debug.Log("ボスのレベルは" + _bossLevel + "　残り体力は" + _hp.Value);
        }
    }
}
