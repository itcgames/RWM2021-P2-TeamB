using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerPlacer : MonoBehaviour
{
    GameState _gameState;
    public GameObject dartTower;

    int _layerMask;
    float _rayDistance = Mathf.Infinity;

    void Start()
    {
        _gameState = GetComponent<GameState>();
        _layerMask = LayerMask.GetMask("BG");
    }

    void Update()
    {
        if (_gameState._currentState == State.PreRound)
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, _rayDistance, _layerMask);

                if (hit.collider)
                    if (hit.collider.name == "Background")
                        Instantiate(dartTower, hit.point, Quaternion.identity);
            }
    }
}
