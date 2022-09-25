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


    [SerializeField, Tooltip("GodMode�̎���")] float _godTime = 1.5f;
    [Tooltip("GodMode�̃��C���[")] const int GOD_MODE_LAYER = 13;
    [Tooltip("Player�̃��C���[")] const int PLAYER_LAYER = 8;
    [Tooltip("�_���[�W���󂯂�A�j���[�V�����̎��Ԃ�\���萔")] const float DAMAGE_TIME = 0.5f;

    bool _isDamage;
    /// <summary>�_���[�W���󂯂����Ƃ�\���v���p�e�B</summary>
    public bool IsDamage => _isDamage;

    Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        _animator.SetBool("IsDamage", _isDamage);
    }


    public void AddDamage(int damage)
    {
        _playerHP.Value -= damage;
        StartCoroutine(DamageCoroutine());
    }


    /// <summary>�_���[�W���󂯂��ۂ̏������R���[�`���Ŏ��s</summary>
    IEnumerator DamageCoroutine()
    {
        _isDamage = true;
        gameObject.layer = GOD_MODE_LAYER;
        yield return new WaitForSeconds(DAMAGE_TIME);

        _isDamage = false;
        yield return new WaitForSeconds(_godTime);

        gameObject.layer = PLAYER_LAYER;
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
