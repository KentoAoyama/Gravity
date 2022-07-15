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
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        if (_onGround)
        {
            Move();
        }
    }


    /// <summary>ÉvÉåÉCÉÑÅ[ÇÃà⁄ìÆÇÃèàóù</summary>
    void MoveX(float x)
    {
        _rb.AddForce(transform.right * _moveSpeed * _h * x, ForceMode2D.Force);
    }


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


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _onGround = false;
        }
    }
}
