using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EffectSystem : MonoBehaviour
{
    [SerializeField, Tooltip("�G�t�F�N�g���j�������܂ł̎���")] float _destroyTime = 1f;

    SpriteRenderer _spriteRenderer;


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.DOFade(0, _destroyTime).OnComplete(() => Destroy(gameObject));       
    }
}
