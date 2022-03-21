using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour
{
    void Start()
    {
        float timer = GetComponent<ParticleSystem>().duration;
        Destroy(gameObject, timer);
    }
}
