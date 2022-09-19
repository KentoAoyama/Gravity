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
    [SerializeField, Tooltip("右上に撃つRaycastの座標")] Transform _upR;
    [SerializeField, Tooltip("左上に撃つRaycastの座標")] Transform _upL;
    [SerializeField, Tooltip("Rayが当たるレイヤー")] LayerMask _groundLayer;


    [Header("Rotate")]
    [SerializeField, Tooltip("回転のスピード")] float _rotationSpeed = 0.6f;
    [SerializeField, Tooltip("回転中の横移動のスピード")] float _rMoveSpeed = 10f;
    [SerializeField, Tooltip("上昇回転中の横移動のスピード")] float _rUpMoveSpeed = 10f;
    [SerializeField, Tooltip("回転中の縦移動のスピード")] float _rUpSpeed = 10f;

    [Tooltip("右下に回転しているか")] bool _isRotateDR = false;
    [Tooltip("左下に回転しているか")] bool _isRotateDL = false;
    [Tooltip("右上に回転しているか")] bool _isRotateUR = false;
    [Tooltip("左上に回転しているか")] bool _isRotateUL = false;

    public bool IsRotate => _isRotateDR || _isRotateDL || _isRotateUR || _isRotateUL;

    [Tooltip("現在の回転数の保存用")]public  float _currentRotate;
    [Tooltip("回転中の回転数")]public  float _rotate;

    [Tooltip("右側への移動方向")] const int MOVE_RIGHT = 1;
    [Tooltip("左側への移動方向")] const int MOVE_LEFT = -1;

    Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //回転中でなければ
        if (!IsRotate)  
        {
            GravityRaycast();

            //現在の回転数を保存
            _currentRotate = _rotate;  
        }
    }


    void FixedUpdate()
    {
        //プレイヤーの下方向に重力をかける
        _playerGravity = transform.up * _gravityLevel * -1;
        //常に重力を固定
        Physics2D.gravity = _playerGravity;                 

        if (_isRotateDR)
        {
            //移動の処理中の回転数を返す
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


    /// <summary>Raycastを飛ばす</summary>
    void GravityRaycast()
    {
        if (!Raycast(_underR))       //右下のRaycast
        {
            _isRotateDR = true;
        }
        else if (!Raycast(_underL))  //左下のRaycast
        {
            _isRotateDL = true;
        }

        if (Raycast(_upR))       //右上のRaycast
        {
            _isRotateUR = true;
        }
        else if (Raycast(_upL))  //左上のRaycast
        {
            _isRotateUL = true;
        }
    }

    /// <summary>Raycastによる接地判定</summary>
    bool Raycast(Transform rayPos)
    {
        Vector2 start = transform.position;

        Vector2 vec = rayPos.position - transform.position;
        RaycastHit2D hit = Physics2D.Linecast(start, start + vec, _groundLayer); 
        Debug.DrawLine(start, start + vec);

        //Rayの判定を返す
        return hit.collider;  
    }

    
    /// <summary>右下に降りる</summary>
    float  DownToRight()
    {
        //現在の角度から-90
        float rotationAngle = _currentRotate - 90;  
        float correctAngle = _rotate % 90;

        //回転が90度以下なら
        if (_rotate >= rotationAngle)
        {
            MoveRotateD(MOVE_RIGHT);
        }
        else  //回転が終わったら
        {
            AngleCorrection(correctAngle);

            _isRotateDR = false;
        }

        //回転数を返す
        return _rotate; 
    }


    /// <summary>左下に降りる</summary>
    float DownToLeft()
    {
        //現在の角度から+90
        float rotationAngle = _currentRotate + 90;  
        float correctAngle = _rotate % 90;

        //回転が90度以下なら
        if (_rotate <= rotationAngle)
        {
            MoveRotateD(MOVE_LEFT);
        }
        else  //回転が終わったら
        {
            AngleCorrection(correctAngle);

            _isRotateDL = false;
        }

        //回転数を返す
        return _rotate;
    }

    
    /// <summary>プレイヤーの下方向への回転移動</summary>
    float MoveRotateD(float rotateD)
    {
        //降りる方向に力を加える
        _rb.AddForce(transform.right * _rMoveSpeed * rotateD, ForceMode2D.Force);
        //回転数に応じて向きを変更
        transform.rotation = Quaternion.Euler(0, 0, _rotate);
        //回転の速度分値を加減させる
        _rotate -= _rotationSpeed * rotateD;

        //回転数を返す
        return _rotate;
    }

    
    /// <summary>右上に上がる</summary>
    float UpToRight()
    {
        //現在の角度から+90
        float rotationAngle = _currentRotate + 90; 
        float correctAngle = _rotate % 90;

        //回転が90度以下なら
        if (_rotate <= rotationAngle)  
        {
            MoveRotateU(MOVE_RIGHT);
        }
        else  //回転が終わったら
        {
            AngleCorrection(correctAngle);

            _isRotateUR = false;
        }
        
        return _rotate;
    }


    /// <summary>左上に上がる</summary>
    float UpToLeft()
    {
        //現在の角度から-90
        float rotationAngle = _currentRotate - 90;  
        float correctAngle = _rotate % 90;

        //回転が90度以下なら
        if (_rotate >= rotationAngle)
        {
            MoveRotateU(MOVE_LEFT);
        }
        else  //回転が終わったら
        {
            AngleCorrection(correctAngle);

            _isRotateUL = false;
        }
        
        return _rotate;
    }


    /// <summary>プレイヤーの上方向への回転移動　(1)なら右で(-1)なら左</summary>
    float MoveRotateU(float rotateD)
    {
        //横方向に力を加える
        _rb.AddForce(transform.right * _rUpMoveSpeed * rotateD, ForceMode2D.Force);
        //縦方向に力を加える
        _rb.AddForce(transform.up * _rUpSpeed, ForceMode2D.Force);
        //回転数に応じて向きを変更
        transform.rotation = Quaternion.Euler(0, 0, _rotate);
        //回転の速度分値を加減させる
        _rotate += _rotationSpeed * rotateD;                                         

        //回転数を返す
        return _rotate;
    }


    /// <summary>角度の修正</summary>
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
