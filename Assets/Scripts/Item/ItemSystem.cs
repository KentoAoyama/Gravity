using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField, Tooltip("アイテム取得時に出すエフェクト")] GameObject _effect;
    [SerializeField, Tooltip("回復するHPの量")] int _healPoint = 5;

    Rigidbody2D _rb;
    BoxCollider2D _collider;

    PlayerHPStatus _playerHPStatus;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _playerHPStatus = FindObjectOfType<PlayerHPStatus>().GetComponent<PlayerHPStatus>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //接地をしたらその場から動かないようにする
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _collider.enabled = false;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerHPStatus.Heal(_healPoint);

            if (_effect)
            {
                Instantiate(_effect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
