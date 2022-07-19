using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControllerA : MonoBehaviour
{
    Vector2 _upGravity = new(0, 10);
    Vector2 _downGravity = new(0, -10);
    Vector2 _leftGravity = new(-10, 0);
    Vector2 _rightGravity = new(10, 0);

    GameObject _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        ChangeGravityArrow();        
    }


    void ChangeGravityArrow()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Physics2D.gravity = _upGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.gravity = _downGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Physics2D.gravity = _leftGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Physics2D.gravity = _rightGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
