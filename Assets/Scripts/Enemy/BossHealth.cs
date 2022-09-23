using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class BossHealth : MonoBehaviour, IAddDamage
{
    [Header("HP")]
    [SerializeField, Tooltip("�{�X�̃��C�t")] int _maxHp = 10;
    /// <summary>ReactiveProperty�Ƃ��ĎQ�Ɖ\�ȃ{�X��HP</summary>
    readonly IntReactiveProperty _hp = new();
    /// <summary>ReactiveProperty�Ƃ��ĎQ�Ɖ\�ȃ{�X��HP</summary>
    readonly IntReactiveProperty _bossLevel = new();

    [Header("Damage")]
    [SerializeField, Tooltip("�_���[�W�̃G�t�F�N�g")] GameObject _damagePrefab;
    [SerializeField, Tooltip("�_���[�W�̃G�t�F�N�g���o���ꏊ")] Transform _effectPos;

    [Header("Level")]
    [SerializeField, Tooltip("�{�X���ړ�������W")] Transform[] _bossPos;
    [SerializeField, Tooltip("�ړ��ɂ����鎞��")] float _moveTime = 2f;

    BossAttack _bossAttack;


    void Start()
    {
        _bossAttack = GetComponent<BossAttack>();

        // UniRx �̏���
        _hp.Value = _maxHp;
        _bossLevel.Value = 1;
        // ���C�t���������ۂ̏�����ݒ�
        _hp.Skip(1).Subscribe(_ => BossDamage());
        //���x�����オ�����ۂ̏�����ݒ�
        _bossLevel.Skip(1).Subscribe(x => BossLevel(x));
    }


    void FixedUpdate()
    {

    }


    /// <summary>�{�X���_���[�W������������̏���</summary>
    void BossDamage()
    {
        Debug.Log("�_���[�W�̉��o");
    }


    /// <summary>�{�X�̃��x�����オ�������̏���</summary>
    void BossLevel(int Level)
    {      
        if (_damagePrefab)
        {
            //Instantiate();
        }

        StartCoroutine(DamageDelayCoroutine());

        //���x�����オ�邽�тɔz��̍��W�̈ʒu�Ɉړ�
        transform.DOMove(_bossPos[Level - 2].position, _moveTime)
            //�����𔽓]
            .OnComplete(() => 
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1));


        Debug.Log("�{�X�̃��x����" + _bossLevel + "�@�c��̗͂�" + _hp.Value);
    }


    IEnumerator DamageDelayCoroutine()
    {
        _bossAttack.IsDamage = true;

        yield return new WaitForSeconds(_moveTime);

        _bossAttack.IsDamage = false;
    }


    public void AddDamage(int damage)
    {
        if (_hp.Value > 0)
        {
            _hp.Value--;
            Debug.Log("�{�X�̎c��̗͂�" + _hp.Value);
        }
        else
        {
            _bossLevel.Value++;
            _hp.Value = _maxHp;
            //_bossAttack.IsAction = true;

            if (_bossLevel.Value > 3)
            {
                //�{�X�̂���鏈��������
            }
        }
    }
}
