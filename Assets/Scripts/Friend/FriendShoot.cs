using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShoot : MonoBehaviour
{
    [SerializeField, Tooltip("�e�̃v���n�u")] GameObject _bullet;
    [SerializeField, Tooltip("�ˌ����s���ꏊ")] Transform _muzzle;
    [SerializeField, Tooltip("�ˌ��̃C���^�[�o��")] float _shootInterval = 0.2f;
    float _timer;

    GameObject _playerHeart;
    GameObject _player;
    FriendMove _friendMove;

    Vector3 _defaultScale;



    void Start()
    {
        _playerHeart = GameObject.FindGameObjectWithTag("Player");
        _player = FindObjectOfType<PlayerFlip>().gameObject;
        _friendMove = FindObjectOfType<FriendMove>().GetComponent<FriendMove>();

        _defaultScale = transform.localScale;  //�J�n���_�̃X�P�[����ۑ�
    }


    void FixedUpdate()
    {
        if (_friendMove)
        {
            FriendShootRotate();
        }
        
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            BulletShoot();
        }
    }


    /// <summary>State�ɉ����������̏���</summary>
    void FriendShootRotate()
    {
        if (_friendMove.FriendState == FriendMove.FriendMoveState.Shoot)
        {
            transform.localScale = _defaultScale;  �@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@  //�������f�t�H���g�̒l�ɖ߂�
            transform.right = MousePosManager.MousePos() - _playerHeart.transform.position;�@//�}�E�X�̃|�W�V�����Ɍ�����
        }
        else if (_friendMove.FriendState == FriendMove.FriendMoveState.Stay)
        {
            transform.localScale = _player.transform.localScale;  //�v���C���[�ƌ����ƌX�������킹��
            transform.right = _player.transform.right;
        }
        else
        {
            transform.right = _player.transform.right;�@//�v���C���[�ƌX�������킹��
        }
    }


    /// <summary>�e�̎ˌ����s������</summary>
    void BulletShoot()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer > _shootInterval)
        {
            _timer = 0;
            Instantiate(_bullet, _muzzle.position, transform.rotation);
        }
    }
}
