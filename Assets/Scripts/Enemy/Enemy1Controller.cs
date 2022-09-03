using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyBase
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

    Rigidbody2D _rb;

    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }


    /// <summary>敵１の動き</summary>
    public override void Move()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);　//常に自分から見て下に力を加える


        if (!_isTurn)
        {
            _timer += Time.deltaTime;

            if (_timer > _impulseInterval)
            {
                _rb.AddForce(transform.right * _moveSpeed * _dir, ForceMode2D.Impulse);　//一定の間隔で力を加える
                _timer = 0;
            }
        }

        if (!GroundRay(_rightRayPos) && !_isTurn || !GroundRay(_leftRayPos) && !_isTurn)
        {
            StartCoroutine(EnemyTurn());　//Rayが地面にあたっておらず、ターン中でなければ
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
        yield return new WaitForSeconds(_turnTime / 2);  //移動の向きを逆にしてvelocityをzeroにする

        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        yield return new WaitForSeconds(_turnTime / 2);　//Spriteを反転させる

        _rb.AddForce(transform.right * _moveSpeed * _dir, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_impulseInterval);　//反転した方向に移動させる
        
        _timer = _impulseInterval;
        _isTurn = false;
    }
}
