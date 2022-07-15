using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControllerA : MonoBehaviour
{
    GameObject _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        ChangeGravityArrow();        
    }


    void ChangeGravityArrow()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //PlayerDirection(PlayerGravity.Up);

            Physics2D.gravity = GravityController._upGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //PlayerDirection(PlayerGravity.Down);

            Physics2D.gravity = GravityController._downGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //PlayerDirection(PlayerGravity.Left);

            Physics2D.gravity = GravityController._leftGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //PlayerDirection(PlayerGravity.Right);

            Physics2D.gravity = GravityController._rightGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }


    void PlayerDirection(PlayerGravity gravity)
    {
        if (gravity == PlayerGravity.Up)
        {
            Physics2D.gravity = GravityController._upGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (gravity == PlayerGravity.Down)
        {
            Physics2D.gravity = GravityController._downGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (gravity == PlayerGravity.Left)
        {
            Physics2D.gravity = GravityController._leftGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        if (gravity == PlayerGravity.Right)
        {
            Physics2D.gravity = GravityController._rightGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }


    enum PlayerGravity
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
