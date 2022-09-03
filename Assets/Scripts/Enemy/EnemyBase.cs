using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IAddDamage
{
    [Header("Status")]
    [SerializeField] int _hp = 3;
    [SerializeField] GameObject _deathPrefab;
    
    bool _isActive;

    SpriteRenderer _targetRenderer;


    /// <summary>“G‚²‚Æ‚Ì“®‚«‚Ìˆ—</summary>
    public abstract void Move();
    
    /// <summary>“G‚²‚Æ‚ÌUŒ‚‚Ìˆ—</summary>
    public virtual void Attack() { }


    void Awake()
    {
        _targetRenderer = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        if (_targetRenderer.isVisible)@//ƒJƒƒ‰‚ÉÊ‚Á‚Ä‚¢‚½‚ç
        {
            _isActive = true;
        }

        if (_isActive)
        {
            Move();
            Attack();
        }

        if (_hp <= 0)
        {
            EnemyDeath();
        }
    }


    /// <summary>“G‚ªƒ_ƒ[ƒW‚ğó‚¯‚½‚Ìˆ—</summary>
    void EnemyDeath()
    {
        if (_deathPrefab)
        {
            Instantiate(_deathPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }


    public void AddDamage(int damage)
    {
        _hp -= damage;
    }
}
