using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Tooltip("移動のスピード")] float _moveSpeed = 20;

    [Tooltip("接地の判定")] bool _onGround;
    [Tooltip("x軸の入力判定")] float _h;
    [Tooltip("y軸の入力判定")] float _v;
    /// <summary>接地の判定</summary>
    public bool OnGround => _onGround;
    /// <summary>x軸の入力判定</summary>
    public float MoveH => _h;
    /// <summary>y軸の入力判定</summary>
    public float MoveV => _v;

    [Tooltip("右と下の場合の移動方向")] public const float RIGHT_AND_DOWN = 1;
    [Tooltip("左と上の場合の移動方向")] public const float LEFT_AND_UP = -1;

    Rigidbody2D _rb;
    GravityController _gc;
    FriendMoveArrow _friendMoveMk2;

    PlayerGravityState _pgs;
    public PlayerGravityState PGS => _pgs;


    /// <summary>プレイヤーの重力がどの方向か表すenum</summary>
    public enum PlayerGravityState
    {
        Up,
        Down,
        Right,
        Left
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gc = GetComponent<GravityController>();
        _friendMoveMk2 = FindObjectOfType<FriendMoveArrow>().GetComponent<FriendMoveArrow>();
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
        //回転していない、かつ接地している時移動できる
        if (!_gc.IsRotate && _onGround)
        {
            //プレイヤーが上下どちらを向いているか
            if (transform.up.y > 0.5)
            {
                MoveX(RIGHT_AND_DOWN);
                _pgs = PlayerGravityState.Down;
            }
            else if (transform.up.y < -0.5)
            {
                MoveX(LEFT_AND_UP);
                _pgs = PlayerGravityState.Up;
            }

            //プレイヤーが左右どちらを向いているか
            if (transform.up.x > 0.5)
            {
                MoveY(LEFT_AND_UP);
                _pgs = PlayerGravityState.Left;
            }
            else if (transform.up.x < -0.5)
            {
                MoveY(RIGHT_AND_DOWN);
                _pgs = PlayerGravityState.Right;
            }
        }
    }


    /// <summary>プレイヤーの横移動</summary>
    /// <param name="x">(1)なら重力は下で(-1)なら上</param>
    void MoveX(float x)
    {
        if (!_friendMoveMk2.IsShootStop)
        {
            _rb.AddForce(transform.right * _moveSpeed * _h * x, ForceMode2D.Force);
        }        
    }


    /// <summary>プレイヤーの縦移動</summary>
    /// <param name="y">(-1)なら重力は右で(1)なら左</param>
    void MoveY(float y)
    {
        if (!_friendMoveMk2.IsShootStop)
        {
            _rb.AddForce(transform.right * _moveSpeed * _v * y, ForceMode2D.Force);
        }
    }


    void OnCollisionStay2D(Collision2D collision) //接地判定
    {
        _onGround = true;
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        _onGround = false;
    }
}
