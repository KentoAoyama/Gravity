using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Tooltip("プレイヤーの重力がある座標"), SerializeField] Vector2 _playerGravity;
    [Tooltip("重力の大きさ"), SerializeField] float _gravityLevel = 15;

    [Tooltip("右下に撃つRaycastの座標"),SerializeField] Transform _underR;
    [Tooltip("左下に撃つRaycastの座標"), SerializeField] Transform _underL;
    [Tooltip("Rayが当たるレイヤー"), SerializeField] LayerMask _groundLayer = default;

    [Tooltip("回転のスピード"), SerializeField] float _rotationSpeed = 0.6f;
    [Tooltip("回転中の横移動のスピード"), SerializeField] float _moveSpeed = 10f;
    
    /// <summary>右下に回転しているか</summary>
    bool _isRotateR = false;
    /// <summary>左下に回転しているか</summary>
    bool _isRotateL = false;
    /// <summary>回転しているか</summary>
    public bool IsRotate
    {
        get
        {
            return _isRotateR || _isRotateL; 
        }
    }

    [Tooltip("回転中の回転数"), SerializeField] float _rotate;
    [Tooltip("現在の回転数"), SerializeField] float _currentRotate;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _playerGravity = transform.up * _gravityLevel * -1;//プレイヤーの下方向に重力をかける
        Physics2D.gravity = _playerGravity;//常に重力を固定

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


    /// <summary>Raycastを飛ばす処理</summary>
    void GravityRaycast()
    {       
        if (!Raycast(_underR))//右下のRaycast
        {
            _isRotateR = true;
        }
        else if (!Raycast(_underL))//左下のRaycast
        {
            _isRotateL = true;
        }
    }


    /// <summary>Raycastによる接地判定</summary>
    bool Raycast(Transform rayPos)
    {
        Vector2 start = this.transform.position;

        Vector2 vec = rayPos.position - this.transform.position;
        RaycastHit2D hit = Physics2D.Linecast(start, start + vec, _groundLayer);
        Debug.DrawLine(start, start + vec);

        return hit.collider;
    }

    
    /// <summary>重力を下から左に変更</summary>
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


    /// <summary>重力を下から右に変更</summary>
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
