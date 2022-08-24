using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField, Tooltip("�E����Ray���΂��ꏊ")] Transform _rightRayPos;
    [SerializeField, Tooltip("������Ray���΂��ꏊ")] Transform _leftRayPos;
    [SerializeField, Tooltip("Ray�������郌�C���[")] LayerMask _groundLayer;

    [Header("Move")]
    [SerializeField, Tooltip("�ړ��̑��x")] float _moveSpeed;
    [SerializeField, Tooltip("�^�[���ɂ����鎞��")] float _turnTime;
    [Tooltip("�^�[�������Ă��邩")] bool _isTurn ;

    [Header("Gravity")]
    [SerializeField] float _gravityLevel;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        Enemy1Move();
    }


    /// <summary>�G�P�̓���</summary>
    void Enemy1Move()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);

        if (!_isTurn)
        {
            _rb.velocity = new Vector3(_moveSpeed, 0);
        }

        if (!GroundRay(_rightRayPos) && !_isTurn)
        {
            StartCoroutine(EnemyTurn());
        }

        if (!GroundRay(_leftRayPos) && !_isTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }


    /// <summary>Raycast�ɂ��ڒn����</summary>
    bool GroundRay(Transform rayPos)
    {
        Vector2 start = transform.position;

        Vector2 vec = rayPos.position - transform.position;
        RaycastHit2D hit = Physics2D.Linecast(start, start + vec, _groundLayer);
        Debug.DrawLine(start, start + vec);

        return hit.collider;  //Ray�̔����Ԃ�
    }


    IEnumerator EnemyTurn()
    {
        _isTurn = true;
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(_turnTime / 2);

        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        yield return new WaitForSeconds(_turnTime / 2);

        _isTurn = false;
    }
}
