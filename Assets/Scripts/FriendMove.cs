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


    /// <summary>Friend��State���Ƃ̏���</summary>
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


    /// <summary>State��Stay�̎��̈ړ��̏���</summary>
    void MoveStay()
    {
        _stayTimer += Time.deltaTime;
        float posX = Mathf.Sin(_stayTimer * _speedX) * _amplitudeX;
        float posY = Mathf.Cos(_stayTimer * _speedY) * _amplitudeY;

        Vector3 position = _player.transform.position + _player.transform.up + new Vector3(posX, posY);
        transform.position = position;

        _pos = transform.position - _player.transform.position;
    }


    /// <summary>Friend���ˌ��̒n�_���v�Z���鏈��</summary>
    Vector3 ShootPos()
    {
        Vector3 ShootPos = GetMousePos() - _player.transform.position;  �@�@�@�@�@�@�@�@�@//�v���C���[����}�E�X�ւ̌���
        Vector3 movePos = _player.transform.position + ShootPos.normalized * _friendDis;  //Friend���ˌ����s���ꏊ

        return movePos;
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


    /// <summary>�}�E�X�̍��W���擾����</summary>
    Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;  �@�@�@�@�@�@ //�}�E�X���W���擾
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);  //�J�������W�ɕϊ�
        mousePos.z = 0;                                       //Z�������C��

        return mousePos;
    }


    /// <summary>State��ύX����</summary>
    void ChangeState(FriendMoveState friendMoveState)
    {
        _friendMove = friendMoveState;
    }


    /// <summary>Friend�̈ړ��̏�Ԃ��Ǘ�����enum</summary>
    enum FriendMoveState
    {
        /// <summary>�v���C���[�̎���ɂ�����</summary>
        Stay,
        /// <summary>�ˌ��̏ꏊ�Ɉړ����Ă�����</summary>
        Go,
        /// <summary>���̏ꏊ�ɖ߂��Ă�����</summary>
        Back,
        /// <summary>�ˌ������Ă�����</summary>
        Shoot,
    }
}
