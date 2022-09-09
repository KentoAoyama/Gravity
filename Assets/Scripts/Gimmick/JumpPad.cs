using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPad : MonoBehaviour
{
    [SerializeField, Tooltip("�]������ꏊ")] Transform _teleport;
    [SerializeField, Tooltip("�q���g��")] GameObject _tips;
    [SerializeField, Tooltip("�e���|�[�g�̎���")] float _teleportTime = 5f;
    [SerializeField, Tooltip("�ړ��̃G�t�F�N�g")] GameObject _particle;
    [Tooltip("�v���C���[�̐ڐG�̔�����Ƃ�")] bool _isPlayerIn;
    [Tooltip("�e���|�[�g���ł��邩�ǂ���")] bool _isTeleport;

    GameObject _player;

    Animator _tipsAnimator;

    Rigidbody2D _playerRb;

    GravityController _gravityController;
    PlayerMove _playerMove;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody2D>();
        _tipsAnimator = _tips.GetComponent<Animator>();
        _gravityController = FindObjectOfType<GravityController>().GetComponent<GravityController>();
        _playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
        _particle.SetActive(false);
    }


    void Update()
    {
        Teleport();
        Tips();
    }


    /// <summary>�e���|�[�g�̏���</summary>
    void Teleport()
    {
        if (_isPlayerIn && Input.GetButton("Action") && !_gravityController.IsRotate && !_isTeleport)
        {
            StartCoroutine(TeleportCoroutine());
        }
    }


    /// <summary>�e���|�[�g�̏���</summary>
    IEnumerator TeleportCoroutine()
    {
        //�ړ��J�n���̏���
        _isTeleport = true;
        _playerRb.Sleep();
        _playerMove.enabled = false;
        _particle.SetActive(true);

    �@�@//�G�t�F�N�g���o�Ă���ԑ҂�
        yield return new WaitForSeconds(_teleportTime);

        //�ړ��I�����̏���
        _playerRb.WakeUp();
        _particle.SetActive(false);
        _player.transform.position = _teleport.position;
        _playerMove.enabled = true;
        _isTeleport = false;
    }


    /// <summary>�q���g���o������</summary>
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
