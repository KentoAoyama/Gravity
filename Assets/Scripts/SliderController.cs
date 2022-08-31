using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>MVRP�p�^�[����M�iModel�j�ɂ�����N���X</summary>
public class SliderController : MonoBehaviour
{
    Slider _slider;

    
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    
    public void SetValue(float value)
    {
        DOTween.To(() => _slider.value,  //����
            n => _slider.value = n,      //����
            value,                       //�ǂ��܂�
            duration: 1.0f);             //�ǂꂭ�炢�̎���
    }
}
