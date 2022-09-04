using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>MVRPパターンのP(Presenter)にあたるクラス。ModelとViewの橋渡しをするだけのもの</summary>
public class StatusPresenter : MonoBehaviour
{
    //紐付けたいModelとViewの両方を参照する

    [SerializeField, Tooltip("Model")] PlayerStatus _playerStatus;

    [SerializeField, Tooltip("View")] SliderController _sliderControllerHp;

    [SerializeField, Tooltip("View")] SliderController _sliderControllerBeam;


    void Start()
    {
        //PlayerのHPを監視
        _playerStatus.PlayerHP
            .Subscribe(x =>
            {
                //Viewに反映
                _sliderControllerHp.SetValueDOTween((float)x / _playerStatus.MaxHp);               
            }).AddTo(this);  //通知を受け取った際に実行する関数を登録


        //PlayerのBeamのカウントを監視
        _playerStatus.BeamCount
            .Subscribe(y =>
            {
                _sliderControllerBeam.SetValueDOTween(y / _playerStatus.MaxBeam);
            }).AddTo(this);
    }
}
