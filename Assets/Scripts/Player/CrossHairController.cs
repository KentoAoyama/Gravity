using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = MousePosManager.MousePos();
    }
}
