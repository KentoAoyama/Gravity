using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField, Tooltip("アイテム取得時に出すエフェクト")] GameObject _effect;

    PlayerBeamStatus _playerBeamStatus;


    void Start()
    {
        _playerBeamStatus = FindObjectOfType<PlayerBeamStatus>().GetComponent<PlayerBeamStatus>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerBeamStatus.AddBeamGauge();
            Destroy(gameObject);
        }

        if (_effect)
        {
            Instantiate(_effect, transform.position, transform.rotation);
        }       
    }
}
