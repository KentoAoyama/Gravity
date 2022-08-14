using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove : MonoBehaviour
{
    [SerializeField] float _friendDis;
    [SerializeField] Vector3 _pos;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _stopDis;
    [SerializeField] float _amplitudeX = 1.5f;
    [SerializeField] float _amplitudeY = 1.5f;
    [SerializeField] float _speedX = 3f;
    [SerializeField] float _speedY = 1f;
    [SerializeField] float _movePositionY = 1;

    float _stayTimer;

    GameObject _player;
    GravityController _gravityController;

    [SerializeField] FriendMoveState _friendMove;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _gravityController = _player.GetComponent<GravityController>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_friendMove == FriendMoveState.Stay || _friendMove == FriendMoveState.Back)
            {
                ChangeState(FriendMoveState.Go);
            }

            if (_friendMove == FriendMoveState.Shoot)
            {
                ChangeState(FriendMoveState.Back);
            }
        }
    }


    void FixedUpdate()
    {
        FriendStateProcess();
    }


    /// <summary>FriendのStateごとの処理</summary>
    void FriendStateProcess()
    {
        if (_friendMove == FriendMoveState.Stay)
        {
            MoveStay();
        }
        else if (_friendMove == FriendMoveState.Go || _friendMove == FriendMoveState.Shoot)
        {
            MovePos(ShootPos(), FriendMoveState.Shoot);
        }
        else if (_friendMove == FriendMoveState.Back)
        {
            MovePos(_pos + _player.transform.position, FriendMoveState.Stay);
        }
    }


    /// <summary>StateがStayの時の移動の処理</summary>
    void MoveStay()
    {
        _stayTimer += Time.deltaTime;
        float posX = Mathf.Sin(_stayTimer * _speedX) * _amplitudeX;
        float posY = Mathf.Cos(_stayTimer * _speedY) * _amplitudeY;

        Vector3 position = _player.transform.position + _player.transform.up + new Vector3(posX, posY);
        transform.position = position;

        _pos = transform.position - _player.transform.position;
    }


    /// <summary>Friendが射撃の地点を計算する処理</summary>
    Vector3 ShootPos()
    {
        Vector3 ShootPos = GetMousePos() - _player.transform.position;  　　　　　　　　　//プレイヤーからマウスへの向き
        Vector3 movePos = _player.transform.position + ShootPos.normalized * _friendDis;  //Friendが射撃を行う場所

        return movePos;
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


    /// <summary>マウスの座標を取得する</summary>
    Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;  　　　　　　 //マウス座標を取得
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);  //カメラ座標に変換
        mousePos.z = 0;                                       //Z軸だけ修正

        return mousePos;
    }


    /// <summary>Stateを変更する</summary>
    void ChangeState(FriendMoveState friendMoveState)
    {
        _friendMove = friendMoveState;
    }


    /// <summary>Friendの移動の状態を管理するenum</summary>
    enum FriendMoveState
    {
        /// <summary>プレイヤーの周りにいる状態</summary>
        Stay,
        /// <summary>射撃の場所に移動している状態</summary>
        Go,
        /// <summary>元の場所に戻っている状態</summary>
        Back,
        /// <summary>射撃をしている状態</summary>
        Shoot,
    }
}
