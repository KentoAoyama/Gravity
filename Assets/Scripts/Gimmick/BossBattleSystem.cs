using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleSystem : MonoBehaviour
{
    [SerializeField, Tooltip("GravityArrow")] GameObject _gravityArrow;
    [SerializeField, Tooltip("Boss�̃I�u�W�F�N�g")] GameObject _boss; 
    bool _isBossFight;


    void Start()
    {
        //_gravityArrow.SetActive(true);
        _boss.SetActive(false);
    }

    
    void Update()
    {
        
    }
}
