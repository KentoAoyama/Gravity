using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField, Tooltip("プレイヤーの重力がある座標")] Vector2 _playerGravity;
    [SerializeField, Tooltip("重力の大きさ")] float _gravityLevel = 15;

   
    [Header("RayCast")]
    [SerializeField, Tooltip("右下に撃つRaycastの座標")] Transform _underR;
    [SerializeField, Tooltip("左下に撃つRaycastの座標")] Transform _underL;
    [SerializeField, Tooltip("Rayが当たるレイヤー")] LayerMask _groundLayer = default;

    
    [Header("Rotate")]
    [SerializeField, Tooltip("回転のスピード")] float _rotationSpeed = 0.6f;
    [SerializeField, Tooltip("回転中の横移動のスピード")] float _rMoveSpeed = 10f;
    
    [Tooltip("右下に回転しているか")] bool _isRotateR = false;
    [Tooltip("左下に回転しているか")] bool _isRotateL = false;
    public bool IsRotate => _isRotateR || _isRotateL;
    
    [SerializeField, Tooltip("回転中の回転数")] float _rotate;
    [SerializeField, Tooltip("現在の回転数の保存用")] float _currentRotate;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _playerGravity = transform.up * _gravityLevel * -1;//プレイヤーの下方向に重力をかける
        Physics2D.gravity = _playerGravity;//常に重力を固定

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


    /// <summary>重力を下から右に変更</summary>
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
