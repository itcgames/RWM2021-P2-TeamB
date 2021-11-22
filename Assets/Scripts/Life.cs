using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] public int maxLives;
    [HideInInspector] public int currentLives;

    void start()
    {
        currentLives = maxLives;
    }

    public void removeLife(int livesLost)
    {
        currentLives -= livesLost;
    }
}
