using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    Vector3 _defaultScale;

    PlayerMove _playerMove;
    GravityController _gc;

    float _x;
    float _y;


    void Start()
    {
        _defaultScale = transform.localScale;
        _playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
        _gc = FindObjectOfType<GravityController>().GetComponent<GravityController>();
    }

    void Update()
    {
        if (_playerMove.MoveH != 0 || _playerMove.MoveV != 0)
        {
            _x = _playerMove.MoveH;
            _y = _playerMove.MoveV;
        }

        if (_playerMove && !_gc.IsRotate)
        {
            Flip();
        }
    }


    /// <summary>Spriteの向きを入力に応じて変更する</summary>
    void Flip()
    {
        if (_playerMove.PGS == PlayerMove.PlayerGravityState.Down)
        {
            ChangeScale(_x, PlayerMove.RIGHT_AND_DOWN);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Up)
        {
            ChangeScale(_x, PlayerMove.LEFT_AND_UP);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Left)
        {
            ChangeScale(_y, PlayerMove.LEFT_AND_UP);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Right)
        {
            ChangeScale(_y, PlayerMove.RIGHT_AND_DOWN);
        }
    }


    /// <summary>Stateごとに受け付ける入力を変更する</summary>
    /// <param name="playerMove">受けつけるplayerの入力</param>
    /// <param name="dir">１か-１で上下左右の状態を判定</param>
    void ChangeScale(float playerMove, float dir)
    {
        if (playerMove > 0)
        {
            transform.localScale = new Vector3(_defaultScale.x * dir, transform.localScale.y, 1);
        }
        else if (playerMove < 0)
        {
            transform.localScale = new Vector3(_defaultScale.x * dir * -1, transform.localScale.y, 1);
        }
    }
}
