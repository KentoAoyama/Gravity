using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField, Tooltip("’e‚Ì‘¬“x")] protected float _bulletSpeed = 10;
    [SerializeField, Tooltip("’e‚Ì‚ª—^‚¦‚éƒ_ƒ[ƒW")] protected int _bulletDamage = 1;
    [SerializeField, Tooltip("G‚ê‚½‚ç”j‰ó‚³‚ê‚éƒ^ƒO‚Ì–¼‘O")] protected string[] _destroyTagName;
    [SerializeField, Tooltip("“G‚Ì’e‚©‚Ç‚¤‚©")] bool _isEnemyBullet;


    /// <summary>’e‚Ì“®‚«‚Ìˆ—</summary>
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
