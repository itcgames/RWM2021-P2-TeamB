using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    PreRound, 
    Playing
}

public struct Data
{
    public int completion_time;
    public int level;
    public int moneySpent;
    public int towersBought;
    public int livesLeft;
}

public class GameState : MonoBehaviour
{
    [HideInInspector]
    public State _currentState = State.PreRound;
    [SerializeField] Text _text;
    SpawnController _spawnController;

    bool _fastForward = false;
    Data _gameData;

    private AnalyticsManager _am;

    void Start()
    {
        _gameData.level = 1;
        _gameData.completion_time = (int)Time.time;

        _spawnController = GetComponent<SpawnController>();
        _am = GameObject.FindGameObjectsWithTag("Analytics")[0].GetComponent<AnalyticsManager>();
    }

    public void beginGame()
    {
        _gameData.completion_time = (int)Time.time;
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
        SendData();
        _gameData.level++;
    }

    public void SendData()
    {
        MoneyManager money = GetComponent<MoneyManager>();  

        _gameData.completion_time = (int)Time.time - _gameData.completion_time;
        _gameData.moneySpent = money.getTotalSpent();
        _gameData.towersBought = money.getTowersPurchased();
        _gameData.livesLeft = GetComponent<Life>().currentLives;

        string gameData = JsonUtility.ToJson(_gameData);
        _am.Send(gameData);
        Debug.Log("Enqueued level data: " + gameData);
    }
}
