using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMoveArrow : MonoBehaviour
{
    [Header("ChangeMove")]
    [SerializeField, Tooltip("�v���C���[�Ƃ̋���")] float _friendDis;
    [SerializeField, Tooltip("�ړ��̑��x")] float _moveSpeed;
    [SerializeField, Tooltip("�ړ����~�܂鋗��")] float _goStopDis;


    [Header("StayState")]
    [SerializeField, Tooltip("��]�̔��a")] float _amplitude = 1f;
    [SerializeField, Tooltip("X���̉�]�̑��x")] float _staySpeed = 3f;
    [Tooltip("StayState�ɂȂ��Ă��鎞��")] float _stayTimer;
    [SerializeField, Tooltip("StayState���̏ꏊ")] Transform _stayPos;
    [SerializeField, Tooltip("StayState���̈ړ����~�܂鋗��")] float _stayStopDis;



    [Header("Shoot")]
    [SerializeField, Tooltip("ShootState���ێ�����鎞��")] float _shootTimeLimit = 5;
    [Tooltip("ShootState�ɂȂ��Ă��鎞��")] float _shootTimer;
    [Tooltip("�d���ˌ���")] bool _isShootStop;
    [SerializeField, Tooltip("ShootState���̈ړ����~�܂鋗��")] float _shootStopDis;


    /// <summary>�ˌ����s���Ă��鎞�Ԃ𑪂�ϐ�</summary>
    public float ShootTimer { get => _shootTimer; set => _shootTimer = value; }

    /// <summary>�d���ˌ��������Q�Ƃł���v���p�e�B</summary>
    public bool IsShootStop => _isShootStop;


    [Tooltip("���͂̒l��ۑ����Ă������߂̕ϐ�x")] float _x;
    [Tooltip("���͂̒l��ۑ����Ă������߂̕ϐ�y")] float _y;

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


    /// <summary>�d�͂̏�Ԃɉ�����Friend�̓�����ς��鏈��</summary>
    void FriendGravityProcess()
    {
        if (_playerMove.MoveH != 0 || _playerMove.MoveV != 0)
        {
            _x = _playerMove.MoveH;
            _y = _playerMove.MoveV;
        }
    }


    /// <summary>Friend��State���Ƃ̏���</summary>
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


    /// <summary>State��Stay�̎��̈ړ��̏���</summary>
    Vector3 MoveStayWave()
    {
        _stayTimer += Time.deltaTime;

        float wave = Mathf.Sin(_stayTimer * _staySpeed) * _amplitude;

        Vector3 position = new(0, wave);

        return position;
    }


    /// <summary>Friend���ڕW�̒n�_�Ɉړ����鏈��</summary>
    void MovePos(Vector3 targetPos, FriendMoveState friendMoveState, float stopDis)
    {
        //�ڕW�̒n�_�ɓ��B����܂�
        if (Vector2.Distance(transform.position, targetPos) > stopDis)
        {
            //�ړ��̌���
            Vector2 dir = targetPos - transform.position;
            //�ړ������鏈��
            transform.Translate(_moveSpeed * Time.deltaTime * dir);
        }
        else
        {
            ChangeState(friendMoveState);
        }
    }


    /// <summary>Friend���ˌ��̒n�_���v�Z���鏈��</summary>
    public Vector3 ShootPos()
    {
        Vector3 pos = new(_x, _y);

        //Friend���ˌ����s���ꏊ
        Vector3 movePos = _player.transform.position + pos.normalized * _friendDis;

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
        /// <summary>�ˌ������Ă�����</summary>
        Shoot,
        /// <summary>Stay�̍��W�Ɉړ����Ă�����</summary>
        Go,
        /// <summary>Shoot�̍��W�Ɉړ����Ă�����</summary>
        Back,
        /// <summary>Beam�ˌ���</summary>
        Beam
    }
}
