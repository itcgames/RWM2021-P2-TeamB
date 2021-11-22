using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private Text lifeText;
    [SerializeField] public int maxLives;
    [HideInInspector] public int currentLives;

    void Start()
    {
        if(lifeText == null)
        {
            Debug.LogError("ERROR: Text variable empty, please assign text object in inspector");
        }

        currentLives = maxLives;
        UpdateText();
    }

    public void removeLife(int livesLost)
    {
        currentLives -= livesLost;
        UpdateText();
    }

    public void UpdateText()
    {
        lifeText.text = $"<3 : {currentLives} / {maxLives}";
    }
}
