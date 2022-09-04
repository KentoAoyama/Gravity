using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShootMk2 : MonoBehaviour
{
    [SerializeField, Tooltip("弾のプレハブ")] GameObject _bullet;
    [SerializeField, Tooltip("ビームのオブジェクト")] GameObject _beam;
    [SerializeField, Tooltip("射撃を行う場所")] Transform _muzzle;
    [SerializeField, Tooltip("射撃のインターバル")] float _shootInterval = 0.2f;

    float _timer;

    GameObject _player;
    GameObject _playerSprite;
    FriendMoveMk2 _friendMove;


    void Start()
    {
        _beam.SetActive(false);
        _player = GameObject.Find("Player");
        _playerSprite = FindObjectOfType<PlayerFlip>().gameObject;
        _friendMove = FindObjectOfType<FriendMoveMk2>().GetComponent<FriendMoveMk2>();
    }


    void FixedUpdate()
    {
        FriendRotateChange();
        BulletShoot();
    }


    void FriendRotateChange()
    {
        if (_friendMove.FriendState != FriendMoveMk2.FriendMoveState.Shoot)
        {
            transform.right = _playerSprite.transform.right;
        }
        else
        {
            transform.right = transform.position - _player.transform.position;
        }
    }


    /// <summary>弾の射撃を行う処理</summary>
    void BulletShoot()
    {
        if (_friendMove.FriendState == FriendMoveMk2.FriendMoveState.Shoot)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0;
        }

        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            _friendMove.FriendState = FriendMoveMk2.FriendMoveState.Shoot;

            if (_timer > _shootInterval)
            {
                Instantiate(_bullet, _muzzle.position, transform.rotation);
                _friendMove.ShootTimer = 0;
                _timer = 0;
            }
        }
    }
}
