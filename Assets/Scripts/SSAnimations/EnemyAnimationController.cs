using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] EnemyBase _enemyBase;

    AnimationStep _step;


    enum AnimationPattern
    {
        Stay,
        Damage
    }


    /// <summary>�����X�e�b�v��\���񋓌^</summary>
    enum AnimationStep
    {
        /// <summary>�ҋ@</summary>
        Stay,
        /// <summary>����</summary>
        Damage,
    }


    void Update()
    {
        FriendAnimation();
    }


    void FriendAnimation()
    {
        switch (_step)
        {
            case AnimationStep.Stay:
                if (_enemyBase.IsDamage)
                {
                    FriendAnimationChange(AnimationPattern.Damage);
                    _step = AnimationStep.Damage;
                }
                break;
            case AnimationStep.Damage:
                if (!_enemyBase.IsDamage)
                {
                    FriendAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;

                }
                break;
        }
    }


    // �A�j���[�V���� �Đ�/�ύX 
    void FriendAnimationChange(AnimationPattern pattern)
    {
        int timesPlay = 0;

        // SpriteStudio Anime �𑀍삷�邽�߂̃N���X
        Script_SpriteStudio6_Root scriptRoot;
        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);

        if (scriptRoot != null)
        {
            switch (pattern)
            {
                case AnimationPattern.Stay:
                    timesPlay = 0;    // ���[�v�Đ� 
                    break;
                case AnimationPattern.Damage:
                    timesPlay = 1;    // ��x�����Đ� 
                    break;
            }
            //�A�j���\�V�������Đ�
            scriptRoot.AnimationPlay(-1, (int)pattern, timesPlay);
        }
    }
}
