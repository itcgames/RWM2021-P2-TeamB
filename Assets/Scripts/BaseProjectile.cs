using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float m_ProjectileSpeed;
    [SerializeField] protected float m_LifeTime;
    [SerializeField] protected Rigidbody2D m_Rigidbody2D;
    [SerializeField] protected int m_Damage;
    private float m_Timer;

    public virtual void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
     
    public virtual void Update()
    {
        m_LifeTime -= Time.deltaTime;

        if(m_LifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("bloon"))
        {
            //other.GetComponent<Bloon>().hit();
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("obstacle"))
        {
            Destroy(this.gameObject);
        }
    }
    public virtual void Move(Vector2 vel)
    {
        SetRotation(vel);
        m_Rigidbody2D.AddForce(vel * m_ProjectileSpeed, ForceMode2D.Impulse);
    }

    public virtual void SetRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
