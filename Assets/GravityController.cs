using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Tooltip("�v���C���[�̏d�͂�������W"), SerializeField] Vector2 _playerGravity;
    [Tooltip("�d�͂̑傫��"), SerializeField] float _gravityLevel = 15;

    [Tooltip("�E���Ɍ���Raycast�̍��W"),SerializeField] Transform _underR;
    [Tooltip("�����Ɍ���Raycast�̍��W"), SerializeField] Transform _underL;
    [Tooltip("Ray�������郌�C���["), SerializeField] LayerMask _groundLayer = default;

    [Tooltip("��]�̃X�s�[�h"), SerializeField] float _rotationSpeed = 0.6f;
    [Tooltip("��]���̉��ړ��̃X�s�[�h"), SerializeField] float _moveSpeed = 10f;
    
    /// <summary>�E���ɉ�]���Ă��邩</summary>
    bool _isRotateR = false;
    /// <summary>�����ɉ�]���Ă��邩</summary>
    bool _isRotateL = false;
    /// <summary>��]���Ă��邩</summary>
    public bool IsRotate
    {
        get
        {
            return _isRotateR || _isRotateL; 
        }
    }

    [Tooltip("��]���̉�]��"), SerializeField] float _rotate;
    [Tooltip("���݂̉�]��"), SerializeField] float _currentRotate;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _playerGravity = transform.up * _gravityLevel * -1;//�v���C���[�̉������ɏd�͂�������
        Physics2D.gravity = _playerGravity;//��ɏd�͂��Œ�

        if (!IsRotate)
        {
            GravityRaycast();

            _rotate = _currentRotate;
        }      
        else if (_isRotateR)
        {
            DownToLeft(ref _currentRotate);
        }       
        else if (_isRotateL)
        {
            DownToRight(ref _currentRotate);
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
    void  DownToLeft(ref float currentRotation)
    {
        float rotationAngle = _currentRotate - 90;
        
        if (_rotate >= rotationAngle)
        {
            _rb.AddForce(transform.right * _moveSpeed, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(0, 0, _rotate);
            _rotate -= _rotationSpeed;
        }
        else
        {
            currentRotation = _rotate;
            _isRotateR = false;
        }
    }


    /// <summary>�d�͂�������E�ɕύX</summary>
    void DownToRight(ref float currentRotation)
    {
        float rotationAngle = _currentRotate + 90;
        
        if (_rotate <= rotationAngle)
        {
            _rb.AddForce(transform.right * _moveSpeed * -1, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(0, 0, _rotate);
            _rotate += _rotationSpeed;
        }
        else
        {
            currentRotation = _rotate;
            _isRotateL = false;
        }
    }
}
