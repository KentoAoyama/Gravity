using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField, Tooltip("弾の速度")] protected float _bulletSpeed = 10;
    [SerializeField, Tooltip("弾のが与えるダメージ")] protected int _bulletDamage = 1;
    [SerializeField, Tooltip("触れたら破壊されるタグの名前")] protected string[] _destroyTagName;
    [SerializeField, Tooltip("出すエフェクトのプレハブ")] protected GameObject _effect;
    [SerializeField, Tooltip("敵の弾かどうか")] bool _isEnemyBullet;

    const string CAMERA_TAGNAME = "MainCamera";

    /// <summary>弾の動きの処理</summary>
    public virtual void BulletMove() { }


    void Start()
    {
        BulletMove();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage))
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
        if (collision.gameObject.tag == CAMERA_TAGNAME)
        {
            Destroy(gameObject);
        }
    }


    void DestroyBullet(Collider2D collision)
    {
        _destroyTagName.Where(i => collision.gameObject.CompareTag(i)).ToList().ForEach(i => Destroy(gameObject));

        if (_effect)
        {
            Instantiate(_effect, transform.position, transform.rotation);
        }
    }
}
