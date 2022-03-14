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
    public int cost = 100;

    protected GameObject _target; // object I'm firing at 
    protected TargetingSystem _targetingSystem;

    [Tooltip("Projectile that the tower will fire")]
    [SerializeField] protected GameObject _projectile; // what im firing

    protected RangeDetection _targetSystem;


    void Awake()
    {
        _targetingSystem = GetComponent<TargetingSystem>();
        GameObject child = new GameObject();
        child.transform.parent = this.transform;
        child.name = "Range Detector";
        child.transform.position = transform.position;
        _targetSystem = child.AddComponent<RangeDetection>();
        _targetSystem.setRange(_range);
        _targetSystem.OnObjectDetected += Fire;
    }

    private void OnDisable()
    {
        transform.GetChild(1).GetComponent<RangeDetection>().OnObjectDetected -= Fire;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("tower"))
        {
            // Block placement
        }
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
    }

    public virtual void upgradeFireRate(float t_rate)
    {
        _reloadTime = t_rate;
    }

    public float getWaitTime()
    {
        return _waitToFire;
    }

    public float getReloadTime()
    {
        return _reloadTime;
    }
}
