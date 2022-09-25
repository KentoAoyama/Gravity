using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossBattleSystem : MonoBehaviour
{
    [SerializeField, Tooltip("GravityArrow")] GameObject _gravityArrow;
    [SerializeField, Tooltip("Boss�̃I�u�W�F�N�g")] GameObject _boss;
    [SerializeField, Tooltip("Boss�펞�ɊJ����")] GameObject _bossStartWall;
    [SerializeField, Tooltip("Boss���j��ɊJ����")] GameObject _bossEndWall;
    [SerializeField, Tooltip("Boss���j��ɏo���")] GameObject _bossWallLight;
    [SerializeField, Tooltip("Boss��p�̃J����")] GameObject _camera;

    bool _isBossFight;

    CinemachineVirtualCamera _cinema;

    BossHealth _bossHealth;

    [SerializeField] GameObject _bgm1;
    [SerializeField] GameObject _bgm2;


    void Start()
    {
        _bossHealth = _boss.GetComponent<BossHealth>();
        _cinema = _camera.GetComponent<CinemachineVirtualCamera>();

        _boss.SetActive(false);
        _bossStartWall.SetActive(false);
        _bossEndWall.SetActive(true);
        _bossWallLight.SetActive(false);

        _cinema.Priority = 0;
    }

    
    void Update()
    {
        if (_isBossFight)
        {
            _boss.SetActive(true);
            _bossStartWall.SetActive(true);
            _cinema.Priority = 1;

            _bgm1.SetActive(false);
            _bgm2.SetActive(true);
        }

        if (_bossHealth.IsLose)
        {
            _bossEndWall.SetActive(false);
            _bossWallLight.SetActive(true);

            _bgm1.SetActive(true);
            _bgm2.SetActive(false);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isBossFight = true;
        }
    }
}
