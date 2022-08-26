using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] int _hp = 3;
    [SerializeField] GameObject _deathPrefab;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            EnemyDamage();
        }

        if (collision.gameObject.tag == "Friend")
        {
            EnemyDeath();
        }
    }


    /// <summary>敵がダメージを受けた時の処理</summary>
    void EnemyDamage()
    {
        if (_hp > 0)
        {
            _hp--;
        }
        else
        {
            EnemyDeath();
        }
    }


    /// <summary>敵がダメージをやられた時の処理</summary>
    void EnemyDeath()
    {
        if (_deathPrefab)
        {
            Instantiate(_deathPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
