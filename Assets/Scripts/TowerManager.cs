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

    [SerializeField] Button _fireRateUpgrade;
    [SerializeField] Button _rangeUpgrade;

    public TowerSynposis[] _synopses;

    GameObject _currentTower;

    private AnalyticsManager _am;

    private void Awake()
    {
        towerStatus.SetActive(false);
        _am = GameObject.FindGameObjectsWithTag("Analytics")[0].GetComponent<AnalyticsManager>();
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
            hide();

        _currentTower = t_towerObject;
        _currentTower.GetComponent<BaseTower>().showTargetCircle();
        towerStatus.SetActive(true);
        _name.text = _currentTower.GetComponent<BaseTower>().GetType().ToString();

        foreach (TowerSynposis name in _synopses)
            if (name._name == _name.text)
                _synopsis.text = name._description;
        _image.sprite = _currentTower.GetComponent<SpriteRenderer>().sprite;

        GetComponent<LevelText>().UIUpdate();

        _price.text = "Sale Price: " + Mathf.RoundToInt(_currentTower.GetComponent<BaseTower>().getCost() * .7f);

        _rangeUpgrade.onClick.AddListener(_currentTower.GetComponent<BaseTower>().tryUpgradeRange);
        _rangeUpgrade.onClick.AddListener(GetComponent<LevelText>().UIUpdate);

        _fireRateUpgrade.onClick.AddListener(_currentTower.GetComponent<BaseTower>().tryUpgradeFireRate);
        _fireRateUpgrade.onClick.AddListener(GetComponent<LevelText>().UIUpdate);
    }

    public void sellTower()
    {
        if (_currentTower)
        {
            GetComponent<MoneyManager>().soldTower(_currentTower.GetComponent<BaseTower>().getCost());
            Destroy(_currentTower);
            hide();

            TowerEvent sellEvent = new TowerEvent();
            sellEvent.id = EventID.TOWER_SELL;
            sellEvent.type = TowerType.DART;
            sellEvent.UID = _currentTower.GetInstanceID();
            sellEvent.value = _currentTower.GetComponent<BaseTower>().getCost();
            sellEvent.position = _currentTower.GetComponent<Transform>().position;
            _am.Send(JsonUtility.ToJson(sellEvent));
        }           
    }

    public void hide()
    {
        towerStatus.SetActive(false);
        if (_currentTower)
            _currentTower.GetComponent<BaseTower>().hideTargetCirlce();
        _currentTower = null;
        _rangeUpgrade.onClick.RemoveAllListeners();
        _fireRateUpgrade.onClick.RemoveAllListeners();
    }

    public GameObject getActiveTower()
    {
        return _currentTower;
    }
}
