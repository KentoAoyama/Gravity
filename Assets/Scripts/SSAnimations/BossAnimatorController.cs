using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorController : MonoBehaviour
{
    AnimationStep _step;

    GameObject _boss;

    BossAttack _bossAttack;
    BossHealth _bossHealth;


    enum AnimationPattern
    {
        Attack,
        Damage,
        Encount,
        Lose,
        Move,
        Stay
    }


    /// <summary>�����X�e�b�v��\���񋓌^</summary>
    enum AnimationStep
    {
        /// <summary>�ҋ@</summary>
        Stay,
        /// <summary>����</summary>
        Damage,
        /// <summary>�U��</summary>
        Attack,
        /// <summary>�ړ�</summary>
        Move,
        /// <summary>�s�k</summary>
        Lose
    }


    void Start()
    {
        _boss = FindObjectOfType<BossHealth>().gameObject;
        _bossAttack = _boss.GetComponent<BossAttack>();
        _bossHealth = _boss.GetComponent<BossHealth>();

        BossAnimationChange(AnimationPattern.Encount);       
    }

    
    void Update()
    {
        BossAnimation();
    }


    void BossAnimation()
    {
        switch(_step)
        {
            case AnimationStep.Stay:
                if (_bossHealth.IsLose)
                {
                    BossAnimationChange(AnimationPattern.Lose);
                    _step = AnimationStep.Lose;
                }
                else if (_bossAttack.IsDamage)
                {
                    BossAnimationChange(AnimationPattern.Damage);
                    _step = AnimationStep.Damage;
                }
                else if (_bossAttack.IsAttack)
                {
                    BossAnimationChange(AnimationPattern.Attack);
                    _step = AnimationStep.Attack;
                }
                break;

            case AnimationStep.Damage:
                if (!IsAnimationPlay())
                {
                    BossAnimationChange(AnimationPattern.Move);
                    _step = AnimationStep.Move;
                }
                break;

            case AnimationStep.Attack:
                if (_bossHealth.IsLose)
                {
                    BossAnimationChange(AnimationPattern.Lose);
                    _step = AnimationStep.Lose;
                }
                else if (_bossAttack.IsDamage)
                {
                    BossAnimationChange(AnimationPattern.Damage);
                    _step = AnimationStep.Damage;
                }
                else if (!IsAnimationPlay())
                {
                    BossAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;

            case AnimationStep.Move:
                if (!_bossAttack.IsDamage)
                {
                    BossAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;

            case AnimationStep.Lose:              
                break;
        }
    }


    // �A�j���[�V���� �Đ�/�ύX 
    void BossAnimationChange(AnimationPattern pattern)
    {
        int timesPlay = 0;

        // SpriteStudio Anime �𑀍삷�邽�߂̃N���X
        Script_SpriteStudio6_Root scriptRoot;
        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);

        if (scriptRoot != null)
        {
            switch (pattern)
            {
                case AnimationPattern.Attack:
                    timesPlay = 1;    // ��x�����Đ� 
                    break;
                case AnimationPattern.Damage:
                    timesPlay = 1;    
                    break;
                case AnimationPattern.Lose:
                    timesPlay = 0;    // ���[�v�Đ�
                    break;
                case AnimationPattern.Move:
                    timesPlay = 0;
                    break;
                case AnimationPattern.Stay:
                    timesPlay = 0;
                    break;
            }
            //�A�j���\�V�������Đ�
            scriptRoot.AnimationPlay(-1, (int)pattern, timesPlay);
        }
    }


    // �A�j���[�V�������Đ������擾
    bool IsAnimationPlay()
    {
        bool isPlay = false;

        Script_SpriteStudio6_Root scriptRoot;
        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);

        if (scriptRoot != null)
        {
            // �Đ��񐔂��擾���āA�v���C�I�����𔻒f
            int Remain = scriptRoot.PlayTimesGetRemain(0);

            if (Remain >= 0)
            {
                isPlay = true;
            }
        }

        return isPlay;
    }
}
