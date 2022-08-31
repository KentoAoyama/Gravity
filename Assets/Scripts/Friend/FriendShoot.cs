using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShoot : MonoBehaviour
{
    [SerializeField, Tooltip("�e�̃v���n�u")] GameObject _bullet;
    [SerializeField, Tooltip("�ˌ����s���ꏊ")] Transform _muzzle;
    [SerializeField, Tooltip("�ˌ��̃C���^�[�o��")] float _shootInterval = 0.2f;
    float _timer;

    GameObject _player;
    GameObject _playerSprite;
    FriendMove _friendMove;

    Vector3 _defaultScale;



    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerSprite = FindObjectOfType<PlayerFlip>().gameObject;
        _friendMove = FindObjectOfType<FriendMove>().GetComponent<FriendMove>();

        _defaultScale = transform.localScale;  //�J�n���_�̃X�P�[����ۑ�
    }


    void FixedUpdate()
    {
        if (_friendMove)
        {
            FriendStateMove();
        }
        
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            BulletShoot();
        }
    }


    /// <summary>State�ɉ����������̏���</summary>
    void FriendStateMove()
    {
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            transform.localScale = _defaultScale;  �@�@�@�@�@�@�@�@�@�@�@�@�@�@ //�������f�t�H���g�̒l�ɖ߂�
            transform.right = MousePosManager.MousePos() - transform.position;�@//�}�E�X�̃|�W�V�����Ɍ�����
        }
        else if (_friendMove.FriendState == FriendMove.FriendMoveState.Stay)
        {
            transform.localScale = _playerSprite.transform.localScale;  //�v���C���[�ƌ����ƌX�������킹��
            transform.right = _playerSprite.transform.right;
        }
        else
        {
            transform.right = _playerSprite.transform.right;�@//�v���C���[�ƌX�������킹��
        }
    }


    /// <summary>�e�̎ˌ����s������</summary>
    void BulletShoot()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer > _shootInterval)
        {
            Instantiate(_bullet, _muzzle.position, transform.rotation);
            _timer = 0;
        }
    }
}
