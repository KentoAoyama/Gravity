using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyBase
{
    [Header("Raycast")]
    [SerializeField, Tooltip("右側のRayを飛ばす場所")] Transform _rightRayPos;
    [SerializeField, Tooltip("右上側のRayを飛ばす場所")] Transform _uRightRayPos;
    [SerializeField, Tooltip("左側のRayを飛ばす場所")] Transform _leftRayPos;
    [SerializeField, Tooltip("左上側のRayを飛ばす場所")] Transform _uLeftRayPos;
    [SerializeField, Tooltip("Rayが当たるレイヤー")] LayerMask _groundLayer;

    [Header("Move")]
    [SerializeField, Tooltip("移動の速度")] float _moveSpeed = 10f;
    [SerializeField, Tooltip("発見時に加速させる速度")] float _upSpeed = 10f;
    [SerializeField, Tooltip("力を加える間隔")] float _impulseInterval = 1f;
    [SerializeField, Tooltip("ターンにかかる時間")] float _turnTime = 1f;
    [Tooltip("ターンをしているか")] bool _isTurn ;

    [Header("Gravity")]
    [SerializeField, Tooltip("重力の大きさ")] float _gravityLevel = 20f;

    [Header("Damage")]
    [SerializeField, Tooltip("プレイヤーに与えるダメージ")] int _playerDamage = 10;
    [SerializeField, Tooltip("自分が食らうダメージ")] int _damage = 1;

    int _dir = 1;
    float _timer;

    Rigidbody2D _rb;   


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.gravityScale = 0;
        _timer = _impulseInterval;
    }


    void FixedUpdate()
    {        
        Warning();
    }
    

    /// <summary>敵１の動き</summary>
    public override void Move()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);　//常に自分から見て下に力を加える

        //ダメージを受けたら動きを止める
        if (_isDamage)
        {
            _rb.velocity = Vector2.zero;
            _timer = 0;
        }
        //通常の移動の処理
        else if (!_isTurn)
        {
            _timer += Time.deltaTime;
            
            if (_timer > _impulseInterval)
            {
                _rb.AddForce(transform.right * _moveSpeed * _dir, ForceMode2D.Impulse);　//一定の間隔で力を加える
                _timer = 0;
            }
        }
        else
        {
            _timer = 0;
        }

        //Rayが地面にあたっておらず、ターン中でなければターンをする
        if (!GroundRay(_rightRayPos) && !_isTurn || !GroundRay(_leftRayPos) && !_isTurn ||
            GroundRay(_uRightRayPos) && !_isTurn || GroundRay(_uLeftRayPos) && !_isTurn)
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
        yield return new WaitForSeconds(_turnTime / 2);  //移動の向きを逆にしてvelocityをzeroにする

        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
        yield return new WaitForSeconds(_turnTime / 2);　//向きを反転させる

        _rb.AddForce(transform.right * _moveSpeed * _dir, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_impulseInterval);　//反転した方向に移動させる
        
        _timer = _impulseInterval;
        _isTurn = false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage))
        {
            addDamage.AddDamage(_playerDamage);
            AddDamage(_damage);
        }
    }


    void Warning()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _warningDis)
        {
            WarningMove();
            _isWarning = true;
        }
    }


    void WarningMove()
    {
        if (!_isWarning)
        {
           
            _moveSpeed += _upSpeed;
        }
    }


    public override void Damage()
    {
        _rb.velocity = Vector2.zero;
    }
}
