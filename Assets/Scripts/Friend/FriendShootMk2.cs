using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShootMk2 : MonoBehaviour
{
    [SerializeField, Tooltip("�e�̃v���n�u")] GameObject _bullet;
    [SerializeField, Tooltip("�r�[���̃I�u�W�F�N�g")] GameObject _beam;
    [SerializeField, Tooltip("�ˌ����s���ꏊ")] Transform _muzzle;
    [SerializeField, Tooltip("�ˌ��̃C���^�[�o��")] float _shootInterval = 0.2f;

    float _timer;

    GameObject _player;
    FriendMoveMk2 _friendMove;


    void Start()
    {
        _beam.SetActive(false);
        _player = GameObject.FindGameObjectWithTag("Player");
        _friendMove = FindObjectOfType<FriendMoveMk2>().GetComponent<FriendMoveMk2>();
    }


    void FixedUpdate()
    {
        FriendRotateChange();
        BulletShoot();

        if (_friendMove.FriendState != FriendMoveMk2.FriendMoveState.Shoot)
        {
            transform.right = _player.transform.right;
        }
    }


    void FriendRotateChange()
    {
        transform.right = transform.position - _player.transform.position;
    }


    /// <summary>�e�̎ˌ����s������</summary>
    void BulletShoot()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer > _shootInterval || Input.GetButton("Fire2") && _timer > _shootInterval)
        {
            _friendMove.FriendState = FriendMoveMk2.FriendMoveState.Shoot;
            _friendMove.ShootTimer = 0;

            Instantiate(_bullet, _muzzle.position, transform.rotation);
            _timer = 0;
        }    
    }
}
