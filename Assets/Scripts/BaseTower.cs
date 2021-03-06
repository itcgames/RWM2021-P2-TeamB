using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingSystem))]
public class BaseTower : MonoBehaviour
{
    protected float _waitToFire; // how long until I can fire
    [Tooltip("how long I have to reload")]
    [SerializeField] protected float _reloadTime = 1;

    [Tooltip("how far the tower can see")] 
    [SerializeField] protected float _range = 1;

    [Tooltip("How much it costs to place the tower")]
    public int cost;

    float _baseUpgradeCost;
    float _upgradeScale;

    MoneyManager _moneyManager;

    protected GameObject _target; // object I'm firing at 
    protected TargetingSystem _targetingSystem;

    [Tooltip("Projectile that the tower will fire")]
    [SerializeField] protected GameObject _projectile; // what im firing

    protected RangeDetection _targetSystem;

    public Sprite _texture;
    protected SpriteRenderer _targetCircle;

    private AnalyticsManager _am;

    public virtual void Awake()
    {
        _targetingSystem = GetComponent<TargetingSystem>();
        GameObject child = new GameObject();
        child.transform.parent = this.transform;
        child.name = "Range Detector";
        child.transform.position = transform.position;
        _targetSystem = child.AddComponent<RangeDetection>();
        _targetSystem.setRange(_range);

        _targetCircle = child.AddComponent<SpriteRenderer>();
        _targetCircle.sprite = _texture;
        _targetCircle.color = new Color(1f, 1f, 1f, .5f);
        _targetCircle.sortingOrder = 1;
        hideTargetCirlce();

        _baseUpgradeCost = 30f;
        _upgradeScale = 1.2f;

        _moneyManager = GameObject.FindObjectOfType<MoneyManager>();
        _am = GameObject.FindGameObjectsWithTag("Analytics")[0].GetComponent<AnalyticsManager>();
    }

    void Update()
    {
        if (_waitToFire > 0)
            _waitToFire -= Time.deltaTime;
    }

    public virtual void Fire()
    {
        if (_waitToFire <= 0)
        {
            Vector2 velocity = new Vector2();
            _waitToFire = _reloadTime;
            //if (_targetSystem.targets[0] != null)
                //velocity = _targetingSystem.getVelocity(_target.transform.position, transform.position);
            if (_projectile != null)
            {
                //Debug.Log(Mathf.Atan2(velocity.y, velocity.x) * 180 / Mathf.PI);
                GameObject go = Instantiate(_projectile, transform.position, Quaternion.identity);
                go.GetComponent<BaseProjectile>().Move(velocity);                
            }
        }
    }

    public virtual void upgradeRange(float t_range)
    {
        _range = t_range;
        _targetSystem.setRange(t_range);

        TowerEvent upgradeEvent = new TowerEvent();
        upgradeEvent.id = EventID.TOWER_UPGRADE;
        upgradeEvent.type = TowerType.DART;
        upgradeEvent.upgradeType = UpgradeType.RANGE;
        upgradeEvent.UID = this.GetInstanceID();
        upgradeEvent.value = getRangeCost();
        upgradeEvent.position = this.GetComponent<Transform>().position;
        _am.Send(JsonUtility.ToJson(upgradeEvent));
    }

    public virtual void upgradeFireRate(float t_rate)
    {
        _reloadTime = t_rate;

        TowerEvent upgradeEvent = new TowerEvent();
        upgradeEvent.id = EventID.TOWER_UPGRADE;
        upgradeEvent.type = TowerType.DART;
        upgradeEvent.upgradeType = UpgradeType.FIRE_RATE;
        upgradeEvent.UID = this.GetInstanceID();
        upgradeEvent.value = getFireRateCost();
        upgradeEvent.position = this.GetComponent<Transform>().position;
        _am.Send(JsonUtility.ToJson(upgradeEvent));
    }

    public float getWaitTime()
    {
        return _waitToFire;
    }

    public float getReloadTime()
    {
        return _reloadTime;
    }

    public int getCost()
    {
        return cost;
    }

    public void showTargetCircle()
    {
        _targetCircle.enabled = true;  
    }

    public void hideTargetCirlce()
    {
        _targetCircle.enabled = false;
    }

    public virtual void tryUpgradeRange()
    {
        int upgradeCost = getRangeCost();
        if (_moneyManager.inquire(upgradeCost))
        {
            int currLevel = transform.GetChild(1).GetComponent<EntityLeveling>().getLevel();
            transform.GetChild(1).GetComponent<EntityLeveling>().levelUp();
            int newLevel = transform.GetChild(1).GetComponent<EntityLeveling>().getLevel();

            if (newLevel != currLevel)
                _moneyManager.purchaseUpgrade(upgradeCost);
        }
    }

    public virtual void tryUpgradeFireRate()
    {
        int upgradeCost = getRangeCost();
        if (_moneyManager.inquire(upgradeCost))
        {
            int currLevel = transform.GetChild(2).GetComponent<EntityLeveling>().getLevel();
            transform.GetChild(2).GetComponent<EntityLeveling>().levelUp();
            int newLevel = transform.GetChild(2).GetComponent<EntityLeveling>().getLevel();

            if (newLevel != currLevel)
                _moneyManager.purchaseUpgrade(upgradeCost);
        }
    }

    public virtual int getFireRateCost()
    {
        int level = transform.GetChild(2).GetComponent<EntityLeveling>().getLevel();
        return Mathf.RoundToInt(_baseUpgradeCost * Mathf.Pow(_upgradeScale, level));
    }

    public virtual int getRangeCost()
    {
        int level = transform.GetChild(1).GetComponent<EntityLeveling>().getLevel();
        return Mathf.RoundToInt(_baseUpgradeCost * Mathf.Pow(_upgradeScale, level));
    }
}
