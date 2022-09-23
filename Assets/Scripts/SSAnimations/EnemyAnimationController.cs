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


    /// <summary>処理ステップを表す列挙型</summary>
    enum AnimationStep
    {
        /// <summary>待機</summary>
        Stay,
        /// <summary>やられ</summary>
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
                case AnimationPattern.Damage:
                    timesPlay = 1;    // 一度だけ再生 
                    break;
            }
            //アニメ―ションを再生
            scriptRoot.AnimationPlay(-1, (int)pattern, timesPlay);
        }
    }
}
