using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletBase
{
    [SerializeField, Tooltip("èdóÕÇÃëÂÇ´Ç≥")] float _gravityLevel = 10f;
    Rigidbody2D _rb;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        _rb.AddForce(new Vector2(0, -1) * _gravityLevel, ForceMode2D.Force);
    }


    public override void BulletMove()
    {
        _rb.AddForce(transform.right * _bulletSpeed, ForceMode2D.Impulse);
    }
}
