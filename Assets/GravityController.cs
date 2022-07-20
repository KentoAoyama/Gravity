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
    [SerializeField, Tooltip("Ray�������郌�C���[")] LayerMask _groundLayer = default;

    
    [Header("Rotate")]
    [SerializeField, Tooltip("��]�̃X�s�[�h")] float _rotationSpeed = 0.6f;
    [SerializeField, Tooltip("��]���̉��ړ��̃X�s�[�h")] float _rMoveSpeed = 10f;
    
    [Tooltip("�E���ɉ�]���Ă��邩")] bool _isRotateR = false;
    [Tooltip("�����ɉ�]���Ă��邩")] bool _isRotateL = false;
    public bool IsRotate => _isRotateR || _isRotateL;
    
    [SerializeField, Tooltip("��]���̉�]��")] float _rotate;
    [SerializeField, Tooltip("���݂̉�]���̕ۑ��p")] float _currentRotate;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _playerGravity = transform.up * _gravityLevel * -1;//�v���C���[�̉������ɏd�͂�������
        Physics2D.gravity = _playerGravity;//��ɏd�͂��Œ�

        if (!IsRotate)
        {
            GravityRaycast();

            _currentRotate = _rotate;
        }      
        else if (_isRotateR)
        {
            _rotate = DownToLeft();
        }       
        else if (_isRotateL)
        {
            _rotate = DownToRight();
        }
    }


    /// <summary>Raycast���΂�����</summary>
    void GravityRaycast()
    {       
        if (!Raycast(_underR))//�E����Raycast
        {
            _isRotateR = true;
        }
        else if (!Raycast(_underL))//������Raycast
        {
            _isRotateL = true;
        }
    }


    /// <summary>Raycast�ɂ��ڒn����</summary>
    bool Raycast(Transform rayPos)
    {
        Vector2 start = this.transform.position;

        Vector2 vec = rayPos.position - this.transform.position;
        RaycastHit2D hit = Physics2D.Linecast(start, start + vec, _groundLayer);
        Debug.DrawLine(start, start + vec);

        return hit.collider;
    }

    
    /// <summary>�d�͂������獶�ɕύX</summary>
    float  DownToLeft()
    {
        float rotationAngle = _currentRotate - 90;
        
        if (_rotate >= rotationAngle)
        {
            _rb.AddForce(transform.right * _rMoveSpeed, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(0, 0, _rotate);
            _rotate -= _rotationSpeed;
        }
        else
        {
            _isRotateR = false;
        }
        return _rotate;
    }


    /// <summary>�d�͂�������E�ɕύX</summary>
    float DownToRight()
    {
        float rotationAngle = _currentRotate + 90;
        
        if (_rotate <= rotationAngle)
        {
            _rb.AddForce(transform.right * _rMoveSpeed * -1, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(0, 0, _rotate);
            _rotate += _rotationSpeed;
        }
        else
        {
            _isRotateL = false;
        }
        return _rotate;
    }

    
    void MoveRotate()
    {

    }
}
