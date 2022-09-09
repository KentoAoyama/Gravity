using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGravityStatus : MonoBehaviour
{
    [SerializeField, Tooltip("Gravity�Q�[�W�̃X���C�_�[")] Slider _gravitySlider;
    [SerializeField, Tooltip("Gravity�Q�[�W�̏㏸��")] float _upSpeed = 2f;
    [SerializeField, Tooltip("Gravity�Q�[�W�̌�����")] float _downSpeed = 2f;
    [SerializeField, Tooltip("�Q�[�W���[���ɂȂ����ۂ̕ǂ��痣��鋗��")] float _pushPower = 5f;
    [SerializeField, Tooltip("")] float _rotationSpeed = 2f;
    [Tooltip("Gravity�Q�[�W�̗�")] public float _gravityGauge = 100f;
    [Tooltip("Gravity�Q�[�W�̍ő��")] float _maxGauge = 100f;
    bool _isPush;
    bool _isFall;

    PlayerMove _playerMove;
    GravityController _gravityController;

    Rigidbody2D _rb;


    void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _gravityController = GetComponent<GravityController>();
        _rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        GravityGauge();
        GravityGaugeZeroMove();
        SliderValue();
    }


    /// <summary>�Q�[�W�̑������Ǘ����郁�\�b�h</summary>
    void GravityGauge()
    {
        if (_playerMove.PGS == PlayerMove.PlayerGravityState.Down && _playerMove.OnGround)
        {
            _gravityGauge += Time.deltaTime * _upSpeed;
        }
        else if (_playerMove.PGS != PlayerMove.PlayerGravityState.Down && _playerMove.OnGround && !_gravityController.IsRotate)
        {
            _gravityGauge -= Time.deltaTime * _downSpeed;
        }
        else if (_playerMove.PGS != PlayerMove.PlayerGravityState.Down && _isFall)
        {
            _gravityGauge += Time.deltaTime * _upSpeed;
        }


        if (_gravityGauge > 100)
        {
            _gravityGauge = 100;
        }

        if (_gravityGauge < 0 && !_gravityController.IsRotate)
        {
            _isFall = true;
            _gravityGauge = 0;
        }
        else
        {
            _isFall = false;
        }
    }


    /// <summary>�Q�[�W��0�ɂȂ������̏���</summary>
    void GravityGaugeZeroMove()
    {
        if (_isFall && !_isPush)
        {
            _gravityController.enabled = false;
            Physics2D.gravity = new(0, -30);
            _rb.AddForce(transform.up * _pushPower, ForceMode2D.Impulse);
            _isPush = true;
        }

        if (!_isFall && _isPush && _playerMove.OnGround && !_gravityController.enabled)
        {
            _gravityController.enabled = true;
            _isPush = false;
        }
    }


    /// <summary>�X���C�_�[��Value��ύX���郁�\�b�h</summary>
    void SliderValue()
    {
        _gravitySlider.value = _gravityGauge / _maxGauge;
    }


    public void GaugeUp()
    {

    }
}
