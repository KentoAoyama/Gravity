using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        mousePosition.z = 0;

        transform.position = mousePosition;
    }
}
