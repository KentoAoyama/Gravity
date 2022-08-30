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


    /// <summary>�G���Ƃ̓����̏���</summary>
    public abstract void Move();
    /// <summary>�G���Ƃ̍U���̏���</summary>
    public abstract void Attack();


    void Awake()
    {
        _targetRenderer = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        if (_targetRenderer.isVisible)�@//�J�����Ɏʂ��Ă�����
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


    /// <summary>�G���_���[�W���󂯂����̏���</summary>
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


    /// <summary>�G���_���[�W���󂯂����̏���</summary>
    void EnemyDeath()
    {
        if (_deathPrefab)
        {
            Instantiate(_deathPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
