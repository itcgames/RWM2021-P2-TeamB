using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    GraphicRaycaster _rayCaster;
    PointerEventData _pointerEventData;
    EventSystem _eventSystem;

    void Start()
    {
        _rayCaster = transform.GetChild(0).GetComponent<GraphicRaycaster>();

        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>(); 

        _moneyManager = GetComponent<MoneyManager>();
        _gameState = GetComponent<GameState>();
        _layerMask = LayerMask.GetMask("BG");

        _currentTower = towers[0];

        _towerPreview = new GameObject();
        _towerPreview.name = "Tower Preview";

        SpriteRenderer r = _towerPreview.AddComponent<SpriteRenderer>();
        r.sortingOrder = 1;

        SetCurrentTower(-1);
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, _rayDistance, _layerMask);
        if (_currentTower)
        {
            bool placable = false;
            SpriteRenderer spr = _towerPreview.GetComponent<SpriteRenderer>();
            spr.color = new Color(1f, 1f, 1f, 0.5f); // Reset the colour to default


            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            _rayCaster.Raycast(_pointerEventData, results);

            if (hit.collider && results.Count <= 0)
            {
                if (hit.collider.name == "Background")
                {     
                    placable = true;
                    if (Input.GetMouseButtonDown(0))
                        spawn(hit.point);
                }
            }

            if (!placable)
                spr.color = new Color(1f, 0f, 0f, 0.75f);

            // Set to mouse position
            _towerPreview.GetComponent<Transform>().position = hit.point;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _currentTower = null;
                _towerPreview.SetActive(false);
            }
        }
        else
        {
            if (hit.collider && hit.collider.tag == "Tower")
                    if (Input.GetMouseButtonDown(0))
                        GetComponent<TowerManager>().setTower(hit.collider.transform.parent.gameObject);
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
            
            _towerPreview.SetActive(true);
        }
    }


    void spawn(Vector2 _point)
    {
        if (_currentTower)
        {
            int cost = _currentTower.GetComponent<BaseTower>().cost;
            if (_moneyManager.inquire(cost))
            {
                _moneyManager.purchasedTower(cost);

                Instantiate(_currentTower, _point, Quaternion.identity);

                _currentTower = null;
                _towerPreview.SetActive(false);
            }
        }
    }
}
