using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    [SerializeField] GameObject _eventSystem;
    [SerializeField] GameObject _help;

    public static bool _isHelp;


    void Start()
    {
        _eventSystem.SetActive(false);
        _help.SetActive(false);

        _isHelp = false;
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !_isHelp)
        {
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
        _isHelp = false;
        _eventSystem.SetActive(false);
        _help.SetActive(false);
    }
}
