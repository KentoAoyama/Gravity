using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] _enemys;

    GameObject _player;

    
    void Start()
    {
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player");
        // ê´äià´Ç¢Ç≈Ç∑ÇÊ


        foreach(var enemy in _enemys)
        {
            enemy.SetActive(false);
        }
    }


    void FixedUpdate()
    {
        transform.position = _player.transform.position;    
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(true);
        }
    }
}
