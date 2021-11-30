using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTower : BaseTower
{

    Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetInteger("attackAngle", 1);
    }

    public override void Fire()
    {
        if (_waitToFire <= 0)
        {
            _animator.SetTrigger("Attack");
            Vector2 velocity = new Vector2();
            _waitToFire = _reloadTime;
            if (_target != null)
                velocity = _targetingSystem.getVelocity(_target.transform.position, transform.position);
            if (_projectile != null)
            {

                _animator.SetInteger("attackAngle", _targetingSystem.getQuadrant(velocity));  
                GameObject go = Instantiate(_projectile, transform.position, Quaternion.identity, transform);
                go.GetComponent<BaseProjectile>().Move(velocity);
            }
        }
    }

    private void Update()
    {
        if (_waitToFire > 0)
            _waitToFire -= Time.deltaTime;
        
        if (_target)
        {
            //float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg + 90;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        }
        else
        {
            _animator.SetTrigger("Idle");
        }
    }

}
