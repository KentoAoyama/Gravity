using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove : MonoBehaviour
{
    [SerializeField] float _friendDis;
    [SerializeField] Vector2 _pos;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _stopDis;


    GameObject _player;
    Animator _animator;

    bool _isShoot;

    FriendMoveState _friendMove;


    void Start()
    {        
        _player = GameObject.FindGameObjectWithTag("Player");

        _animator = GetComponent<Animator>();
        _animator.enabled = true;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_isShoot)
            {
                _pos = transform.position;
            }           
            
            _isShoot = !_isShoot;
            _animator.enabled = !_animator.enabled;
        }       
    }


    void FixedUpdate()
    {
        if (_friendMove == FriendMoveState.Move)
        {
            MoveShootPos();
        }
        else
        {
            MovePos(_pos);

            
        }
    }


    void MoveShootPos()
    {
        Vector3 ShootPos = GetMousePos() - _player.transform.position;  　　　　　　　　　//プレイヤーからマウスへの向き
        Vector3 movePos = _player.transform.position + ShootPos.normalized * _friendDis;  //Friendが射撃を行う場所

        MovePos(movePos);
    }


    void MovePos(Vector3 targetPos)
    {
        if (Vector2.Distance(transform.position, targetPos) > _stopDis)  //目標の地点に到達するまで
        {
            Vector2 dir = targetPos - transform.position;　　　　　　　　//移動の向き
            transform.Translate(dir * _moveSpeed * Time.deltaTime);　　//移動させる処理
        }
        else
        {
            transform.position = targetPos;　//座標を固定
        }

    }


    Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;  　　　　　　 //マウス座標を取得
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);  //カメラ座標に変換
        mousePos.z = 0;                                       //Z軸だけ修正

        return mousePos;
    }


    enum FriendMoveState
    {
        Stay,
        Move,
        Back,
        Shoot,
    }
}
