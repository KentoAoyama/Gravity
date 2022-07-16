using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _moveSpeed = 20;
    float _h;
    float _v;
    bool _onGround;
    GravityController _gc = null;

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
        if (_onGround)
        {
            Move();
        }
    }


    /// <summary>�v���C���[�̉��ړ��̏���</summary>
    void MoveX(float x)
    {
        _rb.AddForce(transform.right * _moveSpeed * _h * x, ForceMode2D.Force);
    }

    /// <summary>�v���C���[�̏c�ړ��̏���</summary>
    void MoveY(float y)
    {
        _rb.AddForce(transform.right * _moveSpeed * _v * y, ForceMode2D.Force);
    }


    void Move()
    {
        if (Physics2D.gravity == GravityController._downGravity)
        {
            MoveX(1);
        }
        else if (Physics2D.gravity == GravityController._upGravity)
        {
            MoveX(-1);
        }
        else if (Physics2D.gravity == GravityController._rightGravity)
        {
            MoveY(1);
        }
        else if (Physics2D.gravity == GravityController._leftGravity)
        {
            MoveY(-1);
        }
    }


    void OnCollisionStay2D(Collision2D collision)//�ڒn����
    {
        _onGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _onGround = false;
    }
}
