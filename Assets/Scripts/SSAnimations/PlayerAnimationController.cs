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


    /// <summary>処理ステップを表す列挙型</summary>
    enum AnimationStep
    {
        /// <summary>待機</summary>
        Stay,
        /// <summary>移動</summary>
        Move,
        /// <summary>落下</summary>
        Fall,
        /// <summary>やられ</summary>
        Damage,
        /// <summary>ジャンプ</summary>
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
            //=======待機中のアニメーションの遷移=======
            case AnimationStep.Stay:
                //やられ
                if (_playerHPStatus.IsDamage)
                {
                    //ダメージに変更
                    PlayerAnimationChange(AnimationPattern.Damage);
                    _step = AnimationStep.Damage;
                }
                //落下
                else if (_playerGravityStatus.IsFall)
                {
                    //向きに応じて落下に変更
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
                //移動
                else if (Mathf.Abs(_rb.velocity.x) > 0.1 || Mathf.Abs(_rb.velocity.y) > 0.1)
                {
                    //移動に変更
                    PlayerAnimationChange(AnimationPattern.Walk);
                    _step = AnimationStep.Move;
                }
                break;
            //=======移動中のアニメーションの遷移=======
            case AnimationStep.Move:
                //ジャンプ
                if (_gravityController.IsRotate)
                {
                    PlayerAnimationChange(AnimationPattern.Jump);
                    _step = AnimationStep.Jump;
                }
                //やられ
                else if (_playerHPStatus.IsDamage)
                {
                    //ダメージに変更
                    PlayerAnimationChange(AnimationPattern.Damage);
                    _step = AnimationStep.Damage;
                }
                //落下
                else if (_playerGravityStatus.IsFall)
                {
                    //向きに応じて落下に変更
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
                //何もなければ待機に遷移
                else if (Mathf.Abs(_rb.velocity.x) < 0.1 && Mathf.Abs(_rb.velocity.y) < 0.1)
                {
                    PlayerAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;
            //=======やられ中のアニメーションの遷移=======
            case AnimationStep.Damage:
                if (!IsAnimationPlay())
                {
                    // 待機に変更 
                    PlayerAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;
            //=======落下中のアニメーションの遷移=======
            case AnimationStep.Fall:
                if (!_playerGravityStatus.IsFall && _playerMove.OnGround)
                {
                    // 待機に変更 
                    PlayerAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;
            //=======ジャンプ中のアニメーションの遷移=======
            case AnimationStep.Jump:
                if (!_gravityController.IsRotate && _playerMove.OnGround)
                {
                    // 待機に変更 
                    PlayerAnimationChange(AnimationPattern.Stay);
                    _step = AnimationStep.Stay;
                }
                break;
        }
    }


    // アニメーション 再生/変更 
    void PlayerAnimationChange(AnimationPattern pattern)
    {
        int timesPlay = 0;

        // SpriteStudio Anime を操作するためのクラス
        Script_SpriteStudio6_Root scriptRoot;
        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);

        if (scriptRoot != null)
        {
            switch (pattern)
            {
                case AnimationPattern.Walk:
                    timesPlay = 0;    // ループ再生 
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
                    timesPlay = 1;    // 一度だけ再生 
                    break;
            }
            //アニメ―ションを再生
            scriptRoot.AnimationPlay(-1, (int)pattern, timesPlay);
        }
    }


    // アニメーションが再生中か取得
    bool IsAnimationPlay()
    {
        bool isPlay = false;

        Script_SpriteStudio6_Root scriptRoot;
        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);

        if (scriptRoot != null)
        {
            // 再生回数を取得して、プレイ終了かを判断
            int Remain = scriptRoot.PlayTimesGetRemain(0);

            if (Remain >= 0)
            {
                isPlay = true;
            }
        }

        return isPlay;
    }
}
