using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>MVRPパターンのM（Model）にあたるクラス</summary>
public class SliderController : MonoBehaviour
{
    Slider _slider;

    
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    
    public void SetValue(float value)
    {
        DOTween.To(() => _slider.value,  //何に
            n => _slider.value = n,      //何を
            value,                       //どこまで
            duration: 1.0f);             //どれくらいの時間
    }
}
