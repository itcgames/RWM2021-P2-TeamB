using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Place the prefab for all the available towers here")]
    GameObject[] towers;


    MoneyManager _moneyManager;
    GameState _gameState;

    GameObject _currentTower;

    int _layerMask;
    float _rayDistance = Mathf.Infinity;

    void Start()
    {
        _moneyManager = GetComponent<MoneyManager>();  
        _gameState = GetComponent<GameState>();
        _layerMask = LayerMask.GetMask("BG");
        _currentTower = towers[0];
    }

    void Update()
    {
        if (_gameState._currentState == State.PreRound)
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, _rayDistance, _layerMask);

                if (hit.collider)
                    if (hit.collider.name == "Background")
                        spawn(hit.point);
                        
            }
    }

    void SetCurrentTower(int _index)
    {
        _currentTower = null;
        if (_index >= 0)
            _currentTower = towers[_index];
    }


    void spawn(Vector2 _point)
    {
        if (_currentTower)
        {
            int cost = _currentTower.GetComponent<BaseTower>().cost;
            if (cost <= _moneyManager.balance)
            {
                _moneyManager.balance -= cost;
                Instantiate(_currentTower, _point, Quaternion.identity);
            }
        }
    }
}
