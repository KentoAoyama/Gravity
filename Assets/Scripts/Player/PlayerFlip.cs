using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    PlayerMove _playerMove;
    GravityController _gc;

    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
        _gc = FindObjectOfType<GravityController>().GetComponent<GravityController>();
    }

    void FixedUpdate()
    {
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
            ChangeScale(_playerMove.MoveH, PlayerMove.RIGHT_AND_DOWN);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Up)
        {
            ChangeScale(_playerMove.MoveH, PlayerMove.LEFT_AND_UP);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Left)
        {
            ChangeScale(_playerMove.MoveV, PlayerMove.LEFT_AND_UP);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Right)
        {
            ChangeScale(_playerMove.MoveV, PlayerMove.RIGHT_AND_DOWN);
        }
    }


    /// <summary>Stateごとに受け付ける入力を変更する</summary>
    /// <param name="playerMove">受けつけるplayerの入力</param>
    /// <param name="dir">１か-１で上下左右の状態を判定</param>
    void ChangeScale(float playerMove, float dir)
    {
        if (playerMove > 0)
        {
            transform.localScale = new Vector3(dir, transform.localScale.y);
        }
        else if (playerMove < 0)
        {
            transform.localScale = new Vector3(dir * -1, transform.localScale.y);
        }
    }
}
