using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FadeManager : MonoBehaviour
{
    [SerializeField, Tooltip("フェードのオブジェクト")] GameObject _fadeObject;
    [SerializeField, Tooltip("フェードに使うImage")] Image _fadeImage;
    [SerializeField, Tooltip("フェードにかける時間")] float _fadeTime = 1f;


    void Awake()
    {
        _fadeObject.SetActive(true);
    }


    public void StartFadeIn()//フェードイン関数
    {
        _fadeImage.DOFade(endValue: 0f, duration: _fadeTime).OnComplete(() => _fadeImage.gameObject.SetActive(false));
    }


    public void StartFadeOut(string scene)//フェードアウト関数
    {
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.DOFade(duration: 1f, endValue: _fadeTime).OnComplete(() => SceneManager.LoadScene(scene));
    }


    public void Exit()
    {
        Application.Quit();
    }


    void OnDestroy()
    {
        DOTween.KillAll();
    }
}
