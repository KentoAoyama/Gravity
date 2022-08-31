using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>MVRP�p�^�[����V�iView�j�ɂ�����N���X</summary>
public class PlayerStatus : MonoBehaviour, IAddDamage
{
    [SerializeField, Tooltip("�v���C���[��HP")] static int playerHP;
    

    /// <summary>ReactiveProperty�Ƃ��ĊO���ɏ������J</summary>
    public IReadOnlyReactiveProperty<int> PlayerHP => _playerHP;

    readonly IntReactiveProperty _playerHP = new IntReactiveProperty(playerHP);


    public void AddDamage(int damage)
    {
        _playerHP.Value -= damage;
    }


    /// <summary>�v���C���[��HP���񕜂����ۂɌĂяo�����\�b�h</summary>
    public static void ItemGet()
    {
        
    }


    void OnDestroy()
    {
        _playerHP.Dispose();  //����Ȃ��Ȃ�����K�X�j������
    }
}
