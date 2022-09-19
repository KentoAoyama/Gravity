using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>MVRP�p�^�[����M(Model)�ɂ�����N���X�BGUI�ɕ\������f�[�^�̎��̂��������̂���</summary>
public class PlayerHPStatus : MonoBehaviour, IAddDamage
{
    const int Hp = 100; //HP�̒l���`

    int _maxHp = Hp;

    /// <summary>�ő�HP</summary>
    public int MaxHp => _maxHp;

    /// <summary>ReactiveProperty�Ƃ��ĎQ�Ɖ\�ɂ���</summary>    //Presenter����̃A�N�Z�X���\�ɂ��邽��
    public IReadOnlyReactiveProperty<int> PlayerHP => _playerHP;
                                                                   //���J���邱�ƂŁAModel�̓�����Ԃ��ω������Ƃ���
    readonly IntReactiveProperty _playerHP = new(Hp);              //���ꂪObservable�Ƃ��ĊO���ɒʒm�ł���


    public void AddDamage(int damage)
    {
        _playerHP.Value -= damage;
    }


    /// <summary>�v���C���[��HP���񕜂����ۂɌĂяo�����\�b�h</summary>
    public void Heal(int value)
    {
        _playerHP.Value += value;
    }


    void OnDestroy()
    {
        //����Ȃ��Ȃ�����K�X�j������
        _playerHP.Dispose();
    }
}
