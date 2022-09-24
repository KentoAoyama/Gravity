using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField, Tooltip("�A�C�e���擾���ɏo���G�t�F�N�g")] GameObject _effect;
    [SerializeField, Tooltip("�񕜂���HP�̗�")] int _healPoint = 5;
    [SerializeField, Tooltip("�h���b�v���ɉ������")] float _pushPower = 5f;

    Rigidbody2D _rb;

    PlayerHPStatus _playerHPStatus;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerHPStatus = FindObjectOfType<PlayerHPStatus>().GetComponent<PlayerHPStatus>();

        //�����_���ȕ����Ɏˏo����
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
