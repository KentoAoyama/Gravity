using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    GameObject _player;

    Rigidbody2D _rb;

    PlayerMove _playerMove;
    GravityController _gravityController;
    PlayerGravityStatus _playerGravityStatus;
    PlayerHPStatus _playerHPStatus;

    AnimationStep _step;

    enum AnimationPattern
    {
        Walk = 0,
        Jump = 1,
        FallForward = 3,
        FallBack = 4,
        Stay = 5,
        Damage = 6
    }


    /// <summary>�����X�e�b�v��\���񋓌^</summary>
    enum AnimationStep
    {
        /// <summary>�ҋ@</summary>
        Stay,
        /// <summary>�ړ�</summary>
        Move,
        /// <summary>����</summary>
        Fall,
        /// <summary>����</summary>
        Damage,
        /// <summary>�W�����v</summary>
        Jump
    }


    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _rb = _player.GetComponent<Rigidbody2D>();
        _playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
        _gravityController = FindObjectOfType<GravityController>().GetComponent<GravityController>();
        _playerGravityStatus = FindObjectOfType<PlayerGravityStatus>().GetComponent<PlayerGravityStatus>();
        _playerHPStatus = FindObjectOfType<PlayerHPStatus>().GetComponent<PlayerHPStatus>();
    }


    void Update()
    {
        PlayerAnimation();
    }


    void PlayerAnimation()
    {
        switch (_step)
        {
            //=======�ҋ@���̃A�j���[�V�����̑J��=======
            case AnimationStep.Stay:
                //����
                if (_playerHPStatus.IsDamage)
                {
                    //�_���[�W�ɕύX
                    PlayerAnimationChange(AnimationPattern.Damage);
                    _step = AnimationStep.Damage;
                }
                //����
                else if (_playerGravityStatus.IsFall)
                {
                    //�����ɉ����ė����ɕύX
                    if (_player.transform.localRotation.z < 0)
                    {
                        PlayerAnimationChange(AnimationPattern.FallForward);
                    }
                    else
                    {
                        PlayerAnimationChange(AnimationPattern.FallBack);
                    }
                    _step = AnimationStep.Fall;
                }
                //�ړ�
                else if (Mathf.Abs(_rb.velocity.x) > 0.1 || Mathf.Abs(_rb.velocity.y) > 0.1)
                {
                    //�ړ��ɕύX
                    PlayerAnimationChange(AnimationPattern.Walk);
                    _step = AnimationStep.Move;
                }
                break;
            //=======�ړ����̃A�j���[�V�����̑J��=======
            case AnimationStep.Move:
                //�W�����v
                if (_gravityController.IsRotate)
                {
                    PlayerAnimationChange(AnimationPattern.Jump);
                    _step = AnimationStep.Jump;
                }
                //����
                else if (_playerHPStatus.IsDamage)
                {
                    //�_���[�W�ɕύX
                    PlayerAnimationChange(AnimationPattern.Damage);
                    _step = AnimationStep.Damage;
                }
                //����
                else if (_playerGravityStatus.IsFall)
                {
                    //�����ɉ����ė����ɕύX
                    if (_player.transform.localRotation.z < 0)
                    {
                        PlayerAnimationChange(AnimationPattern.FallForward);
                    }
                    else
                    {
                        PlayerAnimationChange(AnimationPattern.FallBack);
                    }
                    _step = AnimationStep.Fall;
                }
                //�����Ȃ���Αҋ@�ɑJ��
                else if (Mathf.Abs(_rb.velocity.x) < 0.1 && Mathf.Abs(_rb.velocity.y) < 0.1)
                {
                    PlayerAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;
            //=======���ꒆ�̃A�j���[�V�����̑J��=======
            case AnimationStep.Damage:
                if (!IsAnimationPlay())
                {
                    // �ҋ@�ɕύX 
                    PlayerAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;
            //=======�������̃A�j���[�V�����̑J��=======
            case AnimationStep.Fall:
                if (!_playerGravityStatus.IsFall && _playerMove.OnGround)
                {
                    // �ҋ@�ɕύX 
                    PlayerAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;
            //=======�W�����v���̃A�j���[�V�����̑J��=======
            case AnimationStep.Jump:
                if (!_gravityController.IsRotate && _playerMove.OnGround)
                {
                    // �ҋ@�ɕύX 
                    PlayerAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;
        }
    }


    // �A�j���[�V���� �Đ�/�ύX 
    void PlayerAnimationChange(AnimationPattern pattern)
    {
        int timesPlay = 0;

        // SpriteStudio Anime �𑀍삷�邽�߂̃N���X
        Script_SpriteStudio6_Root scriptRoot;
        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);

        if (scriptRoot != null)
        {
            switch (pattern)
            {
                case AnimationPattern.Walk:
                    timesPlay = 0;    // ���[�v�Đ� 
                    break;
                case AnimationPattern.Jump:
                    timesPlay = 0;
                    break;
                case AnimationPattern.FallForward:
                    timesPlay = 0;
                    break;
                case AnimationPattern.FallBack:
                    timesPlay = 0;
                    break;
                case AnimationPattern.Stay:
                    timesPlay = 0;
                    break;
                case AnimationPattern.Damage:
                    timesPlay = 1;    // ��x�����Đ� 
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
