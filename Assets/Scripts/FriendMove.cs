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
        Vector3 ShootPos = GetMousePos() - _player.transform.position;  �@�@�@�@�@�@�@�@�@//�v���C���[����}�E�X�ւ̌���
        Vector3 movePos = _player.transform.position + ShootPos.normalized * _friendDis;  //Friend���ˌ����s���ꏊ

        MovePos(movePos);
    }


    void MovePos(Vector3 targetPos)
    {
        if (Vector2.Distance(transform.position, targetPos) > _stopDis)  //�ڕW�̒n�_�ɓ��B����܂�
        {
            Vector2 dir = targetPos - transform.position;�@�@�@�@�@�@�@�@//�ړ��̌���
            transform.Translate(dir * _moveSpeed * Time.deltaTime);�@�@//�ړ������鏈��
        }
        else
        {
            transform.position = targetPos;�@//���W���Œ�
        }

    }


    Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;  �@�@�@�@�@�@ //�}�E�X���W���擾
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);  //�J�������W�ɕϊ�
        mousePos.z = 0;                                       //Z�������C��

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
