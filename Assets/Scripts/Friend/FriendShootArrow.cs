using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShootArrow : MonoBehaviour
{
    [SerializeField, Tooltip("�e�̃v���n�u")] GameObject _bullet;
    [SerializeField, Tooltip("�ˌ����s���ꏊ")] Transform _muzzle;
    [SerializeField, Tooltip("�ˌ��̃C���^�[�o��")] float _shootInterval = 0.2f;

    float _timer;

    Vector3 _defaultScale;

    GameObject _player;
    GameObject _friend;
    PlayerBeamStatus _playerBeamStatus;
    FriendMoveArrow _friendMove;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _friend = GameObject.FindGameObjectWithTag("Friend");
        _playerBeamStatus = _player.GetComponent<PlayerBeamStatus>();
        _friendMove = FindObjectOfType<FriendMoveArrow>().GetComponent<FriendMoveArrow>();

        _defaultScale = transform.localScale;
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
        Vector3 _dir = transform.position - _player.transform.position - _player.transform.up * _friendMove._shootPosUp;

        if (_friendMove.FriendState == FriendMoveArrow.FriendMoveState.Shoot ||
            _friendMove.FriendState == FriendMoveArrow.FriendMoveState.Go)
        {
            transform.right = _dir;

            if (_player.transform.position.x < _friend.transform.position.x)
            {
                transform.localScale = new(_defaultScale.x, _defaultScale.y, _defaultScale.z);
            }
            else
            {
                transform.localScale = new(_defaultScale.x, -_defaultScale.y, _defaultScale.z);
            }
        }
        else
        {
            transform.localScale = new(_defaultScale.x, _defaultScale.y, _defaultScale.z);
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
