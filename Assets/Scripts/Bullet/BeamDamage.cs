using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDamage : MonoBehaviour
{
    [SerializeField] int _beamDamage = 100;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage))
        {
            addDamage.AddDamage(_beamDamage);
        }
    }
}
