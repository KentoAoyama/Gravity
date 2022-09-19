using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathPrefab : MonoBehaviour
{
    [SerializeField, Tooltip("アイテム取得時に出すエフェクト")] GameObject _effect;

    CircleCollider2D _collider;
    Rigidbody2D _rb;

    PlayerBeamStatus _playerBeamStatus;


    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _playerBeamStatus = FindObjectOfType<PlayerBeamStatus>().GetComponent<PlayerBeamStatus>();
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
            _playerBeamStatus.AddBeamGauge();   

            if (_effect)
            {
                Instantiate(_effect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
