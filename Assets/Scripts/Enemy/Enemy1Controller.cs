using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField, Tooltip("右側のRayを飛ばす場所")] Transform _rightRayPos;
    [SerializeField, Tooltip("左側のRayを飛ばす場所")] Transform _leftRayPos;
    [SerializeField, Tooltip("Rayが当たるレイヤー")] LayerMask _groundLayer;

    [Header("Move")]
    [SerializeField, Tooltip("移動の速度")] float _moveSpeed = 10;
    [SerializeField, Tooltip("力を加える間隔")] float _impulseInterval = 1;
    [SerializeField, Tooltip("ターンにかかる時間")] float _turnTime = 1;
    [Tooltip("ターンをしているか")] bool _isTurn ;

    [Header("Gravity")]
    [SerializeField, Tooltip("重力の大きさ")] float _gravityLevel = 20;

    float _dir = 1;
    float _timer;

    SpriteRenderer _targetRenderer;
    Rigidbody2D _rb;


    void Start()
    {
        _targetRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    
    void FixedUpdate()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);

        if (_targetRenderer.isVisible)
        {
            Enemy1Move();
        }
    }


    /// <summary>敵１の動き</summary>
    void Enemy1Move()
    {

        if (!_isTurn)
        {
            _timer += Time.deltaTime;

            if (_timer > _impulseInterval)
            {
                _rb.AddForce(transform.right * _moveSpeed * _dir, ForceMode2D.Impulse);
                _timer = 0;
            }
        }

        if (!GroundRay(_rightRayPos) && !_isTurn || !GroundRay(_leftRayPos) && !_isTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }


    /// <summary>Raycastによる接地判定</summary>
    bool GroundRay(Transform rayPos)
    {
        Vector2 start = transform.position;

        Vector2 vec = rayPos.position - transform.position;
        RaycastHit2D hit = Physics2D.Linecast(start, start + vec, _groundLayer);
        Debug.DrawLine(start, start + vec);

        return hit.collider;  //Rayの判定を返す
    }


    IEnumerator EnemyTurn()
    {
        _isTurn = true;
        _dir *= -1;
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(_turnTime / 2);

        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        yield return new WaitForSeconds(_turnTime / 2);

        _rb.AddForce(transform.right * _moveSpeed * _dir, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_impulseInterval);
        
        _timer = _impulseInterval;
        _isTurn = false;
    }
}
