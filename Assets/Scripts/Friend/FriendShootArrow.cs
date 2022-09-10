using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShootArrow : MonoBehaviour
{
    [SerializeField, Tooltip("�e�̃v���n�u")] GameObject _bullet;
    [SerializeField, Tooltip("�ˌ����s���ꏊ")] Transform _muzzle;
    [SerializeField, Tooltip("�ˌ��̃C���^�[�o��")] float _shootInterval = 0.2f;

    Vector3 _defaultRotate;

    float _timer;

    GameObject _player;
    PlayerBeamStatus _playerBeamStatus;
    FriendMoveArrow _friendMove;


    void Start()
    {
        _player = GameObject.Find("Player");
        _playerBeamStatus = _player.GetComponent<PlayerBeamStatus>();
        _friendMove = FindObjectOfType<FriendMoveArrow>().GetComponent<FriendMoveArrow>();

        _defaultRotate = transform.right;
    }


    void FixedUpdate()
    {
        FriendRotateChange();

        if (!_playerBeamStatus.IsBeamShoot)
        {
            BulletShoot();
        }
    }


    /// <summary>ShootState���̌���</summary>
    void FriendRotateChange()
    {
        if (_friendMove.FriendState == FriendMoveArrow.FriendMoveState.Shoot)
        {
            transform.right = transform.position - _player.transform.position;
        }
        else if (_friendMove.FriendState == FriendMoveArrow.FriendMoveState.Go)
        {
            transform.right = _friendMove.ShootPos() - _player.transform.position;
        }
    }


    /// <summary>�e�̎ˌ����s������</summary>
    void BulletShoot()
    {
        if (_friendMove.FriendState == FriendMoveArrow.FriendMoveState.Shoot)
        {
            _timer += Time.deltaTime;
        }


        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            if (_friendMove.FriendState == FriendMoveArrow.FriendMoveState.Shoot)
            {
                if (_timer > _shootInterval)
                {
                    Instantiate(_bullet, _muzzle.position, transform.rotation);
                    _friendMove.ShootTimer = 0;
                    _timer = 0;
                }
            }
            else
            {
                _friendMove.FriendState = FriendMoveArrow.FriendMoveState.Go;
                _timer = 0;
            }
        }
    }
}
