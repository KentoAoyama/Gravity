using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public static Vector2 _upGravity = new (0, 10);
    public static Vector2 _downGravity = new (0, -10);
    public static Vector2 _leftGravity = new (-10, 0);
    public static Vector2 _rightGravity = new(10, 0);

    [Tooltip("右下に撃つRayの座標"),SerializeField] Vector2 _lineForGroundR = new (0.5f, -2f);
    //[Tooltip("左下に撃つRayの座標"), SerializeField] Vector2 _lineForGroundL = new (-0.5f, -2f);
    [Tooltip("Rayが当たるレイヤー"), SerializeField] LayerMask _groundLayer = default;

    public PlayerGravity _playerGravity = PlayerGravity.Down;

    bool _isRotate = false;

    GameObject _player;


    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

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
        Vector2 start = this.transform.position;
        
        Debug.DrawLine(start, start + _lineForGroundR);
        RaycastHit2D underRightHit = Physics2D.Linecast(start, start + _lineForGroundR, _groundLayer);

        //Debug.DrawLine(start, start + _lineForGroundL);
        //RaycastHit2D underLeftHit = Physics2D.Linecast(start, start + _lineForGroundL, _groundLayer);


        if (!underRightHit.collider)
        {
            StartCoroutine(DownToLeft());
        }
    }


    IEnumerator DownToLeft()
    {
        _isRotate = true;

        _player.transform.rotation = Quaternion.Euler(0, 0, -90);

        _playerGravity = PlayerGravity.Right;
        yield return new WaitForSeconds(0.2f);
        
        _playerGravity = PlayerGravity.Down;
        yield return new WaitForSeconds(0.2f);
        
        _playerGravity = PlayerGravity.Left;
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
