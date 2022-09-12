using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBeamStatus : MonoBehaviour
{
    [SerializeField, Tooltip("�A�C�e���擾���ɉ��Z�����|�C���g")] int _addBeamPoint = 10;

    const float Beam = 0;

    const float _maxBeam = 100;

    /// <summary>�ő�Beam�o�[</summary>
    public float MaxBeam => _maxBeam;

    /// <summary>Beam��ReactiveProperty�Ƃ��ĎQ�Ɖ\��</summary>
    public IReadOnlyReactiveProperty<float> BeamCount => _beamCount;

    readonly FloatReactiveProperty _beamCount = new(Beam);


    bool _isBeamShoot;

    /// <summary>�r�[���ˌ�������\���ϐ�</summary>
    public bool IsBeamShoot { get => _isBeamShoot; set => _isBeamShoot = value; }


    void Update()
    {
        BeamSystem();
    }


    /// <summary>�r�[���̑������Ǘ����郁�\�b�h</summary>
    void BeamSystem()
    {
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2") && _beamCount.Value >= MaxBeam)
        {
            _isBeamShoot = true;
            _beamCount.Value = 0;
        }
    }


    /// <summary>�r�[���Q�[�W�𑝉������郁�\�b�h</summary>
    public void AddBeamGauge()
    {
        if (_beamCount.Value < _maxBeam)
        {
            _beamCount.Value += _addBeamPoint;
        }
    }
}
