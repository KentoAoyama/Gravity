using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    protected float _bulletSpeed = 10;
    [SerializeField] string[] _destroyTagName;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "MainCamera")
        {
            Destroy(gameObject);
        }

        foreach (string tagName in _destroyTagName)
        {
            if (collision.gameObject.tag == tagName)
            {
                Destroy(gameObject);
            }
        }
    }
}
