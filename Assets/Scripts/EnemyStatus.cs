using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] int _hp = 3;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            EnemyDamage();
        }
    }


    /// <summary>“G‚ªƒ_ƒ[ƒW‚ğó‚¯‚½‚Ìˆ—</summary>
    void EnemyDamage()
    {
        if (_hp > 0)
        {
            _hp--;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
