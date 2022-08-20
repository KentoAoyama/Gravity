using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletScript : BulletBase
{    
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _rb.velocity = transform.up * _bulletSpeed;
    }
}
