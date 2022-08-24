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
    [Tooltip("ShootStateになっている時間")] float _shootTimer;


    GameObject _player;

    FriendMoveState _friendState;
    public FriendMoveState FriendState => _friendState; 


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
            MovePos(_player.transform.position + _player.transform.up + new Vector3(0, _amplitude), FriendMoveState.Stay);
        }
    }


    /// <summary>StateがStayの時の移動の処理</summary>
    void MoveStay()
    {
        _stayTimer += Time.deltaTime;
        float posX = Mathf.Sin(_stayTimer * _speedX) * _amplitude;
        float posY = Mathf.Cos(_stayTimer * _speedY) * _amplitude;

        Vector3 position = _player.transform.position + _player.transform.up + new Vector3(posX, posY);
        transform.position = position;
    }


    /// <summary>Friendが目標の地点に移動する処理</summary>
    void MovePos(Vector3 targetPos, FriendMoveState friendMoveState)
    {
        if (Vector2.Distance(transform.position, targetPos) > _stopDis)  //目標の地点に到達するまで
        {
            Vector2 dir = targetPos - transform.position;　　　　　　　　//移動の向き
            transform.Translate(dir * _moveSpeed * Time.deltaTime);　　  //移動させる処理
        }
        else
        {
            ChangeState(friendMoveState);　//State変更
        }
    }


    /// <summary>Friendが射撃の地点を計算する処理</summary>
    Vector3 ShootPos()
    {
        Vector3 ShootPos = GetMousePos() - _player.transform.position;  　　　　　　　　　//プレイヤーからマウスへの向き
        Vector3 movePos = _player.transform.position + ShootPos.normalized * _friendDis;  //Friendが射撃を行う場所

        return movePos;
    }


    /// <summary>マウスの座標を取得する</summary>
    public Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;  　　　　　　 //マウス座標を取得
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);  //カメラ座標に変換
        mousePos.z = 0;                                       //Z軸だけ修正

        return mousePos;
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
        /// <summary>元の場所に戻っている状態</summary>
        Back,
        /// <summary>射撃をしている状態</summary>
        Shoot,
    }
}
