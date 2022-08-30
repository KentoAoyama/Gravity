using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : EnemyBase
{
    [Header("Gravity")]
    [SerializeField, Tooltip("d—Í‚Ì‘å‚«‚³")] float _gravityLevel = 20;
    
    [Header("Shoot")]
    [SerializeField, Tooltip("’e‚ÌƒvƒŒƒnƒu")] GameObject _bullet;
    [SerializeField, Tooltip("eŒû‚ÌˆÊ’u")] Transform _muzzle;
    [SerializeField, Tooltip("ËŒ‚‚ğs‚¤ŠÔŠu")] float _shootInterval;

    float _timer;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }


    public override void Move()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);
    }
        

    /// <summary>“G‚Q‚ÌËŒ‚‚Ìˆ—</summary>
    public override void Attack()
    {
        _timer += Time.deltaTime;

        if (_timer > _shootInterval)
        {
            Instantiate(_bullet, _muzzle.position, transform.rotation);@//ˆê’èŠÔŠu‚Å’e‚ğ¶¬
            _timer = 0;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;@//Ú’n‚ğ‚µ‚½‚ç‚»‚Ìê‚©‚ç“®‚©‚È‚¢‚æ‚¤‚É‚·‚é
        }
    }
}
