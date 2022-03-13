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
    public State _currentState = State.PreRound;
    [SerializeField] GameObject[] _objects;
    [SerializeField] Text _text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach(var obj in _objects)
                obj.SetActive(true);
            _text.text = "Game Status: Playing";
            _currentState = State.Playing;
        }
    }
}
