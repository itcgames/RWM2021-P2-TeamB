using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [System.Serializable]
    public struct TowerSynposis
    {
        public string _name;
        public string _description;
    }

    public GameObject towerStatus;
    public Text _name;
    public Text _synopsis;
    public Text _price;
    public Image _image;

    public TowerSynposis[] _synopses;

    GameObject _currentTower;

    private void Awake()
    {
        towerStatus.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hide();
        }
    }

    public void setTower(GameObject t_towerObject)
    {
        if (_currentTower)
            _currentTower.GetComponent<BaseTower>().hideTargetCirlce();

        _currentTower = t_towerObject;
        _currentTower.GetComponent<BaseTower>().showTargetCircle();
        towerStatus.SetActive(true);
        _name.text = _currentTower.GetComponent<BaseTower>().GetType().ToString();

        foreach (TowerSynposis name in _synopses)
            if (name._name == _name.text)
                _synopsis.text = name._description;
        _image.sprite = _currentTower.GetComponent<SpriteRenderer>().sprite;

        _price.text = "Sale Price: " + Mathf.RoundToInt(_currentTower.GetComponent<BaseTower>().getCost() * .7f);
    }

    public void sellTower()
    {
        if (_currentTower)
        {
            GetComponent<MoneyManager>().soldTower(_currentTower.GetComponent<BaseTower>().getCost());
            Destroy(_currentTower);
            hide();
        }           
    }

    void hide()
    {
        towerStatus.SetActive(false);
        _currentTower.GetComponent<BaseTower>().hideTargetCirlce();
        _currentTower = null;
    }
}
