using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>MVRP�p�^�[����V(View)�ɂ�����N���X�BuGUI(Unity Graphical User Interface)���g���Ă镔���̂���</summary>
public class SliderController : MonoBehaviour
{
    [SerializeField] float _sliderDownTime = 1f;
    Slider _slider;

    
    void Start()
    {
        _slider = GetComponent<Slider>();
    }


    /// <summary>View�Ƃ��Ĉ�����悤�ɂ��邽�߁APresenter����Q�Ɖ\�ɂ���</summary>
    /// <param name="value">�X�e�[�^�X�̌��݂̒l</param>
    public void SetValueDOTween(float value) 
    {
        DOTween.To(() => _slider.value,   //����
            n => _slider.value = n,       //����
            value,                        //�ǂ��܂�
            duration: _sliderDownTime);   //�ǂꂭ�炢�̎���
    }
}
