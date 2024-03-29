using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPad : MonoBehaviour
{
    [SerializeField, Tooltip("転送する場所")] Transform _teleport;
    [SerializeField, Tooltip("ヒントの")] GameObject _tips;
    [SerializeField, Tooltip("テレポートの時間")] float _teleportTime = 5f;
    [SerializeField, Tooltip("移動のエフェクト")] GameObject _particle;
    [Tooltip("プレイヤーの接触の判定をとる")] bool _isPlayerIn;
    [Tooltip("テレポート中であるかどうか")] bool _isTeleport;

    GameObject _player;

    Animator _tipsAnimator;

    Rigidbody2D _playerRb;

    GravityController _gravityController;
    PlayerMove _playerMove;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _sound;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody2D>();
        _tipsAnimator = _tips.GetComponent<Animator>();
        _gravityController = FindObjectOfType<GravityController>().GetComponent<GravityController>();
        _playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
        _particle.SetActive(false);

        _audioSource.clip = _sound;
    }


    void Update()
    {
        Teleport();
        Tips();
    }


    /// <summary>テレポートの処理</summary>
    void Teleport()
    {
        if (_isPlayerIn && Input.GetButton("Submit") && !_gravityController.IsRotate && !_isTeleport)
        {
            _audioSource.Play();
            StartCoroutine(TeleportCoroutine());
        }
    }


    /// <summary>テレポートの処理</summary>
    IEnumerator TeleportCoroutine()
    {
        //移動開始時の処理
        _isTeleport = true;
        _playerRb.Sleep();
        _playerMove.enabled = false;
        _particle.SetActive(true);

    　　//エフェクトが出ている間待つ
        yield return new WaitForSeconds(_teleportTime);

        //移動終了時の処理
        _player.transform.position = _teleport.position;
        _playerRb.WakeUp();
        _particle.SetActive(false);
        _playerMove.enabled = true;

        //連続でテレポートできなくする
        yield return new WaitForSeconds(1);

        _isTeleport = false;
    }


    /// <summary>ヒントを出す処理</summary>
    void Tips()
    {
        if (_isPlayerIn)
        {
            _tipsAnimator.Play("Tips");
        }
        
        if (!_isPlayerIn)
        {
            _tipsAnimator.Play("TipsOut");
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerIn = true;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerIn = false;
        }
    }
}
