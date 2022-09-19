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


    public void AddDamage(int damage)
    {
        _playerHP.Value -= damage;
    }


    /// <summary>プレイヤーのHPが回復した際に呼び出すメソッド</summary>
    public void Heal(int value)
    {
        _playerHP.Value += value;
    }


    void OnDestroy()
    {
        //いらなくなったら適宜破棄する
        _playerHP.Dispose();
    }
}
