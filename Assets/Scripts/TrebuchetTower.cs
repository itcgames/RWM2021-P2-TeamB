using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetTower : BaseTower
{
    float _speed = 1f;
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Fire()
    {
        if (_waitToFire <= 0)
        {

        }
    }

    void Update()
    {
        if (_waitToFire > -0.1f)
            _waitToFire -= Time.deltaTime;
        //else
        //    _animator.SetTrigger("Idle");

        if (_targetSystem.targets.Count > 0)
        {
            Vector2 diff = _targetSystem.targets[0].transform.position - transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90.0f;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
