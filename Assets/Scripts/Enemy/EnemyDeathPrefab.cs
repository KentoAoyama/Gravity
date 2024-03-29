using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathPrefab : MonoBehaviour
{
    [SerializeField, Tooltip("アイテム取得時に出すエフェクト")] GameObject _effect;
    [SerializeField, Tooltip("ドロップ時に加える力")] float _pushPower = 5f;

    CircleCollider2D _collider;
    Rigidbody2D _rb;

    PlayerBeamStatus _playerBeamStatus;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _sound;


    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _playerBeamStatus = FindObjectOfType<PlayerBeamStatus>().GetComponent<PlayerBeamStatus>();

        //ランダムな方向に射出する
        Vector2 randomUpVector = new(Random.Range(-1f, 1f), 1f);
        _rb.AddForce(randomUpVector * _pushPower, ForceMode2D.Impulse);

        if (_audioSource && _sound)
        {
            _audioSource.clip = _sound;
            _audioSource.Play();
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
