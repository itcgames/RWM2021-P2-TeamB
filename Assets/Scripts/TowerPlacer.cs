using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Tower
{
    Archer, 
    Ballista,
    Mangonel,
    Trebuchet,
    Caltrops
}

public class TowerPlacer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Place the prefab for all the available towers here")]
    GameObject[] towers;


    MoneyManager _moneyManager;
    GameState _gameState;

    GameObject _currentTower;
    GameObject _towerPreview;

    int _layerMask;
    float _rayDistance = Mathf.Infinity;

    void Start()
    {
        _moneyManager = GetComponent<MoneyManager>();
        _gameState = GetComponent<GameState>();
        _layerMask = LayerMask.GetMask("BG");

        _currentTower = towers[0];

        _towerPreview = new GameObject();
        _towerPreview.name = "Tower Preview";

        SpriteRenderer r = _towerPreview.AddComponent<SpriteRenderer>();
        r.sortingOrder = 1;

        SetCurrentTower(0);
    }

    void Update()
    {
        if (_currentTower)
        {
            SpriteRenderer spr = _towerPreview.GetComponent<SpriteRenderer>();
            spr.color = new Color(1f, 1f, 1f, 0.5f); // Reset the colour to default

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, _rayDistance, _layerMask);

            if (hit.collider)
            {
                if (hit.collider.name == "Background")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        spawn(hit.point);
                    }
                }
                else
                {
                    // Set colour to red as placement is invalid
                    spr.color = new Color(1f, 0f, 0f, 0.75f);
                }
            }

            // Set to mouse position
            _towerPreview.GetComponent<Transform>().position = hit.point;
        }
    }
    
    public void SetCurrentTower(int _index)
    {
        _currentTower = null;
        if (_index >= 0)
        {
            _currentTower = towers[_index];

            _towerPreview.GetComponent<SpriteRenderer>().sprite =
                _currentTower.GetComponent<SpriteRenderer>().sprite;
        }
    }


    void spawn(Vector2 _point)
    {
        if (_currentTower)
        {
            int cost = _currentTower.GetComponent<BaseTower>().cost;
            if (cost <= _moneyManager.balance)
            {
                _moneyManager.balance -= cost;
                _moneyManager.updateText();
                Instantiate(_currentTower, _point, Quaternion.identity);
            }
        }
    }
}
