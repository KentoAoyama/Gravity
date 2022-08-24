using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletScript : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 10;
    [SerializeField] string[] _destroyTagName;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(pos);

        _rb.velocity = transform.right * _bulletSpeed;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_destroyTagName.Length != 0)
        {
            DestroyBullet(collision);
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
