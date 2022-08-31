using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField, Tooltip("�e�̑��x")] protected float _bulletSpeed = 10;
    [SerializeField, Tooltip("�e�̂��^����_���[�W")] protected int _bulletDamage = 1;
    [SerializeField, Tooltip("�G�ꂽ��j�󂳂��^�O�̖��O")] protected string[] _destroyTagName;
    [SerializeField, Tooltip("�G�̒e���ǂ���")] bool _isEnemyBullet;


    /// <summary>�e�̓����̏���</summary>
    public abstract void BulletMove();


    void Start()
    {
        BulletMove();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IAddDamage>(out var addDamage))
        {
            addDamage.AddDamage(_bulletDamage);
        }

        if (_destroyTagName != null)
        {
            DestroyBullet(collision);
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject);
        }
    }


    void DestroyBullet(Collider2D collision)
    {
        _destroyTagName.Where(i => collision.gameObject.CompareTag(i)).ToList().ForEach(i => Destroy(gameObject));
    }
}
