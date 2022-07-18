using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("�ړ��̃X�s�[�h"), SerializeField] float _moveSpeed = 20;   
    Rigidbody2D _rb;
    float _h;
    float _v;
    bool _onGround;

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
        if (!_gc.IsRotate && _onGround)
        {
            Move();
        }
    }

    void Move()
    {

    }

    /// <summary>�v���C���[�̉��ړ��̏���</summary>
    void MoveX()
    {
        _rb.AddForce(transform.right * _moveSpeed * _h, ForceMode2D.Force);
    }

    /// <summary>�v���C���[�̏c�ړ��̏���</summary>
    void MoveY()
    {
        _rb.AddForce(transform.right * _moveSpeed * _v * -1, ForceMode2D.Force);
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
