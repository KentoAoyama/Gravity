using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBeamStatus : MonoBehaviour
{
    const int Beam = 0;

    const int _maxBeam = 5;

    /// <summary>�ő�Beam�o�[</summary>
    public float MaxBeam => _maxBeam;

    /// <summary>Beam��ReactiveProperty�Ƃ��ĎQ�Ɖ\��</summary>
    public IReadOnlyReactiveProperty<int> BeamCount => _beamCount;

    readonly IntReactiveProperty _beamCount = new(Beam);


    bool _isBeamShoot;

    /// <summary>�r�[���ˌ�������\���ϐ�</summary>
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


    /// <summary>�r�[���̑������Ǘ����郁�\�b�h</summary>
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


    /// <summary>�r�[���Q�[�W�𑝉������郁�\�b�h</summary>
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
        //����Ȃ��Ȃ�����K�X�j������
        _beamCount.Dispose();
    }
}
