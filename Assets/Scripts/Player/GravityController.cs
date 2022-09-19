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

    [Tooltip("���݂̉�]���̕ۑ��p")]public  float _currentRotate;
    [Tooltip("��]���̉�]��")]public  float _rotate;

    [Tooltip("�E���ւ̈ړ�����")] const int MOVE_RIGHT = 1;
    [Tooltip("�����ւ̈ړ�����")] const int MOVE_LEFT = -1;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //��]���łȂ����
        if (!IsRotate)  
        {
            GravityRaycast();

            //���݂̉�]����ۑ�
            _currentRotate = _rotate;  
        }
    }


    void FixedUpdate()
    {
        //�v���C���[�̉������ɏd�͂�������
        _playerGravity = transform.up * _gravityLevel * -1;
        //��ɏd�͂��Œ�
        Physics2D.gravity = _playerGravity;                 

        if (_isRotateDR)
        {
            //�ړ��̏������̉�]����Ԃ�
            _rotate = DownToRight();  
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
        if (!Raycast(_underR))       //�E����Raycast
        {
            _isRotateDR = true;
        }
        else if (!Raycast(_underL))  //������Raycast
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

        //Ray�̔����Ԃ�
        return hit.collider;  
    }

    
    /// <summary>�E���ɍ~���</summary>
    float  DownToRight()
    {
        //���݂̊p�x����-90
        float rotationAngle = _currentRotate - 90;  
        float correctAngle = _rotate % 90;

        //��]��90�x�ȉ��Ȃ�
        if (_rotate >= rotationAngle)
        {
            MoveRotateD(MOVE_RIGHT);
        }
        else  //��]���I�������
        {
            AngleCorrection(correctAngle);

            _isRotateDR = false;
        }

        //��]����Ԃ�
        return _rotate; 
    }


    /// <summary>�����ɍ~���</summary>
    float DownToLeft()
    {
        //���݂̊p�x����+90
        float rotationAngle = _currentRotate + 90;  
        float correctAngle = _rotate % 90;

        //��]��90�x�ȉ��Ȃ�
        if (_rotate <= rotationAngle)
        {
            MoveRotateD(MOVE_LEFT);
        }
        else  //��]���I�������
        {
            AngleCorrection(correctAngle);

            _isRotateDL = false;
        }

        //��]����Ԃ�
        return _rotate;
    }

    
    /// <summary>�v���C���[�̉������ւ̉�]�ړ�</summary>
    float MoveRotateD(float rotateD)
    {
        //�~�������ɗ͂�������
        _rb.AddForce(transform.right * _rMoveSpeed * rotateD, ForceMode2D.Force);
        //��]���ɉ����Č�����ύX
        transform.rotation = Quaternion.Euler(0, 0, _rotate);
        //��]�̑��x���l������������
        _rotate -= _rotationSpeed * rotateD;

        //��]����Ԃ�
        return _rotate;
    }

    
    /// <summary>�E��ɏオ��</summary>
    float UpToRight()
    {
        //���݂̊p�x����+90
        float rotationAngle = _currentRotate + 90; 
        float correctAngle = _rotate % 90;

        //��]��90�x�ȉ��Ȃ�
        if (_rotate <= rotationAngle)  
        {
            MoveRotateU(MOVE_RIGHT);
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
        //���݂̊p�x����-90
        float rotationAngle = _currentRotate - 90;  
        float correctAngle = _rotate % 90;

        //��]��90�x�ȉ��Ȃ�
        if (_rotate >= rotationAngle)
        {
            MoveRotateU(MOVE_LEFT);
        }
        else  //��]���I�������
        {
            AngleCorrection(correctAngle);

            _isRotateUL = false;
        }
        
        return _rotate;
    }


    /// <summary>�v���C���[�̏�����ւ̉�]�ړ��@(1)�Ȃ�E��(-1)�Ȃ獶</summary>
    float MoveRotateU(float rotateD)
    {
        //�������ɗ͂�������
        _rb.AddForce(transform.right * _rUpMoveSpeed * rotateD, ForceMode2D.Force);
        //�c�����ɗ͂�������
        _rb.AddForce(transform.up * _rUpSpeed, ForceMode2D.Force);
        //��]���ɉ����Č�����ύX
        transform.rotation = Quaternion.Euler(0, 0, _rotate);
        //��]�̑��x���l������������
        _rotate += _rotationSpeed * rotateD;                                         

        //��]����Ԃ�
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
