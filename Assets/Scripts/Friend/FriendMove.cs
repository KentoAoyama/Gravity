using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove : MonoBehaviour
{
    [Header("Move")]
    [SerializeField, Tooltip("プレイヤーとの距離")] float _friendDis;
    [SerializeField, Tooltip("移動の速度")] float _moveSpeed;
    [SerializeField, Tooltip("移動が止まる距離")] float _stopDis;


    [Header("StayState")]
    [SerializeField, Tooltip("回転の半径")] float _amplitude = 1f;
    [SerializeField, Tooltip("X軸の回転の速度")] float _speedX = 3f;
    [SerializeField, Tooltip("Y軸の回転の速度")] float _speedY = 1f;
    [Tooltip("StayStateになっている時間")] float _stayTimer;


    [Header("Shoot")]
    [SerializeField, Tooltip("ShootStateが維持される時間")] float _shootTimeLimit = 5;
    [SerializeField, Tooltip("ShootState中のY座標の位置")] float _shootPosUp = 5;
    [Tooltip("ShootStateになっている時間")] float _shootTimer;

    GameObject _player;

    PlayerMove _playerMove;

    FriendMoveState _friendState;
    public FriendMoveState FriendState => _friendState; 


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMove = _player.GetComponent<PlayerMove>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            FireMove();
        }
    }


    void FixedUpdate()
    {
        FriendStateProcess();

        if (_shootTimer > _shootTimeLimit)
        {
            _shootTimer = 0;
            ChangeState(FriendMoveState.Back);
        }
    }


    /// <summary>FriendのStateごとの移動</summary>
    void FireMove()
    {
        if (_friendState == FriendMoveState.Stay || _friendState == FriendMoveState.Back)
        {
            ChangeState(FriendMoveState.Shoot);
        }

        if (_friendState == FriendMoveState.Shoot)
        {
            _shootTimer = 0;
        }
    }


    /// <summary>FriendのStateごとの処理</summary>
    void FriendStateProcess()
    {
        if (_friendState == FriendMoveState.Stay)
        {
            MoveStay();
        }
        else if (_friendState == FriendMoveState.Shoot)
        {
            _shootTimer += Time.deltaTime;
            MovePos(ShootPos(), FriendMoveState.Shoot);
        }
        else if (_friendState == FriendMoveState.Back)
        {
            _stayTimer = 0;
            　　　　 //プレイヤーの少し上の座標に移動する
            MovePos(_player.transform.position + _player.transform.up + new Vector3(0, _amplitude), FriendMoveState.Stay);
        }
    }


    /// <summary>StateがStayの時の移動の処理</summary>
    void MoveStay()
    {
        _stayTimer += Time.deltaTime;

        //円を描くように移動する
        float posX = Mathf.Sin(_stayTimer * _speedX) * _amplitude;
        float posY = Mathf.Cos(_stayTimer * _speedY) * _amplitude;　

        
        Vector3 position = _player.transform.position + _player.transform.up + new Vector3(posX, posY);
        transform.position = position;
    }


    /// <summary>Friendが目標の地点に移動する処理</summary>
    void MovePos(Vector3 targetPos, FriendMoveState friendMoveState)
    {
        //目標の地点に到達するまで
        if (Vector2.Distance(transform.position, targetPos) > _stopDis)  
        {
            //移動の向き
            Vector2 dir = targetPos - transform.position;
            //移動させる処理
            transform.Translate(dir * _moveSpeed * Time.deltaTime);　　  
        }
        else
        {
            //State変更
            ChangeState(friendMoveState);　
        }
    }


    /// <summary>Friendが射撃の地点を計算する処理</summary>
    Vector3 ShootPos()
    {
        Vector3 posUp = new();

        //重力の向きに応じてプレイヤーの少し上に行くようにする
        if (_playerMove.PGS == PlayerMove.PlayerGravityState.Down)  
        {
            posUp = new (0, _shootPosUp, 0);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Up)
        {
            posUp = new(0, -_shootPosUp, 0);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Left)
        {
            posUp = new(_shootPosUp, 0, 0);
        }
        else if (_playerMove.PGS == PlayerMove.PlayerGravityState.Right)
        {
            posUp = new(-_shootPosUp, 0, 0);
        }

        //プレイヤーからマウスへの向き
        Vector3 ShootPos = MousePosManager.MousePos() - _player.transform.position;
        //Friendが射撃を行う場所
        Vector3 movePos = _player.transform.position + ShootPos.normalized * _friendDis + posUp;  

        return movePos;
    }


    /// <summary>Stateを変更する</summary>
    void ChangeState(FriendMoveState friendMoveState)
    {
        _friendState = friendMoveState;
    }


    /// <summary>Friendの移動の状態を管理するenum</summary>
    public enum FriendMoveState
    {
        /// <summary>プレイヤーの周りにいる状態</summary>
        Stay,
        /// <summary>プレイヤーの場所に戻っている状態</summary>
        Back,
        /// <summary>射撃をしている状態</summary>
        Shoot,
    }
}
