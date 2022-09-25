using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossBattleSystem : MonoBehaviour
{
    [SerializeField, Tooltip("GravityArrow")] GameObject _gravityArrow;
    [SerializeField, Tooltip("Bossのオブジェクト")] GameObject _boss;
    [SerializeField, Tooltip("Boss戦時に開く扉")] GameObject _bossStartWall;
    [SerializeField, Tooltip("Boss撃破後に開く扉")] GameObject _bossEndWall;
    [SerializeField, Tooltip("Boss撃破後に出る光")] GameObject _bossWallLight;
    [SerializeField, Tooltip("Boss戦用のカメラ")] GameObject _camera;

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
