using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Tooltip("")] bool _isPlayerIn;

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        _isPlayerIn = true;
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerIn = false;
    }
}
