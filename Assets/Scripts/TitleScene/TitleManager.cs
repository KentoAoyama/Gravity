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


    /// <summary>State���Ƃɍs��Animation</summary>
    void StateAnimation()
    {
        if (_state == TitleSceneState.Title)
        {
            
        }
    }


    /// <summary>TitleScene��State��Mainmenu�ɕύX���郁�\�b�h</summary>
    void MainMenuChange()
    {
        if (_state == TitleSceneState.Title && Input.anyKey)
        {
            _state = TitleSceneState.MainMenu;
        }
    }


    /// <summary>�{�^������Ăяo���p��Title�֖߂�{�^��</summary>
    public void TitleChange()
    {
        _state = TitleSceneState.Title;
    }


    /// <summary>�^�C�g���V�[���̏�Ԃ�\���񋓌^</summary>
    enum TitleSceneState
    {
        Title,
        MainMenu,
    }
}
