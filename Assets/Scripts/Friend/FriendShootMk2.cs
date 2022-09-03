using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShootMk2 : MonoBehaviour
{
    [SerializeField, Tooltip("弾のプレハブ")] GameObject _bullet;
    [SerializeField, Tooltip("射撃を行う場所")] Transform _muzzle;
    [SerializeField, Tooltip("射撃のインターバル")] float _shootInterval = 0.2f;
    float _timer;

    GameObject _player;
    FriendMoveMk2 _friendMove;
    Rigidbody2D _playerRigidBody;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _friendMove = FindObjectOfType<FriendMoveMk2>().GetComponent<FriendMoveMk2>();
        _playerRigidBody = _player.GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (_friendMove.FriendState == FriendMoveMk2.FriendMoveState.Shoot)
        {
            FriendRotateChange();
            BulletShoot();
        }
        else
        {
            transform.right = _player.transform.right;
        }
    }


    void FriendRotateChange()
    {
        transform.right = transform.position - _player.transform.position;
    }


    /// <summary>弾の射撃を行う処理</summary>
    void BulletShoot()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer > _shootInterval)
        {
            Instantiate(_bullet, _muzzle.position, transform.rotation);
            _timer = 0;
        }

        if (Input.GetButton("Fire2") && _timer > _shootInterval)
        {
            Instantiate(_bullet, _muzzle.position, transform.rotation);
            _timer = 0;

        }
    }

}
