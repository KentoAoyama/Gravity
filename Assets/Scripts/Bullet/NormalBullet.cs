using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletBase
{
    Rigidbody2D _rb;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }


    public override void BulletMove()
    {
        //_rb.velocity = transform.right * _bulletSpeed;
        _rb.AddForce(transform.right * _bulletSpeed, ForceMode2D.Impulse);
    }
}
