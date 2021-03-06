using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTower : BaseTower
{
    float _speed = 1f;
    Animator _animator;

    public override void Awake()
    {
        base.Awake();
        cost = 50;
        _animator = GetComponent<Animator>();
        _animator.SetInteger("attackAngle", 1);
    }

    public override void Fire()
    {
        if (_waitToFire <= 0)
        {
            _animator.SetTrigger("Attack");
            Vector2 velocity = new Vector2();

            if (_targetSystem.targets[0] == null) return;

            velocity = _targetingSystem.getVelocity(_targetSystem.targets[0].transform.position, transform.position);

            if (_projectile)
            {
                GameObject go = Instantiate(_projectile, transform.position, Quaternion.identity);
                go.GetComponent<BaseProjectile>().Move(velocity * _speed);
                _waitToFire = _reloadTime;
            }
        }
    }

    private void Update()
    {
        if (_waitToFire > -0.1f)
            _waitToFire -= Time.deltaTime;
        else
            _animator.SetTrigger("Idle");

        if (_targetSystem.targets.Count > 0)
        {
            Vector2 diff = new Vector2();
            if (_targetSystem.targets[0] ?? false)
                diff = _targetSystem.targets[0].transform.position - transform.position;

            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90.0f;
            transform.eulerAngles = new Vector3(0, 0, angle);

            Fire();
        }
    }
}
