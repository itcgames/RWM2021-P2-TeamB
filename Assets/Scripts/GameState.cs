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

    void Start()
    {
        _spawnController = GetComponent<SpawnController>(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _text.text = "Game Status: Playing";
            _currentState = State.Playing;
            _spawnController.StartWave();
        }
    }
}
