using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderProjectile : BaseProjectile
{
    Vector3 _velocity;
    Vector3 _target;
    float _initialDistance;
    [SerializeField] float _splashRadius;
    [SerializeField] GameObject _particles;
    public override void Awake()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        base.Awake();
    }

    void OnDestroy()
    {
        GameObject go = Instantiate(_particles, transform.position, Quaternion.identity);
        Vector3 pos = go.transform.position;
        pos.z = -1;
        go.transform.position = pos;
    }

    public override void Update()
    {
        float currDistance = Vector2.Distance(_target, transform.position);
        if (currDistance > 0.1f)
        {
            transform.position += _velocity * Time.deltaTime;
            float dist = (_initialDistance - currDistance) / _initialDistance;
            float scale = Mathf.Sin(dist * 3f);
            transform.GetChild(0).transform.localScale = new Vector3(1.5f + (scale * 1.5f), 1.5f + (scale * 1.5f));
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = true;
            Destroy(gameObject, 0.05f);
        }
    }

    public override void Move(Vector2 target)
    {
        _target = target;
        _initialDistance = Vector3.Distance(_target, transform.position);
        _velocity = Vector3.Normalize(_target - transform.position) * m_ProjectileSpeed;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bloon")
        {
            _moneyManager.gainMoney(1);
            Destroy(other.gameObject);
        }
    }
}
