using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendStateStay : MonoBehaviour
{
    [Tooltip("�J�n���̃X�P�[��")] Vector3 _defaultScale;
    [Tooltip("�J�n���̌���")] Vector3 _defaultRotate;

    GameObject _player;
    PlayerMove _playerMove;
    FriendMoveArrow _friendMove;
    GravityController _gravityController;


    void Start()
    {
        _player = GameObject.Find("Player");
        _playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
        _friendMove = FindObjectOfType<FriendMoveArrow>().GetComponent<FriendMoveArrow>();
        _gravityController = FindObjectOfType<GravityController>().GetComponent<GravityController>();

        _defaultScale = transform.localScale;
        _defaultRotate = transform.right;
    }


    void FixedUpdate()
    {
        if (_friendMove.FriendState == FriendMoveArrow.FriendMoveState.Stay && _playerMove.OnGround)
        {
            FriendRotateChange();
        }
        else if (_friendMove.FriendState == FriendMoveArrow.FriendMoveState.Shoot)
        {
            transform.localScale = _defaultScale;
        }
    }


    /// <summary>Friend�̌�����ύX����</summary>
    void FriendRotateChange()
    {
        if (_playerMove.PGS == PlayerMove.PlayerGravityState.Up || _playerMove.PGS == PlayerMove.PlayerGravityState.Down)
        {
            if (transform.position.x < _player.transform.position.x)
            {
                transform.right = _defaultRotate;
            }
            else
            {
                transform.right = -_defaultRotate;
            }
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Left)
        {
            transform.right = _defaultRotate;
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Right)
        {
            transform.right = -_defaultRotate;
        }
    }
}
