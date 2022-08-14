using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShoot : MonoBehaviour
{
    Vector3 _defaultDir;
    
    GameObject _player;
    GameObject _friendHeart;
    FriendMove _friendMove;
    

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _friendHeart = GameObject.Find("FriendHeart");
        _friendMove = _friendHeart.GetComponent<FriendMove>();

        _defaultDir = transform.right;
    }


    void Update()
    {       
        if (_friendHeart)
        {
            FriendShootRotate();
        }
    }


    void FriendShootRotate()
    {
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            transform.right = _friendMove.GetMousePos() - _player.transform.position;
        }
        else
        {
            transform.right = _player.transform.right;
        }
    }
}
