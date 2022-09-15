using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>MVRP�p�^�[����P(Presenter)�ɂ�����N���X�BModel��View�̋��n�������邾���̂���</summary>
public class StatusPresenter : MonoBehaviour
{
    //�R�t������Model��View�̗������Q�Ƃ���

    [SerializeField, Tooltip("Model")] PlayerHPStatus _playerHpStatus;

    [SerializeField, Tooltip("Model")] PlayerBeamStatus _playerBeamStatus;

    [SerializeField, Tooltip("View")] SliderController _sliderControllerHp;

    [SerializeField, Tooltip("View")] BeamPointController _beamPointController;


    void Start()
    {
        //Player��HP���Ď�
        _playerHpStatus.PlayerHP
            .Subscribe(x =>
            {
                //View�ɔ��f
                _sliderControllerHp.SetValueDOTween((float)x / _playerHpStatus.MaxHp);               
            }).AddTo(this);  //�ʒm���󂯎�����ۂɎ��s����֐���o�^
   

        //Player��Beam�̃J�E���g���Ď�
        _playerBeamStatus.BeamCount
            .Subscribe(y =>
            {
                _beamPointController.ChangeBeamPoints(y);
            }).AddTo(this);
    }
}
