using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPunch : MonoBehaviour
{
    BossAttack _bossAttack;

    CircleCollider2D _collider;

    void Start()
    {
        _bossAttack = FindObjectOfType<BossAttack>().GetComponent<BossAttack>();
        _collider = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        if (_bossAttack.IsDamage)
        {
            _collider.enabled = false;
        }
        else
        {
            _collider.enabled = true;
        }
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
