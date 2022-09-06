using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBeamStatus : MonoBehaviour
{
    [SerializeField, Tooltip("�r�[���̃Q�[�W���㏸���鑬�x")] float _beamCountSpeed = 2;
    [SerializeField, Tooltip("�Q�[�W�̏㏸���K������鐔")] float _beamChangeCount = 10;
    [Tooltip("Beam�̑��ʂ�\���^�C�}�[")] float _beamTimer;

    const float Beam = 0;

    const float _maxBeam = 100;

    /// <summary>�ő�Beam�o�[</summary>
    public float MaxBeam => _maxBeam;

    /// <summary>Beam��ReactiveProperty�Ƃ��ĎQ�Ɖ\��</summary>
    public IReadOnlyReactiveProperty<float> BeamCount => _beamCount;

    readonly FloatReactiveProperty _beamCount = new(Beam);



    void Update()
    {
        BeamSystem();
    }


    /// <summary>�r�[���̑������Ǘ����郁�\�b�h</summary>
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
