using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLine : MonoBehaviour
{
    [SerializeField] FadeManager _fade;
    [SerializeField] string _nextScene;

    void Start()
    {
        StartCoroutine(StartDelay());
    }


    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1);
        _fade.StartFadeIn();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _fade.StartFadeOut(_nextScene);
        }
    }
}
