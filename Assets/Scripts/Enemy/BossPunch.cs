using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPunch : MonoBehaviour
{
    BossController _bossController;


    void Start()
    {
        _bossController = FindObjectOfType<BossController>().GetComponent<BossController>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage))
        {
            addDamage.AddDamage(_bossController.Damage);
        }
    }
}
