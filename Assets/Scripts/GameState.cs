using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    PreRound, 
    Playing
}

public class GameState : MonoBehaviour
{
    [HideInInspector]
    public State _currentState = State.PreRound;
    [SerializeField] Text _text;
    SpawnController _spawnController;

    bool _fastForward = false;
    Data _gameData;

    void Start()
    {
        _gameData.id = SystemInfo.deviceUniqueIdentifier;
        _gameData.level = 1;
        _gameData.completion_time = (int)Time.time;
        _spawnController = GetComponent<SpawnController>();
    }

    public void beginGame()
    {

        _text.text = "Game Status: Playing";
        _currentState = State.Playing;
        _spawnController.StartWave();

        fastForward();

    }

    public void fastForward()
    {
        
        if (_fastForward)
        {
            Time.timeScale = 2;
        }
        else 
        {
            Time.timeScale = 1;
        }

        _fastForward = !_fastForward;
    }

    public void WaveCleared()
    {
        _gameData.level = 2;
    }

    public void SendData()
    {
        _gameData.completion_time = (int)Time.time - _gameData.completion_time;
        GameData sender = GetComponent<GameData>();
        string gameData = JsonUtility.ToJson(_gameData);
        Debug.Log("Sent: " + gameData);
        StartCoroutine(sender.PostMethod(gameData));
    }
}
