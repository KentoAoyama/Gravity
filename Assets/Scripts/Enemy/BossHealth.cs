using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BossHealth : MonoBehaviour
{
    [SerializeField, Tooltip("�{�X�̃��C�t")] int _maxHp = 10;
    /// <summary>ReactiveProperty�Ƃ��ĎQ�Ɖ\�ȃ{�X��HP</summary>
    readonly IntReactiveProperty _hp = new();

    int _bossLevel = 0;

    [SerializeField, Tooltip("�_���[�W�̃G�t�F�N�g")] GameObject _damagePrefab;
    [SerializeField, Tooltip("�_���[�W�̃G�t�F�N�g���o���ꏊ")] Transform _effectPos;

    BossController _bossController;


    void Start()
    {
        _bossController = GetComponent<BossController>();

        // UniRx �̏���
        _hp.Value = _maxHp;
        // ���C�t�����������Ɏ��s���鏈����ݒ肷��
        _hp.Skip(1).Subscribe(_ => BossDamage());
    }


    void FixedUpdate()
    {

    }


    /// <summary>�{�X���_���[�W������������̏���</summary>
    void BossDamage()
    {
        if (_hp.Value > 0)
        {
            _hp.Value--;
            Debug.Log("�{�X�̎c��̗͂�" + _hp.Value);
        }
        else
        {
            _bossLevel++;
            _hp.Value = _maxHp;
            //_bossController.IsAction = true;

            if (_bossLevel > 3 && _damagePrefab)
            {
                //Instantiate
            }
            Debug.Log("�{�X�̃��x����" + _bossLevel + "�@�c��̗͂�" + _hp.Value);
        }
    }
}
