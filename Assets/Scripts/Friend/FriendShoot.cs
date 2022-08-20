using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShoot : MonoBehaviour
{    
    GameObject _playerHeart;
    GameObject _player;
    FriendMove _friendMove;

    Vector3 _defaultScale;
    

    void Start()
    {
        _playerHeart = GameObject.FindGameObjectWithTag("Player");
        _player = FindObjectOfType<PlayerFlip>().gameObject;
        _friendMove = FindObjectOfType<FriendMove>().GetComponent<FriendMove>();

        _defaultScale = transform.localScale;
    }


    void FixedUpdate()
    {       
        if (_friendMove)
        {
            FriendShootRotate();
        }

        BulletShoot();
    }


    void FriendShootRotate()
    {
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            transform.localScale = _defaultScale;
            transform.right = _friendMove.GetMousePos() - _playerHeart.transform.position;
        }
        else if (_friendMove.FriendState == FriendMove.FriendMoveState.Stay)
        {
            transform.localScale = _player.transform.localScale;
        }
        else
        {
            transform.right = _player.transform.right;
        }
    }


    void BulletShoot()
    {
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot && Input.GetButton("Fire1"))
        {

        }
    }
}
