using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>MVRP�p�^�[����P(Presenter)�ɂ�����N���X�BModel��View�̋��n�������邾���̂���</summary>
public class StatusPresenter : MonoBehaviour
{
    //�R�t������Model��View�̗������Q�Ƃ���

    [SerializeField, Tooltip("Model")] PlayerStatus _playerStatus;

    [SerializeField, Tooltip("View")] SliderController _sliderControllerHp;

    [SerializeField, Tooltip("View")] SliderController _sliderControllerBeam;


    void Start()
    {
        //Player��HP���Ď�
        _playerStatus.PlayerHP
            .Subscribe(x =>
            {
                //View�ɔ��f
                _sliderControllerHp.SetValueDOTween((float)x / _playerStatus.MaxHp);               
            }).AddTo(this);  //�ʒm���󂯎�����ۂɎ��s����֐���o�^


        //Player��Beam�̃J�E���g���Ď�
        _playerStatus.BeamCount
            .Subscribe(y =>
            {
                _sliderControllerBeam.SetValueDOTween(y / _playerStatus.MaxBeam);
            }).AddTo(this);
    }
}
