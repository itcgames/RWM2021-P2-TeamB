using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitToMainMenu()
    {
        //TODO: add main menu scene number or name
        // SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
