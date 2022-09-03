using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMoveMk2 : MonoBehaviour
{
    [Header("ChangeMove")]
    [SerializeField, Tooltip("プレイヤーとの距離")] float _friendDis;
    [SerializeField, Tooltip("移動の速度")] float _moveSpeed;
    [SerializeField, Tooltip("移動が止まる距離")] float _stopDis;


    [Header("StayState")]
    [SerializeField, Tooltip("回転の半径")] float _amplitude = 1f;
    [SerializeField, Tooltip("X軸の回転の速度")] float _staySpeed = 3f;
    [Tooltip("StayStateになっている時間")] float _stayTimer;
    [SerializeField, Tooltip("StayState時の場所")] Transform _stayPos;


    [Header("Shoot")]
    [SerializeField, Tooltip("ShootStateが維持される時間")] float _shootTimeLimit = 5;
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
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
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
            MovePos(_stayPos.position + MoveStayWave(), FriendMoveState.Stay);
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
            MovePos(_stayPos.position, FriendMoveState.Stay);
        }
    }


    /// <summary>StateがStayの時の移動の処理</summary>
    Vector3 MoveStayWave()
    {
        _stayTimer += Time.deltaTime;

        float wave = Mathf.Sin(_stayTimer * _staySpeed) * _amplitude;

        Vector3 position = new (0, wave);

        return position;
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
            transform.Translate(_moveSpeed * Time.deltaTime * dir);
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
        Vector3 pos = new();

        if (_playerMove.MoveH != 0 || _playerMove.MoveV != 0)
        {
            pos = new(_playerMove.MoveH, _playerMove.MoveV);
        }

        //Friendが射撃を行う場所
        Vector3 movePos = _player.transform.position + pos.normalized * _friendDis;

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
