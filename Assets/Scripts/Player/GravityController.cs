using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField, Tooltip("�v���C���[�̏d�͂�������W")] Vector2 _playerGravity;
    [SerializeField, Tooltip("�d�͂̑傫��")] float _gravityLevel = 15;


    [Header("RayCast")]
    [SerializeField, Tooltip("�E���Ɍ���Raycast�̍��W")] Transform _underR;
    [SerializeField, Tooltip("�����Ɍ���Raycast�̍��W")] Transform _underL;
    [SerializeField, Tooltip("�E��Ɍ���Raycast�̍��W")] Transform _upR;
    [SerializeField, Tooltip("����Ɍ���Raycast�̍��W")] Transform _upL;
    [SerializeField, Tooltip("Ray�������郌�C���[")] LayerMask _groundLayer;


    [Header("Rotate")]
    [SerializeField, Tooltip("��]�̃X�s�[�h")] float _rotationSpeed = 0.6f;
    [SerializeField, Tooltip("��]���̉��ړ��̃X�s�[�h")] float _rMoveSpeed = 10f;
    [SerializeField, Tooltip("�㏸��]���̉��ړ��̃X�s�[�h")] float _rUpMoveSpeed = 10f;
    [SerializeField, Tooltip("��]���̏c�ړ��̃X�s�[�h")] float _rUpSpeed = 10f;

    [Tooltip("�E���ɉ�]���Ă��邩")] bool _isRotateDR = false;
    [Tooltip("�����ɉ�]���Ă��邩")] bool _isRotateDL = false;
    [Tooltip("�E��ɉ�]���Ă��邩")] bool _isRotateUR = false;
    [Tooltip("����ɉ�]���Ă��邩")] bool _isRotateUL = false;

    public bool IsRotate => _isRotateDR || _isRotateDL || _isRotateUR || _isRotateUL;

    [Tooltip("���݂̉�]���̕ۑ��p")] float _currentRotate;
    [Tooltip("��]���̉�]��")] public float _rotate;

    [Tooltip("�E���ւ̈ړ�����")] public const float _moveRight = 1;
    [Tooltip("�����ւ̈ړ�����")] public const float _moveLeft = -1;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (!IsRotate)  //��]���łȂ����
        {
            GravityRaycast();

            _currentRotate = _rotate;  //���݂̉�]����ۑ�
        }
    }


    void FixedUpdate()
    {
        _playerGravity = transform.up * _gravityLevel * -1; //�v���C���[�̉������ɏd�͂�������
        Physics2D.gravity = _playerGravity;                 //��ɏd�͂��Œ�

        if (_isRotateDR)
        {
            _rotate = DownToRight();  //�ړ��̏������̉�]����Ԃ�
        }       
        
        if (_isRotateDL)
        {
            _rotate = DownToLeft();
        }
        
        if (_isRotateUR)
        {
            _rotate = UpToRight();
        }
       
        if (_isRotateUL)
        {
            _rotate = UpToLeft();
        }
    }


    /// <summary>Raycast���΂�</summary>
    void GravityRaycast()
    {
        if (!Raycast(_underR))        //�E����Raycast
        {
            _isRotateDR = true;
        }
        else if (!Raycast(_underL))   //������Raycast
        {
            _isRotateDL = true;
        }

        if (Raycast(_upR))       //�E���Raycast
        {
            _isRotateUR = true;
        }
        else if (Raycast(_upL))  //�����Raycast
        {
            _isRotateUL = true;
        }
    }

    /// <summary>Raycast�ɂ��ڒn����</summary>
    bool Raycast(Transform rayPos)
    {
        Vector2 start = transform.position;

        Vector2 vec = rayPos.position - transform.position;
        RaycastHit2D hit = Physics2D.Linecast(start, start + vec, _groundLayer); 
        Debug.DrawLine(start, start + vec);

        return hit.collider;  //Ray�̔����Ԃ�
    }

    
    /// <summary>�E���ɍ~���</summary>
    float  DownToRight()
    {
        Debug.Log("DownRight");
        float rotationAngle = _currentRotate - 90;  //���݂̊p�x����-90
        float correctAngle = _rotate % 90;

        if (_rotate >= rotationAngle)  //��]��90�x�ȉ��Ȃ�
        {
            MoveRotateD(1);
        }
        else  //��]���I�������
        {
            AngleCorrection(correctAngle);

            _isRotateDR = false;
        }
        
        return _rotate; //��]����Ԃ�
    }


    /// <summary>�����ɍ~���</summary>
    float DownToLeft()
    {
        Debug.Log("DownLeft");
        float rotationAngle = _currentRotate + 90;  //���݂̊p�x����+90
        float correctAngle = _rotate % 90;

        if (_rotate <= rotationAngle)
        {
            MoveRotateD(-1);
        }
        else
        {
            AngleCorrection(correctAngle);

            _isRotateDL = false;
        }
        
        return _rotate;
    }

    
    /// <summary>�v���C���[�̉������ւ̉�]�ړ�</summary>
    float MoveRotateD(float rotateD)
    {
        _rb.AddForce(transform.right * _rMoveSpeed * rotateD, ForceMode2D.Force);  //�~�������ɗ͂�������
        transform.rotation = Quaternion.Euler(0, 0, _rotate);                      //��]���ɉ����Č�����ύX
        _rotate -= _rotationSpeed * rotateD;                                       //��]�̑��x���l������������

        return _rotate;
    }

    
    /// <summary>�E��ɏオ��</summary>
    float UpToRight()
    {
        Debug.Log("UpRight");
        float rotationAngle = _currentRotate + 90;  //���݂̊p�x����+90
        float correctAngle = _rotate % 90;

        if (_rotate <= rotationAngle)  //��]��90�x�ȉ��Ȃ�
        {
            MoveRotateU(1);
        }
        else  //��]���I�������
        {
            AngleCorrection(correctAngle);

            _isRotateUR = false;
        }
        
        return _rotate;
    }


    /// <summary>����ɏオ��</summary>
    float UpToLeft()
    {
        Debug.Log("UpLeft");
        float rotationAngle = _currentRotate - 90;  //���݂̊p�x����-90
        float correctAngle = _rotate % 90;

        if (_rotate >= rotationAngle)
        {
            MoveRotateU(-1);
        }
        else
        {
            AngleCorrection(correctAngle);

            _isRotateUL = false;
        }
        
        return _rotate;
    }


    /// <summary>�v���C���[�̏�����ւ̉�]�ړ��@(1)�Ȃ�E��(-1)�Ȃ獶</summary>
    float MoveRotateU(float rotateD)
    {
        _rb.AddForce(transform.right * _rUpMoveSpeed * rotateD, ForceMode2D.Force);  //�������ɗ͂�������
        _rb.AddForce(transform.up * _rUpSpeed, ForceMode2D.Force);                   //�c�����ɗ͂�������
        transform.rotation = Quaternion.Euler(0, 0, _rotate);                        //��]���ɉ����Č�����ύX
        _rotate += _rotationSpeed * rotateD;                                         //��]�̑��x���l������������

        return _rotate;
    }


    /// <summary>�p�x�̏C��</summary>
    void AngleCorrection(float correctAngle)
    {
        if (correctAngle > 0)
        {
            _rotate = correctAngle > 45 ? _rotate += 90 - correctAngle : _rotate -= correctAngle;
        }
        else
        {
            _rotate = correctAngle < -45 ? _rotate -= 90 + correctAngle : _rotate -= correctAngle;
        }
        transform.rotation = Quaternion.Euler(0, 0, _rotate);
    }
}
