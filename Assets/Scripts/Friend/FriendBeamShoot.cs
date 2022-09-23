using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FriendBeamShoot : MonoBehaviour
{
    [SerializeField, Tooltip("�r�[���ˌ����̈Ó]�p�p�l��")] GameObject _beamPanel;
    [SerializeField, Tooltip("�J����")] GameObject[] _camera;

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


    /// <summary>�r�[����������</summary>
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


    /// <summary>�Ó]�p�̃p�l�����o��</summary>
    public void BeamOn()
    {
        _beamPanel.SetActive(true);
    }


    /// <summary>�Ó]�p�̃p�l�������܂�</summary>
    public void BeamOff()
    {
        _beamPanel.SetActive(false);
        _friendAnimator.SetBool("IsBeam", false);
        _playerBeamStatus.IsBeamShoot = false;
        _friendMove.FriendState = FriendMoveArrow.FriendMoveState.Back;
    }
}
