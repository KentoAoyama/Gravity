using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPointController : MonoBehaviour
{
    [SerializeField, Tooltip("�|�C���g���莞�ɃI���ɂ��Ă����I�u�W�F�N�g")] GameObject[] _beamGauge;


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
