using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("TitleScene全てのUI")] GameObject _titleUI;
    [SerializeField, Tooltip("TitleSceneに戻る時間")] float _backTime = 20f;

    float _timer;
    bool _isMainMenu;

    Animator _animator;

    FadeManager _fadeManager;


    void Awake()
    {
        _animator = _titleUI.GetComponent<Animator>();

        _fadeManager = FindObjectOfType<FadeManager>().GetComponent<FadeManager>();
        _fadeManager.StartFadeIn();
    }

    
    void Update()
    {
        MainMenuChange();
        TitleBack();
    }


    /// <summary>Mainmenuに移行するためのメソッド</summary>
    void MainMenuChange()
    {
        if (Input.anyKey && !_isMainMenu)
        {
            _animator.Play("MainMenu");
            _isMainMenu = true;
        }
    }


    void TitleBack()
    {
        if (_isMainMenu)
        {
            _timer += Time.deltaTime;

            if (Input.anyKey)
            {
                _timer = 0;
            }

            if (_timer > _backTime)
            {
                _fadeManager.StartFadeOut("TitleScene");
            }
        }
    }


    /// <summary>Titleに移行するためのボタン用のメソッド</summary>
    public void TitleOn()
    {
        _animator.Play("Title");
        _isMainMenu = false;
    }
}
