using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    [SerializeField] GameObject _eventSystem;
    [SerializeField] GameObject _help;

    public static bool _isHelp;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _sound;


    void Start()
    {
        _isHelp = false;

        _eventSystem.SetActive(false);
        _help.SetActive(false);

        _audioSource.clip = _sound;
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !_isHelp)
        {
            _audioSource.Play();

            _isHelp = true;
            _eventSystem.SetActive(true);
            _help.SetActive(true);
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            HelpOff();
        }
    }


    public void HelpOff()
    {
        _audioSource.Play();

        _isHelp = false;
        _eventSystem.SetActive(false);
        _help.SetActive(false);
    }
}
