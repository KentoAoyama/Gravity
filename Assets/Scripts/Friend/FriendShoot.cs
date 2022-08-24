using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShoot : MonoBehaviour
{
    [SerializeField, Tooltip("弾のプレハブ")] GameObject _bullet;
    [SerializeField, Tooltip("射撃を行う場所")] Transform _muzzle;
    [SerializeField, Tooltip("射撃のインターバル")] float _shootInterval = 0.2f;
    float _timer;

    GameObject _playerHeart;
    GameObject _player;
    FriendMove _friendMove;

    Vector3 _defaultScale;



    void Start()
    {
        _playerHeart = GameObject.FindGameObjectWithTag("Player");
        _player = FindObjectOfType<PlayerFlip>().gameObject;
        _friendMove = FindObjectOfType<FriendMove>().GetComponent<FriendMove>();

        _defaultScale = transform.localScale;  //開始時点のスケールを保存
    }


    void FixedUpdate()
    {
        if (_friendMove)
        {
            FriendShootRotate();
        }
        
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            BulletShoot();
        }
    }


    /// <summary>Stateに応じた向きの処理</summary>
    void FriendShootRotate()
    {
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            transform.localScale = _defaultScale;  　　　　　　　　　　　　　　　　　　　　  //向きをデフォルトの値に戻す
            transform.right = MousePosManager.MousePos() - _playerHeart.transform.position;　//マウスのポジションに向ける
        }
        else if (_friendMove.FriendState == FriendMove.FriendMoveState.Stay)
        {
            transform.localScale = _player.transform.localScale;  //プレイヤーと向きと傾きを合わせる
            transform.right = _player.transform.right;
        }
        else
        {
            transform.right = _player.transform.right;　//プレイヤーと傾きを合わせる
        }
    }


    /// <summary>弾の射撃を行う処理</summary>
    void BulletShoot()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer > _shootInterval)
        {
            _timer = 0;
            Instantiate(_bullet, _muzzle.position, transform.rotation);
        }
    }
}
