using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public abstract class EnemyBase : MonoBehaviour, IAddDamage
{
    [Header("Status")]
    [SerializeField, Tooltip("�G��HP")] int _hp = 3;
    [SerializeField, Tooltip("���S���ɏo���v���n�u")] GameObject _deathPrefab;

    [Tooltip("�s���̊J�n")] bool _isActive;

    SpriteRenderer _targetRenderer;


    /// <summary>�G���Ƃ̓����̏���</summary>
    public abstract void Move();

    /// <summary>�G���Ƃ̍U���̏���</summary>
    public virtual void Attack() { }


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

        if (_hp <= 0)
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


    public void AddDamage(int damage)
    {
        _hp -= damage;
    }
}
