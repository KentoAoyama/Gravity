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


    /// <summary>処理ステップを表す列挙型</summary>
    enum AnimationStep
    {
        /// <summary>待機</summary>
        Stay,
        /// <summary>射撃</summary>
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


    // アニメーション 再生/変更 
    void FriendAnimationChange(AnimationPattern pattern)
    {
        int timesPlay = 0;

        // SpriteStudio Anime を操作するためのクラス
        Script_SpriteStudio6_Root scriptRoot;
        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);

        if (scriptRoot != null)
        {
            switch (pattern)
            {
                case AnimationPattern.Stay:
                    timesPlay = 0;    // ループ再生 
                    break;
                case AnimationPattern.Beam:
                    timesPlay = 1;    // 一度だけ再生 
                    break;
            }
            //アニメ―ションを再生
            scriptRoot.AnimationPlay(-1, (int)pattern, timesPlay);
        }
    }
}
