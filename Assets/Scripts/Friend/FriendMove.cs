using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove : MonoBehaviour
{
    [Header("Move")]
    [SerializeField, Tooltip("�v���C���[�Ƃ̋���")] float _friendDis;
    [SerializeField, Tooltip("�ړ��̑��x")] float _moveSpeed;
    [SerializeField, Tooltip("�ړ����~�܂鋗��")] float _stopDis;


    [Header("StayState")]
    [SerializeField, Tooltip("��]�̔��a")] float _amplitude = 1f;
    [SerializeField, Tooltip("X���̉�]�̑��x")] float _speedX = 3f;
    [SerializeField, Tooltip("Y���̉�]�̑��x")] float _speedY = 1f;
    [Tooltip("StayState�ɂȂ��Ă��鎞��")] float _stayTimer;


    [Header("Shoot")]
    [SerializeField, Tooltip("ShootState���ێ�����鎞��")] float _shootTimeLimit = 5;
    [SerializeField, Tooltip("ShootState����Y���W�̈ʒu")] float _shootPosUp = 5;
    [Tooltip("ShootState�ɂȂ��Ă��鎞��")] float _shootTimer;

    GameObject _player;

    PlayerMove _pm;

    FriendMoveState _friendState;
    public FriendMoveState FriendState => _friendState; 


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _pm = _player.GetComponent<PlayerMove>();
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


    /// <summary>Friend��State���Ƃ̏���</summary>
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


    /// <summary>State��Stay�̎��̈ړ��̏���</summary>
    void MoveStay()
    {
        _stayTimer += Time.deltaTime;
        float posX = Mathf.Sin(_stayTimer * _speedX) * _amplitude;
        float posY = Mathf.Cos(_stayTimer * _speedY) * _amplitude;

        Vector3 position = _player.transform.position + _player.transform.up + new Vector3(posX, posY);
        transform.position = position;
    }


    /// <summary>Friend���ڕW�̒n�_�Ɉړ����鏈��</summary>
    void MovePos(Vector3 targetPos, FriendMoveState friendMoveState)
    {
        if (Vector2.Distance(transform.position, targetPos) > _stopDis)  //�ڕW�̒n�_�ɓ��B����܂�
        {
            Vector2 dir = targetPos - transform.position;�@�@�@�@�@�@�@�@//�ړ��̌���
            transform.Translate(dir * _moveSpeed * Time.deltaTime);�@�@  //�ړ������鏈��
        }
        else
        {
            ChangeState(friendMoveState);�@//State�ύX
        }
    }


    /// <summary>Friend���ˌ��̒n�_���v�Z���鏈��</summary>
    Vector3 ShootPos()
    {
        Vector3 posUp = new();

        if (_pm.PGS == PlayerMove.PlayerGravityState.Down)
        {
            posUp = new (0, _shootPosUp, 0);
        }
        else if (_pm.PGS == PlayerMove.PlayerGravityState.Up)
        {
            posUp = new(0, -_shootPosUp, 0);
        }
        else if (_pm.PGS == PlayerMove.PlayerGravityState.Left)
        {
            posUp = new(_shootPosUp, 0, 0);
        }
        else if (_pm.PGS == PlayerMove.PlayerGravityState.Right)
        {
            posUp = new(-_shootPosUp, 0, 0);
        }
        
        Vector3 ShootPos = MousePosManager.MousePos() - _player.transform.position;  �@�@ //�v���C���[����}�E�X�ւ̌���
        Vector3 movePos = _player.transform.position + ShootPos.normalized * _friendDis + posUp;  //Friend���ˌ����s���ꏊ

        return movePos;
    }


    /// <summary>State��ύX����</summary>
    void ChangeState(FriendMoveState friendMoveState)
    {
        _friendState = friendMoveState;
    }


    /// <summary>Friend�̈ړ��̏�Ԃ��Ǘ�����enum</summary>
    public enum FriendMoveState
    {
        /// <summary>�v���C���[�̎���ɂ�����</summary>
        Stay,
        /// <summary>���̏ꏊ�ɖ߂��Ă�����</summary>
        Back,
        /// <summary>�ˌ������Ă�����</summary>
        Shoot,
    }
}
