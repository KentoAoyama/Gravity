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


    /// <summary>Sprite�̌�������͂ɉ����ĕύX����</summary>
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


    /// <summary>State���ƂɎ󂯕t������͂�ύX����</summary>
    /// <param name="playerMove">�󂯂���player�̓���</param>
    /// <param name="dir">�P��-�P�ŏ㉺���E�̏�Ԃ𔻒�</param>
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
