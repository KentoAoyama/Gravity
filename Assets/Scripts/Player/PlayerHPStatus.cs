using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>MVRPパターンのM(Model)にあたるクラス。GUIに表示するデータの実体をもつ部分のこと</summary>
public class PlayerHPStatus : MonoBehaviour, IAddDamage
{
    const int Hp = 100; //HPの値を定義

    int _maxHp = Hp;

    /// <summary>最大HP</summary>
    public int MaxHp => _maxHp;

    /// <summary>ReactivePropertyとして参照可能にする</summary>    //Presenterからのアクセスを可能にするため
    public IReadOnlyReactiveProperty<int> PlayerHP => _playerHP;
    //公開することで、Modelの内部状態が変化したときに
    readonly IntReactiveProperty _playerHP = new(Hp);              //それがObservableとして外部に通知できる


    [Tooltip("ダメージを受けるアニメーションの時間を表す定数")] const float Damage_Time = 0.5f;

    bool _isDamage;
    /// <summary>ダメージを受けたことを表すプロパティ</summary>
    public bool IsDamage => _isDamage;


    public void AddDamage(int damage)
    {
        _playerHP.Value -= damage;
        StartCoroutine(DamageCoroutine());
    }


    IEnumerator DamageCoroutine()
    {
        _isDamage = true;
        yield return new WaitForSeconds(Damage_Time);
        _isDamage = false;
    }


    /// <summary>プレイヤーのHPが回復した際に呼び出すメソッド</summary>
    public void Heal(int value)
    {
        _playerHP.Value += value;

        if (_playerHP.Value > 100)
        {
            _playerHP.Value = 100;
        }
    }


    void OnDestroy()
    {
        //いらなくなったら適宜破棄する
        _playerHP.Dispose();
    }
}
