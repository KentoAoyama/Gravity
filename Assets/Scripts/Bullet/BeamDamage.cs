using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDamage : MonoBehaviour
{
    [SerializeField] int _beamDamage = 100;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage) && collision.gameObject.tag != "Player")
        {
            addDamage.AddDamage(_beamDamage);
        }
    }
}
