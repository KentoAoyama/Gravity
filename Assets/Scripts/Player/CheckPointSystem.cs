using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public static Vector2 _initialPosition = new(0, 17);


    void Start()
    {
        transform.position = _initialPosition;
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            _initialPosition = collision.gameObject.transform.position;
        }
    }
}
