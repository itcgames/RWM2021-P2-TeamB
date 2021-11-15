using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public float lifeTime;
    public Vector2 velocity;
    public Rigidbody2D rb;
    private float timer;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRotation(velocity);
    }
     
    public virtual void Update()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public virtual void Move()
    {
        rb.AddForce(velocity * projectileSpeed, ForceMode2D.Force);
    }

    public virtual void SetRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
