using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShoot : MonoBehaviour
{
    [SerializeField, Tooltip("弾のプレハブ")] GameObject _bullet;
    [SerializeField, Tooltip("射撃を行う場所")] Transform _muzzle;
    [SerializeField, Tooltip("射撃のインターバル")] float _shootInterval = 0.2f;
    float _timer;

    GameObject _player;
    GameObject _playerSprite;
    FriendMove _friendMove;

    Vector3 _defaultScale;



    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerSprite = FindObjectOfType<PlayerFlip>().gameObject;
        _friendMove = FindObjectOfType<FriendMove>().GetComponent<FriendMove>();

        _defaultScale = transform.localScale;  //開始時点のスケールを保存
    }


    void FixedUpdate()
    {
        if (_friendMove)
        {
            FriendStateMove();
        }
        
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            BulletShoot();
        }
    }


    /// <summary>Stateに応じた向きの処理</summary>
    void FriendStateMove()
    {
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            transform.localScale = _defaultScale;  　　　　　　　　　　　　　　 //向きをデフォルトの値に戻す
            transform.right = MousePosManager.MousePos() - transform.position;　//マウスのポジションに向ける
        }
        else if (_friendMove.FriendState == FriendMove.FriendMoveState.Stay)
        {
            transform.localScale = _playerSprite.transform.localScale;  //プレイヤーと向きと傾きを合わせる
            transform.right = _playerSprite.transform.right;
        }
        else
        {
            transform.right = _playerSprite.transform.right;　//プレイヤーと傾きを合わせる
        }
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
    }
}
