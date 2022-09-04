using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>MVRPパターンのV(View)にあたるクラス。uGUI(Unity Graphical User Interface)を使ってる部分のこと</summary>
public class SliderController : MonoBehaviour
{
    [SerializeField] float _sliderDownTime = 1f;
    Slider _slider;

    
    void Start()
    {
        _slider = GetComponent<Slider>();
    }


    /// <summary>Viewとして扱えるようにするため、Presenterから参照可能にする</summary>
    /// <param name="value">ステータスの現在の値</param>
    public void SetValueDOTween(float value) 
    {
        DOTween.To(() => _slider.value,   //何に
            n => _slider.value = n,       //何を
            value,                        //どこまで
            duration: _sliderDownTime);   //どれくらいの時間
    }
}
