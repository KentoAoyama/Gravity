using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleSystem : MonoBehaviour
{
    [SerializeField, Tooltip("GravityArrow")] GameObject _gravityArrow;
    [SerializeField, Tooltip("Bossのオブジェクト")] GameObject _boss;
    [SerializeField, Tooltip("Boss撃破後に開く扉")] GameObject _bossWall;
    [SerializeField, Tooltip("Boss撃破後に出る光")] GameObject _bossWallLight;

    bool _isBossFight;

    BossHealth _bossHealth;


    void Start()
    {
        _bossHealth = _boss.GetComponent<BossHealth>();

        _bossWall.SetActive(true);
        _bossWallLight.SetActive(false);
    }

    
    void Update()
    {
        if (_bossHealth.IsLose)
        {
            _bossWall.SetActive(false);
            _bossWallLight.SetActive(true);
        }
    }
}
