using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendStateStay : MonoBehaviour
{
    [Tooltip("�J�n���̃X�P�[��")] Vector3 _defaultScale;
    [Tooltip("�J�n���̌���")] Vector3 _defaultRotate;

    GameObject _player;
    FriendMoveArrow _friendMove;
    GravityController _gravityController;


    void Start()
    {
        _player = GameObject.Find("Player");
        _friendMove = FindObjectOfType<FriendMoveArrow>().GetComponent<FriendMoveArrow>();
        _gravityController = _player.GetComponent<GravityController>();

        _defaultScale = transform.localScale;
        _defaultRotate = transform.right;
    }

    
    void FixedUpdate()
    {
        if (_gravityController.IsRotate)
        {
            _friendMove.FriendState = FriendMoveArrow.FriendMoveState.Stay;
        }

        if (_friendMove.FriendState == FriendMoveArrow.FriendMoveState.Stay)
        {
            transform.right = _defaultRotate;
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
        if (transform.position.x < _player.transform.position.x)
        {
            transform.localScale = _defaultScale;
        }
        else
        {
            transform.localScale = new(-_defaultScale.x, _defaultScale.y, _defaultScale.z);
        }
    }
}
