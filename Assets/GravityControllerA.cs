using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControllerA : MonoBehaviour
{
    GameObject _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        ChangeGravityArrow();        
    }


    void ChangeGravityArrow()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Physics2D.gravity = GravityController._upGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.gravity = GravityController._downGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Physics2D.gravity = GravityController._leftGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Physics2D.gravity = GravityController._rightGravity;
            _player.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
