using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    float _waitToFire;
    public float _reloadTime;
    public float _range;
    GameObject _target;
    TargetingSystem _targetingSystem;


    void Awake()
    {
        _targetingSystem = GetComponent<TargetingSystem>();
        GameObject child = new GameObject();
        child.transform.parent = this.transform;
        RangeDetection script = child.AddComponent<RangeDetection>();
        script.setRange(_range);
        script.OnObjectDetected += objectDetected;
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
            _waitToFire = _reloadTime;
            Vector2 velocity = _targetingSystem.getVelocity(_target.transform.position, transform.position);
            // pass velocity to bullet
        }
    }
}
