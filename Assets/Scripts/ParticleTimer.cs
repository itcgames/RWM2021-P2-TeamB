using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour
{
    void Start()
    {
        float timer = GetComponent<ParticleSystem>().main.duration;
        Destroy(gameObject, timer);
    }
}
