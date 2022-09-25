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


    /// <summary>処理ステップを表す列挙型</summary>
    enum AnimationStep
    {
        /// <summary>待機</summary>
        Stay,
        /// <summary>やられ</summary>
        Damage,
        /// <summary>攻撃</summary>
        Attack,
        /// <summary>移動</summary>
        Move,
        /// <summary>敗北</summary>
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


    // アニメーション 再生/変更 
    void BossAnimationChange(AnimationPattern pattern)
    {
        int timesPlay = 0;

        // SpriteStudio Anime を操作するためのクラス
        Script_SpriteStudio6_Root scriptRoot;
        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);

        if (scriptRoot != null)
        {
            switch (pattern)
            {
                case AnimationPattern.Attack:
                    timesPlay = 1;    // 一度だけ再生 
                    break;
                case AnimationPattern.Damage:
                    timesPlay = 1;    
                    break;
                case AnimationPattern.Lose:
                    timesPlay = 0;    // ループ再生
                    break;
                case AnimationPattern.Move:
                    timesPlay = 0;
                    break;
                case AnimationPattern.Stay:
                    timesPlay = 0;
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
