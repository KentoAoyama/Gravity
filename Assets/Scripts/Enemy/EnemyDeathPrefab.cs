using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathPrefab : MonoBehaviour
{
    [SerializeField, Tooltip("�A�C�e���擾���ɏo���G�t�F�N�g")] GameObject _effect;
    [SerializeField, Tooltip("�h���b�v���ɉ������")] float _pushPower = 5f;

    CircleCollider2D _collider;
    Rigidbody2D _rb;

    PlayerBeamStatus _playerBeamStatus;


    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _playerBeamStatus = FindObjectOfType<PlayerBeamStatus>().GetComponent<PlayerBeamStatus>();

        //�����_���ȕ����Ɏˏo����
        Vector2 randomUpVector = new(Random.Range(-1f, 1f), 1f);
        _rb.AddForce(randomUpVector * _pushPower, ForceMode2D.Impulse);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //�ڒn�������炻�̏ꂩ�瓮���Ȃ��悤�ɂ���
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
