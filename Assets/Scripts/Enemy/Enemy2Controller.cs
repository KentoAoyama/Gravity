using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField, Tooltip("d—Í‚Ì‘å‚«‚³")] float _gravityLevel = 20;
    
    [Header("Shoot")]
    [SerializeField, Tooltip("’e‚ÌƒvƒŒƒnƒu")] GameObject _bullet;
    [SerializeField, Tooltip("eŒû‚ÌˆÊ’u")] Transform _muzzle;
    [SerializeField, Tooltip("ËŒ‚‚ğs‚¤ŠÔŠu")] float _shootInterval;

    float _timer;

    SpriteRenderer _targetRenderer;
    Rigidbody2D _rb;


    void Start()
    {
        _targetRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }


    void FixedUpdate()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);

        if (_muzzle && _targetRenderer.isVisible)
        {
            Enemy2Shoot();
        }
    }


    /// <summary>“G‚Q‚ÌËŒ‚‚Ìˆ—</summary>
    void Enemy2Shoot()
    {
        _timer += Time.deltaTime;

        if (_timer > _shootInterval)
        {
            Instantiate(_bullet, _muzzle);
            _timer = 0;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
