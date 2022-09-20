using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPunch : MonoBehaviour
{
    BossAttack _bossAttack;


    void Start()
    {
        _bossAttack = FindObjectOfType<BossAttack>().GetComponent<BossAttack>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage))
        {
            addDamage.AddDamage(_bossAttack.Damage);
            Debug.Log("ボスからプレイヤーにダメージ");
        }
    }
}
