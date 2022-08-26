using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    [SerializeField, Tooltip("íeÇÃë¨ìx")] float _bulletSpeed = 10;
    [SerializeField, Tooltip("êGÇÍÇΩÇÁîjâÛÇ≥ÇÍÇÈÉ^ÉOÇÃñºëO")] string[] _destroyTagName;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    
        _rb.velocity = transform.right * _bulletSpeed;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
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
        foreach (string tagName in _destroyTagName)
        {
            if (collision.gameObject.tag == tagName)
            {
                Destroy(gameObject);
            }
        }
    }
}
