using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>MVRPパターンのV（View）にあたるクラス</summary>
public class PlayerStatus : MonoBehaviour, IAddDamage
{
    [SerializeField, Tooltip("プレイヤーのHP")] static int playerHP;
    

    /// <summary>ReactivePropertyとして外部に情報を公開</summary>
    public IReadOnlyReactiveProperty<int> PlayerHP => _playerHP;

    readonly IntReactiveProperty _playerHP = new IntReactiveProperty(playerHP);


    public void AddDamage(int damage)
    {
        _playerHP.Value -= damage;
    }


    /// <summary>プレイヤーのHPが回復した際に呼び出すメソッド</summary>
    public static void ItemGet()
    {
        
    }


    void OnDestroy()
    {
        _playerHP.Dispose();  //いらなくなったら適宜破棄する
    }
}
