using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    PlayerMove _playerMove;

    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
    }

    void FixedUpdate()
    {
        if (_playerMove)
        {
            Flip();
        }
    }


    void Flip()
    {
        if (_playerMove.PGS == PlayerMove.PlayerGravityState.Down)
        {
            ChangeScale(_playerMove.MoveH, PlayerMove._rightAndDown);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Up)
        {
            ChangeScale(_playerMove.MoveH, PlayerMove._leftAndUp);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Left)
        {
            ChangeScale(_playerMove.MoveV, PlayerMove._leftAndUp);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Right)
        {
            ChangeScale(_playerMove.MoveV, PlayerMove._rightAndDown);
        }
    }


    /// <summary>State‚²‚Æ‚Éó‚¯•t‚¯‚é“ü—Í‚ğ•ÏX‚·‚é</summary>
    /// <param name="playerMove">ó‚¯‚Â‚¯‚éplayer‚Ì“ü—Í</param>
    /// <param name="dir">‚P‚©-‚P‚Åã‰º¶‰E‚Ìó‘Ô‚ğ”»’è</param>
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
