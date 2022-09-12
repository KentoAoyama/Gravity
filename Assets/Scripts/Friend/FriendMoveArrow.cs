using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMoveArrow : MonoBehaviour
{
    [Header("ChangeMove")]
    [SerializeField, Tooltip("プレイヤーとの距離")] float _friendDis;
    [SerializeField, Tooltip("移動の速度")] float _moveSpeed;
    [SerializeField, Tooltip("移動が止まる距離")] float _goStopDis;


    [Header("StayState")]
    [SerializeField, Tooltip("回転の半径")] float _amplitude = 1f;
    [SerializeField, Tooltip("X軸の回転の速度")] float _staySpeed = 3f;
    [Tooltip("StayStateになっている時間")] float _stayTimer;
    [SerializeField, Tooltip("StayState時の場所")] Transform _stayPos;
    [SerializeField, Tooltip("StayState中の移動が止まる距離")] float _stayStopDis;



    [Header("Shoot")]
    [SerializeField, Tooltip("ShootStateが維持される時間")] float _shootTimeLimit = 5;
    [Tooltip("ShootStateになっている時間")] float _shootTimer;
    [Tooltip("硬直射撃中")] bool _isShootStop;
    [SerializeField, Tooltip("ShootState中の移動が止まる距離")] float _shootStopDis;


    /// <summary>射撃を行っている時間を測る変数</summary>
    public float ShootTimer { get => _shootTimer; set => _shootTimer = value; }

    /// <summary>硬直射撃中かを参照できるプロパティ</summary>
    public bool IsShootStop => _isShootStop;


    [Tooltip("入力の値を保存しておくための変数x")] float _x;
    [Tooltip("入力の値を保存しておくための変数y")] float _y;

    GameObject _player;

    PlayerMove _playerMove;

    FriendMoveState _friendState;
    public FriendMoveState FriendState { get => _friendState; set => _friendState = value; }


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMove = _player.GetComponent<PlayerMove>();
    }


    void Update()
    {
        FireInput();
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


    void FireInput()
    {
        FriendGravityProcess();

        if (Input.GetButton("Fire2"))
        {
            _isShootStop = true;
        }
        else
        {
            _isShootStop = false;
        }
    }


    /// <summary>重力の状態に応じてFriendの動きを変える処理</summary>
    void FriendGravityProcess()
    {
        if (_playerMove.MoveH != 0 || _playerMove.MoveV != 0)
        {
            _x = _playerMove.MoveH;
            _y = _playerMove.MoveV;
        }
    }


    /// <summary>FriendのStateごとの処理</summary>
    void FriendStateProcess()
    {
        if (_friendState == FriendMoveState.Stay)
        {
            MovePos(_stayPos.position + MoveStayWave(), FriendMoveState.Stay, _stayStopDis);
        }
        else if (_friendState == FriendMoveState.Shoot)
        {
            _shootTimer += Time.deltaTime;
            MovePos(ShootPos(), FriendMoveState.Shoot, _shootStopDis);
        }
        else if (_friendState == FriendMoveState.Go)
        {
            MovePos(ShootPos(), FriendMoveState.Shoot, _goStopDis);
        }
        else if (_friendState == FriendMoveState.Back)
        {
            MovePos(_stayPos.position, FriendMoveState.Stay, _goStopDis);
        }
    }


    /// <summary>StateがStayの時の移動の処理</summary>
    Vector3 MoveStayWave()
    {
        _stayTimer += Time.deltaTime;

        float wave = Mathf.Sin(_stayTimer * _staySpeed) * _amplitude;

        Vector3 position = new(0, wave);

        return position;
    }


    /// <summary>Friendが目標の地点に移動する処理</summary>
    void MovePos(Vector3 targetPos, FriendMoveState friendMoveState, float stopDis)
    {
        //目標の地点に到達するまで
        if (Vector2.Distance(transform.position, targetPos) > stopDis)
        {
            //移動の向き
            Vector2 dir = targetPos - transform.position;
            //移動させる処理
            transform.Translate(_moveSpeed * Time.deltaTime * dir);
        }
        else
        {
            ChangeState(friendMoveState);
        }
    }


    /// <summary>Friendが射撃の地点を計算する処理</summary>
    public Vector3 ShootPos()
    {
        Vector3 pos = new(_x, _y);

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
        /// <summary>射撃をしている状態</summary>
        Shoot,
        /// <summary>Stayの座標に移動している状態</summary>
        Go,
        /// <summary>Shootの座標に移動している状態</summary>
        Back,
        /// <summary>Beam射撃中</summary>
        Beam
    }
}
