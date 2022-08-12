using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 20.0f;
    [SerializeField]
    float radius = 5.0f;
    Vector2 _rootPos;
    float angle;

    void Start()
    {
        _rootPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = Vector2.zero;

        angle += Time.deltaTime * moveSpeed;

        move.y = radius * Mathf.Sin(angle);
        move.x = radius * Mathf.Cos(angle);

        transform.position = _rootPos + move;
    }
}