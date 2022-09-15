using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>MVRPパターンのP(Presenter)にあたるクラス。ModelとViewの橋渡しをするだけのもの</summary>
public class StatusPresenter : MonoBehaviour
{
    //紐付けたいModelとViewの両方を参照する

    [SerializeField, Tooltip("Model")] PlayerHPStatus _playerHpStatus;

    [SerializeField, Tooltip("Model")] PlayerBeamStatus _playerBeamStatus;

    [SerializeField, Tooltip("View")] SliderController _sliderControllerHp;

    [SerializeField, Tooltip("View")] BeamPointController _beamPointController;


    void Start()
    {
        //PlayerのHPを監視
        _playerHpStatus.PlayerHP
            .Subscribe(x =>
            {
                //Viewに反映
                _sliderControllerHp.SetValueDOTween((float)x / _playerHpStatus.MaxHp);               
            }).AddTo(this);  //通知を受け取った際に実行する関数を登録
   

        //PlayerのBeamのカウントを監視
        _playerBeamStatus.BeamCount
            .Subscribe(y =>
            {
                _beamPointController.ChangeBeamPoints(y);
            }).AddTo(this);
    }
}
