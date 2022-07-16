using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public static Vector2 _upGravity = new (0, 10);
    public static Vector2 _downGravity = new (0, -10);
    public static Vector2 _leftGravity = new (-10, 0);
    public static Vector2 _rightGravity = new(10, 0);

    [Tooltip("右下に撃つRayの座標"),SerializeField] Transform _underR;
    [Tooltip("左下に撃つRayの座標"), SerializeField] Transform _underL;
    [Tooltip("Rayが当たるレイヤー"), SerializeField] LayerMask _groundLayer = default;

    public PlayerGravity _playerGravity = PlayerGravity.Down;

    bool _isRotate = false;

    float _rotate;
    
    
    void Update()
    {
        ChangeGravity();

        if (!_isRotate)
        {
            GravityRaycast();
        }
    }

    
    void ChangeGravity()
    {
        if (_playerGravity == PlayerGravity.Up)
        {
            Physics2D.gravity = _upGravity;
        }

        if (_playerGravity == PlayerGravity.Down)
        {
            Physics2D.gravity = _downGravity;
        }

        if (_playerGravity == PlayerGravity.Left)
        {
            Physics2D.gravity = _leftGravity;
        }

        if (_playerGravity == PlayerGravity.Right)
        {
            Physics2D.gravity = _rightGravity;
        }
    }


    /// <summary>Raycastを飛ばす処理</summary>
    void GravityRaycast()
    {       
        if (!UnderRayCast(_underR))
        {
            _isRotate = true;

            StartCoroutine(DownToLeft()); 
        }
        else if (!UnderRayCast(_underL))
        {
            _isRotate = true;

            StartCoroutine(LeftToDown());
        }
    }

    
    /// <summary>Raycastによる接地判定</summary>
    bool UnderRayCast(Transform _underPos)
    {
        Vector2 start = this.transform.position;

        Vector2 vec = _underPos.position - transform.position;
        RaycastHit2D underHit = Physics2D.Linecast(start, start + vec, _groundLayer);
        Debug.DrawLine(start, start + vec);

        return underHit.collider;
    }


    /// <summary>重力を下から右に変更</summary>
    IEnumerator DownToLeft()
    {
        _playerGravity = PlayerGravity.Up;
        yield return new WaitForSeconds(0.1f);

        _playerGravity = PlayerGravity.Right;
        yield return new WaitForSeconds(0.15f);

        transform.rotation = Quaternion.Euler(0, 0, _rotate -= 90);

        _playerGravity = PlayerGravity.Down;
        yield return new WaitForSeconds(0.3f);
        
        _playerGravity = PlayerGravity.Left;
    }

    
    /// <summary>重力を左から下に変更</summary>
    IEnumerator LeftToDown()
    {        
        _playerGravity = PlayerGravity.Right;
        yield return new WaitForSeconds(0.1f);

        _playerGravity = PlayerGravity.Up;
        yield return new WaitForSeconds(0.15f);

        transform.rotation = Quaternion.Euler(0, 0, _rotate += 90);

        _playerGravity = PlayerGravity.Left;
        yield return new WaitForSeconds(0.3f);

        _playerGravity = PlayerGravity.Down;
    }

    void OnCollisionEnter2D(Collision2D collision)//接地したら回転を終了とする
    {
        _isRotate = false;
    }


    /// <summary>プレイヤーにかかっている重力</summary>
    public enum PlayerGravity
    {
        /// <summary>上に重力がかかる状態</summary>
        Up,

        /// <summary>下に重力がかかる状態</summary>
        Down,

        /// <summary>左に重力がかかる状態</summary>
        Left,

        /// <summary>右に重力がかかる状態</summary>
        Right,
    }
}
