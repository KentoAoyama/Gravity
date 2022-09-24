using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField, Tooltip("アイテム取得時に出すエフェクト")] GameObject _effect;
    [SerializeField, Tooltip("回復するHPの量")] int _healPoint = 5;
    [SerializeField, Tooltip("ドロップ時に加える力")] float _pushPower = 5f;

    Rigidbody2D _rb;

    PlayerHPStatus _playerHPStatus;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerHPStatus = FindObjectOfType<PlayerHPStatus>().GetComponent<PlayerHPStatus>();

        //ランダムな方向に射出する
        Vector2 randomUpVector = new (Random.Range(-1f, 1f), 1f);
        _rb.AddForce(randomUpVector * _pushPower, ForceMode2D.Impulse);
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
