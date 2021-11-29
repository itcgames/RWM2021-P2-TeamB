using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] public Text lifeText;
    [SerializeField] public int maxLives;
    [SerializeField] public GameObject gameOverScreen;
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
        CheckIfDied(livesLost);
    }

    public void UpdateText()
    {
        lifeText.text = $"{currentLives} / {maxLives}";
    }

    public void CheckIfDied(int livesLost)
    {
        if(currentLives <=0)
        {
            gameOverScreen.SetActive(true);
            //TODO: set timescale to 0
        }
    }
}
