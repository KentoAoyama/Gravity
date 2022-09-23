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


    [Tooltip("�_���[�W���󂯂�A�j���[�V�����̎��Ԃ�\���萔")] const float Damage_Time = 0.5f;

    bool _isDamage;
    /// <summary>�_���[�W���󂯂����Ƃ�\���v���p�e�B</summary>
    public bool IsDamage => _isDamage;


    public void AddDamage(int damage)
    {
        _playerHP.Value -= damage;
        StartCoroutine(DamageCoroutine());
    }


    IEnumerator DamageCoroutine()
    {
        _isDamage = true;
        yield return new WaitForSeconds(Damage_Time);
        _isDamage = false;
    }


    /// <summary>�v���C���[��HP���񕜂����ۂɌĂяo�����\�b�h</summary>
    public void Heal(int value)
    {
        _playerHP.Value += value;

        if (_playerHP.Value > 100)
        {
            _playerHP.Value = 100;
        }
    }


    void OnDestroy()
    {
        //����Ȃ��Ȃ�����K�X�j������
        _playerHP.Dispose();
    }
}
