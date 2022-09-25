using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBeamStatus : MonoBehaviour
{
    const int Beam = 0;

    const int _maxBeam = 5;

    /// <summary>最大Beamバー</summary>
    public float MaxBeam => _maxBeam;

    /// <summary>BeamもReactivePropertyとして参照可能に</summary>
    public IReadOnlyReactiveProperty<int> BeamCount => _beamCount;

    readonly IntReactiveProperty _beamCount = new(Beam);


    bool _isBeamShoot;

    /// <summary>ビーム射撃中かを表す変数</summary>
    public bool IsBeamShoot { get => _isBeamShoot; set => _isBeamShoot = value; }


    [SerializeField] AudioSource[] _audioSource;
    [SerializeField] AudioClip[] _sound;
    [SerializeField] AudioSource _itemAudioSource;
    [SerializeField] AudioClip _itemSound;


    void Start()
    {
        for (int i = 0; i < _audioSource.Length; i++)
        {
            _audioSource[i].clip = _sound[i];
        }
        _itemAudioSource.clip = _itemSound;
    }


    void Update()
    {
        BeamSystem();
    }


    /// <summary>ビームの増減を管理するメソッド</summary>
    void BeamSystem()
    {
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2") && _beamCount.Value >= MaxBeam ||
            Input.GetButton("Fire1") && Input.GetButton("Fire3") && _beamCount.Value >= MaxBeam)
        {
            _isBeamShoot = true;
            _beamCount.Value = 0;

            foreach(var sound in _audioSource)
            {
                sound.Play();
            }
        }
    }


    /// <summary>ビームゲージを増加させるメソッド</summary>
    public void AddBeamGauge()
    {
        _itemAudioSource.Play();
        if (_beamCount.Value < _maxBeam)
        {
            _beamCount.Value++;
        }
    }


    void OnDestroy()
    {
        //いらなくなったら適宜破棄する
        _beamCount.Dispose();
    }
}
