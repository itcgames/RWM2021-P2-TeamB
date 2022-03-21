using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetTower : BaseTower
{
    Animator _animator;

    public override void Awake()
    {
        _range = 5;
        _reloadTime = 2f;
        base.Awake();   
    }

    public override void Fire()
    {
        if (_waitToFire <= 0)
        {
            Vector2 target = new Vector2();

            target = _targetSystem.targets[0].transform.position;

            if (_projectile)
            {    
                GameObject boulder = Instantiate(_projectile, transform.position, Quaternion.identity);
                boulder.GetComponent<BoulderProjectile>().Move(target);
                _waitToFire = _reloadTime;
            }
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
<<<<<<< HEAD

=======
>>>>>>> c535572 (Trebuchet Tower Implemented)
            Fire();
        }
    }
}
