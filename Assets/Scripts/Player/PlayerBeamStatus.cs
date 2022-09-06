using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBeamStatus : MonoBehaviour
{
    [SerializeField, Tooltip("ビームのゲージが上昇する速度")] float _beamCountSpeed = 2;
    [SerializeField, Tooltip("ゲージの上昇が適応される数")] float _beamChangeCount = 10;
    [Tooltip("Beamの総量を表すタイマー")] float _beamTimer;

    const float Beam = 0;

    const float _maxBeam = 100;

    /// <summary>最大Beamバー</summary>
    public float MaxBeam => _maxBeam;

    /// <summary>BeamもReactivePropertyとして参照可能に</summary>
    public IReadOnlyReactiveProperty<float> BeamCount => _beamCount;

    readonly FloatReactiveProperty _beamCount = new(Beam);



    void Update()
    {
        BeamSystem();
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

        if (Input.GetButton("Fire1") && Input.GetButton("Fire2") && _beamCount.Value > MaxBeam)
        {
            _beamCount.Value = 0;
        }
    }
}
