using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingSystem))]
public class BaseTower : MonoBehaviour
{
    float _waitToFire; // how long until I can fire
    [Tooltip("how long I have to reload")]
    [SerializeField] float _reloadTime = 1;

    [Tooltip("how far the tower can see")] 
    [SerializeField] float _range = 1; 

    GameObject _target; // object I'm firing at 
    TargetingSystem _targetingSystem;

    [Tooltip("Projectile that the tower will fire")]
    [SerializeField]GameObject _projectile; // what im firing


    void Awake()
    {
        _targetingSystem = GetComponent<TargetingSystem>();
        GameObject child = new GameObject();
        child.transform.parent = this.transform;
        RangeDetection rangeDetector = child.AddComponent<RangeDetection>();
        rangeDetector.setRange(_range);
        rangeDetector.OnObjectDetected += objectDetected;
    }

    private void OnDisable()
    {
        transform.GetChild(0).GetComponent<RangeDetection>().OnObjectDetected -= objectDetected;
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

    public void objectDetected(GameObject t_obj)
    {
        _target = t_obj;
        Fire();
    }

    public virtual void Fire()
    {
        if (_waitToFire <= 0)
        {
            Vector2 velocity = new Vector2();
            _waitToFire = _reloadTime;
            if (_target != null)
                velocity = _targetingSystem.getVelocity(_target.transform.position, transform.position);
            if (_projectile != null)
            {
                GameObject go = Instantiate(_projectile, transform);
                go.GetComponent<BaseProjectile>().Move(velocity);                
            }
        }
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
