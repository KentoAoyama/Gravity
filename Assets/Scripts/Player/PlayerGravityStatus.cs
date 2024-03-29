using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGravityStatus : MonoBehaviour
{
    [Header("Gauge")]
    [SerializeField, Tooltip("Gravityゲージのスライダー")] Slider _gravitySlider;
    [SerializeField, Tooltip("Gravityゲージの上昇量")] float _upSpeed = 2f;
    [SerializeField, Tooltip("Gravityゲージの減少量")] float _downSpeed = 2f;

    [Header("Move")]
    [SerializeField, Tooltip("ゲージがゼロになった際の壁から離れる距離")] float _pushPower = 5f;
    [SerializeField, Tooltip("落下中の重力の大きさ")] float _fallGravityLevel = 30f;

    [Tooltip("Gravityゲージの量")] public float _gravityGauge = 100f;
    [Tooltip("Gravityゲージの最大量")] float _maxGauge = 100f;

    bool _isPush;
    bool _isFall;
    /// <summary>落下を表すプロパティ</summary>
    public bool IsFall => _isFall;

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


    /// <summary>ゲージの増減を管理するメソッド</summary>
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
            //真下を向いていた場合横に向ける
            if (Mathf.Abs(transform.localRotation.z) > 0.9)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                _gravityController.Rotate = 90;
                _gravityController.CurrentRotate = 90;
            }

            _isFall = true;
            _gravityGauge = 0;
        }
        else
        {
            _isFall = false;
        }
    }


    /// <summary>ゲージが0になった時の処理</summary>
    void GravityGaugeZeroMove()
    {
        if (_isFall && !_isPush)
        {
            _gravityController.enabled = false;
            Physics2D.gravity = new(0, -_fallGravityLevel);
            _rb.AddForce(transform.up * _pushPower, ForceMode2D.Impulse);
            _isPush = true;
        }

        if (!_isFall && _isPush && _playerMove.OnGround && !_gravityController.enabled)
        {
            _gravityController.enabled = true;
            _isPush = false;
        }
    }


    /// <summary>スライダーのValueを変更するメソッド</summary>
    void SliderValue()
    {
        _gravitySlider.value = _gravityGauge / _maxGauge;
    }


    public void GaugeUp()
    {

    }
}
