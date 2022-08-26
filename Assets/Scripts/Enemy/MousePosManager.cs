using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosManager : MonoBehaviour
{
    public static Vector3 MousePos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(pos);

        return mousePosition;
    }
}
