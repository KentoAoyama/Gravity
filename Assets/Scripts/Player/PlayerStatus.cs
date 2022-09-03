using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>MVRP�p�^�[����M(Model)�ɂ�����N���X�BGUI�ɕ\������f�[�^�̎��̂��������̂���</summary>
public class PlayerStatus : MonoBehaviour, IAddDamage
{
    const int Hp = 100; //HP�̒l���`
    
    int _maxHp = Hp;

    /// <summary>�ő�HP</summary>
    public int MaxHp => _maxHp;

    /// <summary>ReactiveProperty�Ƃ��ĎQ�Ɖ\�ɂ���</summary>  //Presenter����̃A�N�Z�X���\�ɂ��邽��
    public IReadOnlyReactiveProperty<int> PlayerHP => _playerHP;
                                                                    //���J���邱�ƂŁAModel�̓�����Ԃ��ω������Ƃ���
    readonly IntReactiveProperty _playerHP = new (Hp);              //���ꂪObservable�Ƃ��ĊO���ɒʒm�ł���



    [SerializeField, Tooltip("�r�[���̃Q�[�W���㏸���鑬�x")] float _beamCountSpeed = 2;
    [SerializeField, Tooltip("�Q�[�W�̏㏸���K������鐔")] float _beamChangeCount = 10;
    [Tooltip("Beam�̑��ʂ�\���^�C�}�[")] float _beamTimer;

    const float Beam = 0;

    const float _maxBeam = 100;

    /// <summary>�ő�Beam�o�[</summary>
    public float MaxBeam => _maxBeam;

    /// <summary>Beam��ReactiveProperty�Ƃ��ĎQ�Ɖ\��</summary>
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


    /// <summary>�r�[���̑������Ǘ����郁�\�b�h</summary>
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

    /// <summary>�v���C���[��HP���񕜂����ۂɌĂяo�����\�b�h</summary>
    public static void ItemGet()
    {
        
    }


    void OnDestroy()
    {
        //����Ȃ��Ȃ�����K�X�j������
        _playerHP.Dispose();  
    }
}
