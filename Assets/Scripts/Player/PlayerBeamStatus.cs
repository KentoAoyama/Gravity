using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBeamStatus : MonoBehaviour
{
    [SerializeField, Tooltip("アイテム取得時に加算されるポイント")] int _addBeamPoint = 10;

    const float Beam = 0;

    const float _maxBeam = 100;

    /// <summary>最大Beamバー</summary>
    public float MaxBeam => _maxBeam;

    /// <summary>BeamもReactivePropertyとして参照可能に</summary>
    public IReadOnlyReactiveProperty<float> BeamCount => _beamCount;

    readonly FloatReactiveProperty _beamCount = new(Beam);


    bool _isBeamShoot;

    /// <summary>ビーム射撃中かを表す変数</summary>
    public bool IsBeamShoot { get => _isBeamShoot; set => _isBeamShoot = value; }


    void Update()
    {
        BeamSystem();
    }


    /// <summary>ビームの増減を管理するメソッド</summary>
    void BeamSystem()
    {
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2") && _beamCount.Value >= MaxBeam)
        {
            _isBeamShoot = true;
            _beamCount.Value = 0;
        }
    }


    /// <summary>ビームゲージを増加させるメソッド</summary>
    public void AddBeamGauge()
    {
        if (_beamCount.Value < _maxBeam)
        {
            _beamCount.Value += _addBeamPoint;
        }
    }
}
