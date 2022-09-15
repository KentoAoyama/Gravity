using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPointController : MonoBehaviour
{
    [SerializeField, Tooltip("ポイント入手時にオンにしていくオブジェクト")] GameObject[] _beamGauge;


    void Start()
    {
        foreach (var gauge in _beamGauge)
        {
            gauge.SetActive(false);
        }
    }


    public void ChangeBeamPoints(int value)
    {
        if (value - 1 < 0)
        {
            foreach(var gauge in _beamGauge)
            {
                gauge.SetActive(false);
            }
        }
        else
        {
            _beamGauge[value - 1].SetActive(true);
        }
    }
}
