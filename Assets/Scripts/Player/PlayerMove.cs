using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Tooltip("�ړ��̃X�s�[�h")] float _moveSpeed = 20;

    [Tooltip("�ڒn�̔���")] bool _onGround;
    [Tooltip("x���̓��͔���")] float _h;
    [Tooltip("y���̓��͔���")] float _v;
    /// <summary>x���̓��͔���</summary>
    public float MoveH => _h;
    /// <summary>y���̓��͔���</summary>
    public float MoveV => _v;

    [Tooltip("�E�Ɖ��̏ꍇ�̈ړ�����")] public const float _rightAndDown = 1;
    [Tooltip("���Ə�̏ꍇ�̈ړ�����")] public const float _leftAndUp = -1;

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


    /// <summary>�ړ��̏���</summary>
    void Move()
    {
        if (!_gc.IsRotate && _onGround) //��]���Ă��Ȃ��A���ڒn���Ă��鎞�ړ��ł���
        {
            if (transform.up.y > 0.5) //�v���C���[���㉺�ǂ���������Ă��邩
            {
                MoveX(_rightAndDown);
                _pgs = PlayerGravityState.Down;
            }
            else if (transform.up.y < -0.5)
            {
                MoveX(_leftAndUp);
                _pgs = PlayerGravityState.Up;
            }
            if (transform.up.x > 0.5) //�v���C���[�����E�ǂ���������Ă��邩
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


    /// <summary>�v���C���[�̉��ړ�</summary>
    /// <param name="x">(1)�Ȃ�d�͉͂���(-1)�Ȃ��</param>
    void MoveX(float x)
    {
        _rb.AddForce(transform.right * _moveSpeed * _h * x, ForceMode2D.Force);
    }


    /// <summary>�v���C���[�̏c�ړ�</summary>
    /// <param name="y">(-1)�Ȃ�d�͉͂E��(1)�Ȃ獶</param>
    void MoveY(float y)
    {
        _rb.AddForce(transform.right * _moveSpeed * _v * y, ForceMode2D.Force);
    }


    void OnCollisionStay2D(Collision2D collision) //�ڒn����
    {
        _onGround = true;
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        _onGround = false;
    }


    /// <summary>�v���C���[�̏d�͂��ǂ̕������\��enum</summary>
    public enum PlayerGravityState
    {
        Up,
        Down,
        Right,
        Left
    }
}
