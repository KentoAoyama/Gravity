using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBullet : BulletBase
{
    GameObject _player;

    Rigidbody2D _rb;


    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }


    public override void BulletMove()
    {
        _rb.velocity = (_player.transform.position - transform.position).normalized * _bulletSpeed;
    }
}
