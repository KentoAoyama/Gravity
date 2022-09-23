using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleSystem : MonoBehaviour
{
    [SerializeField, Tooltip("GravityArrow")] GameObject _gravityArrow;
    [SerializeField, Tooltip("Boss�̃I�u�W�F�N�g")] GameObject _boss;
    [SerializeField, Tooltip("Boss���j��ɊJ����")] GameObject _bossWall;
    [SerializeField, Tooltip("Boss���j��ɏo���")] GameObject _bossWallLight;

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
