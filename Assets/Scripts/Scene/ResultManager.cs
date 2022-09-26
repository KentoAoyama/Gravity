using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] FadeManager _fadeManager;
    [SerializeField] string _titleScene = "TitleScene";
    [SerializeField] float _waitTime = 4f;
    float _timer = 0;


    void Start()
    {
        CheckPointSystem._initialPosition = new(0, 17);

        _fadeManager.StartFadeIn();
    }

 
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _waitTime && Input.anyKey || _timer > _waitTime * 2)
        {
            _fadeManager.StartFadeOut(_titleScene);
        }
    }
}
