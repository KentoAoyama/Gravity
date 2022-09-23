using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FriendBeamShoot : MonoBehaviour
{
    [SerializeField, Tooltip("ビーム射撃時の暗転用パネル")] GameObject _beamPanel;
    [SerializeField, Tooltip("カメラ")] GameObject[] _camera;

    CinemachineImpulseSource _impulse;
    Animator _friendAnimator;

    FriendMoveArrow _friendMove;
    PlayerBeamStatus _playerBeamStatus;


    void Start()
    {
        _beamPanel.SetActive(false);

        _playerBeamStatus = FindObjectOfType<PlayerBeamStatus>().GetComponent<PlayerBeamStatus>();
        _friendMove = GetComponent<FriendMoveArrow>();

        _impulse = GetComponent<CinemachineImpulseSource>();
        _friendAnimator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        ShootBeam();
    }


    /// <summary>ビームを撃つ処理</summary>
    void ShootBeam()
    {
        if (_playerBeamStatus.IsBeamShoot)
        {
            StartCoroutine(BeamImpulseDelay());
            _friendAnimator.SetBool("IsBeam", true);
            _friendMove.FriendState = FriendMoveArrow.FriendMoveState.Beam;
        }
        else if (_camera.Length > 0)
        {
            foreach(var camera in _camera)
            {
                camera.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }


    IEnumerator BeamImpulseDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _impulse.GenerateImpulse();
    }


    /// <summary>暗転用のパネルを出す</summary>
    public void BeamOn()
    {
        _beamPanel.SetActive(true);
    }


    /// <summary>暗転用のパネルをしまう</summary>
    public void BeamOff()
    {
        _beamPanel.SetActive(false);
        _friendAnimator.SetBool("IsBeam", false);
        _playerBeamStatus.IsBeamShoot = false;
        _friendMove.FriendState = FriendMoveArrow.FriendMoveState.Back;
    }
}
