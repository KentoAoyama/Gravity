using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    FadeManager _fadeManager;

    TitleSceneState _state;

    void Awake()
    {
        _fadeManager = FindObjectOfType<FadeManager>().GetComponent<FadeManager>();
        _fadeManager.StartFadeIn();
    }

    
    void FixedUpdate()
    {
        MainMenuChange();
    }


    /// <summary>Stateごとに行うAnimation</summary>
    void StateAnimation()
    {
        if (_state == TitleSceneState.Title)
        {
            
        }
    }


    /// <summary>TitleSceneのStateをMainmenuに変更するメソッド</summary>
    void MainMenuChange()
    {
        if (_state == TitleSceneState.Title && Input.anyKey)
        {
            _state = TitleSceneState.MainMenu;
        }
    }


    /// <summary>ボタンから呼び出す用のTitleへ戻るボタン</summary>
    public void TitleChange()
    {
        _state = TitleSceneState.Title;
    }


    /// <summary>タイトルシーンの状態を表す列挙型</summary>
    enum TitleSceneState
    {
        Title,
        MainMenu,
    }
}
