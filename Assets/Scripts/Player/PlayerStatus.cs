using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>MVRPパターンのM(Model)にあたるクラス。GUIに表示するデータの実体をもつ部分のこと</summary>
public class PlayerStatus : MonoBehaviour, IAddDamage
{
    const int Hp = 100; //HPの値を定義
    
    int _maxHp = Hp;

    /// <summary>最大HP</summary>
    public int MaxHp => _maxHp;

    /// <summary>ReactivePropertyとして参照可能にする</summary>  //Presenterからのアクセスを可能にするため
    public IReadOnlyReactiveProperty<int> PlayerHP => _playerHP;
                                                                    //公開することで、Modelの内部状態が変化したときに
    readonly IntReactiveProperty _playerHP = new (Hp);              //それがObservableとして外部に通知できる



    [SerializeField, Tooltip("ビームのゲージが上昇する速度")] float _beamCountSpeed = 2;
    [SerializeField, Tooltip("ゲージの上昇が適応される数")] float _beamChangeCount = 10;
    [Tooltip("Beamの総量を表すタイマー")] float _beamTimer;

    const float Beam = 0;

    const float _maxBeam = 100;

    /// <summary>最大Beamバー</summary>
    public float MaxBeam => _maxBeam;

    /// <summary>BeamもReactivePropertyとして参照可能に</summary>
    public IReadOnlyReactiveProperty<float> BeamCount => _beamCount;
    
    readonly FloatReactiveProperty _beamCount = new (Beam);



    void Update()
    {
        BeamSystem();        
    }


    public void AddDamage(int damage)
    {
        _playerHP.Value -= damage;
    }


    /// <summary>ビームの増減を管理するメソッド</summary>
    void BeamSystem()
    {
        _beamTimer += Time.deltaTime * _beamCountSpeed;

        if (_beamTimer > _beamChangeCount)
        {
            _beamCount.Value += _beamTimer;
            _beamTimer = 0;
        }

        if (Input.GetButtonDown("Fire2") && _beamCount.Value > MaxBeam)
        {
            _beamCount.Value = 0;
        }

    }

    /// <summary>プレイヤーのHPが回復した際に呼び出すメソッド</summary>
    public static void ItemGet()
    {
        
    }


    void OnDestroy()
    {
        //いらなくなったら適宜破棄する
        _playerHP.Dispose();  
    }
}
