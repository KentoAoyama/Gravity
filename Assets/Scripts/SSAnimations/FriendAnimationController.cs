using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAnimationController : MonoBehaviour
{
    PlayerBeamStatus _playerBeam;

    AnimationStep _step;


    enum AnimationPattern
    {
        Stay,
        Beam
    }


    /// <summary>�����X�e�b�v��\���񋓌^</summary>
    enum AnimationStep
    {
        /// <summary>�ҋ@</summary>
        Stay,
        /// <summary>�ˌ�</summary>
        Shoot,
    }


    void Start()
    {
        _playerBeam = FindObjectOfType<PlayerBeamStatus>().GetComponent<PlayerBeamStatus>();
    }


    void Update()
    {
        FriendAnimation();
    }


    void FriendAnimation()
    {
        switch(_step)
        {
            case AnimationStep.Stay:
                if (_playerBeam.IsBeamShoot)
                {
                    FriendAnimationChange(AnimationPattern.Beam);
                    _step = AnimationStep.Shoot;
                }
                break;
            case AnimationStep.Shoot:
                if (!_playerBeam.IsBeamShoot)
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
                case AnimationPattern.Beam:
                    timesPlay = 1;    // ��x�����Đ� 
                    break;
            }
            //�A�j���\�V�������Đ�
            scriptRoot.AnimationPlay(-1, (int)pattern, timesPlay);
        }
    }
}
