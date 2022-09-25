using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    [SerializeField] PlayerHPStatus _playerHpStatus;

    Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        _animator.SetBool("IsDamage", _playerHpStatus.IsDamage);
    }
}
