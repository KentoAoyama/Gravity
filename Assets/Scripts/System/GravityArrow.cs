using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityArrow : MonoBehaviour
{
    PlayerMove _playerMove;


    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
    }

    
    void FixedUpdate()
    {
        Vector3 changeScale = new ();

        if (_playerMove.PGS == PlayerMove.PlayerGravityState.Up)
        {
            changeScale.z = 0;
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Down)
        {
            changeScale.z = 180;
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Right)
        {
            changeScale.z = -90;
        }
        else
        {
            changeScale.z = 90;
        }

        transform.localRotation = Quaternion.Euler(changeScale);
    }
}
