using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Tooltip("移動のスピード")] float _moveSpeed = 20;   
    
    [Tooltip("x軸の入力判定")]float _h;
    [Tooltip("y軸の入力判定")] float _v;
    [Tooltip("接地の判定")] bool _onGround;
    
    Rigidbody2D _rb;

    GravityController _gc;


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
        if (!_gc.IsRotate && _onGround) //回転していない、かつ接地している時移動できる
        {
            Move();
        }
    }


    /// <summary>移動の処理</summary>
    void Move()
    {
        if (transform.up.y > 0.5) //プレイヤーが上下どちらを向いているか
        {
            MoveX(1);
        }
        else if (transform.up.y < -0.5)
        {
            MoveX(-1);
        }
        
        if (transform.up.x > 0.5) //プレイヤーが左右どちらを向いているか
        {
            MoveY(-1);
        }
        else if (transform.up.x < -0.5)
        {
            MoveY(1);
        }
    }


    /// <summary>プレイヤーの横移動の処理　(1)なら重力は下で(-1)なら上</summary>
    void MoveX(float x)
    {
        _rb.AddForce(transform.right * _moveSpeed * _h * x, ForceMode2D.Force);
    }


    /// <summary>プレイヤーの縦移動の処理　(-1)なら重力は右で(1)なら左</summary>
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
}
