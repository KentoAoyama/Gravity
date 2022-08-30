using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] int _hp = 3;
    [SerializeField] GameObject _deathPrefab;
    
    bool _isActive;

    SpriteRenderer _targetRenderer;


    /// <summary>敵ごとの動きの処理</summary>
    public abstract void Move();
    /// <summary>敵ごとの攻撃の処理</summary>
    public abstract void Attack();


    void Awake()
    {
        _targetRenderer = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        if (_targetRenderer.isVisible)　//カメラに写っていたら
        {
            _isActive = true;
        }

        if (_isActive)
        {
            Move();
            Attack();
        }
    }


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


    /// <summary>敵がダメージを受けた時の処理</summary>
    void EnemyDeath()
    {
        if (_deathPrefab)
        {
            Instantiate(_deathPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
