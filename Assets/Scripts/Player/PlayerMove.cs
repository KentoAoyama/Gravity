using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Tooltip("移動のスピード")] float _moveSpeed = 20;

    [Tooltip("接地の判定")] bool _onGround;
    [Tooltip("x軸の入力判定")] float _h;
    [Tooltip("y軸の入力判定")] float _v;
    /// <summary>x軸の入力判定</summary>
    public float MoveH => _h;
    /// <summary>y軸の入力判定</summary>
    public float MoveV => _v;

    [Tooltip("右と下の場合の移動方向")] public const float _rightAndDown = 1;
    [Tooltip("左と上の場合の移動方向")] public const float _leftAndUp = -1;

    Rigidbody2D _rb;
    GravityController _gc;

    PlayerGravityState _pgs;
    public PlayerGravityState PGS => _pgs;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gc = GetComponent<GravityController>();
    }


    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }


    void FixedUpdate()
    {
            Move();
    }


    /// <summary>移動の処理</summary>
    void Move()
    {
        if (!_gc.IsRotate && _onGround) //回転していない、かつ接地している時移動できる
        {
            if (transform.up.y > 0.5) //プレイヤーが上下どちらを向いているか
            {
                MoveX(_rightAndDown);
                _pgs = PlayerGravityState.Down;
            }
            else if (transform.up.y < -0.5)
            {
                MoveX(_leftAndUp);
                _pgs = PlayerGravityState.Up;
            }
            if (transform.up.x > 0.5) //プレイヤーが左右どちらを向いているか
            {
                MoveY(_leftAndUp);
                _pgs = PlayerGravityState.Left;
            }
            else if (transform.up.x < -0.5)
            {
                MoveY(_rightAndDown);
                _pgs = PlayerGravityState.Right;
            }
        }
    }


    /// <summary>プレイヤーの横移動</summary>
    /// <param name="x">(1)なら重力は下で(-1)なら上</param>
    void MoveX(float x)
    {
        _rb.AddForce(transform.right * _moveSpeed * _h * x, ForceMode2D.Force);
    }


    /// <summary>プレイヤーの縦移動</summary>
    /// <param name="y">(-1)なら重力は右で(1)なら左</param>
    void MoveY(float y)
    {
        _rb.AddForce(transform.right * _moveSpeed * _v * y, ForceMode2D.Force);
    }


    void OnCollisionStay2D(Collision2D collision) //接地判定
    {
        _onGround = true;
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        _onGround = false;
    }


    /// <summary>プレイヤーの重力がどの方向か表すenum</summary>
    public enum PlayerGravityState
    {
        Up,
        Down,
        Right,
        Left
    }
}
