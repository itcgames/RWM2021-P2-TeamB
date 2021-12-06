using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBExplosion : MonoBehaviour
{   
    public float _life;
    private Rigidbody2D _rgb;

 public virtual void Awake()
    {
        _rgb = GetComponent<Rigidbody2D>();
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("CBExplosion onTriggerStay");

        if(other.CompareTag("bloon"))
        {
            //other.GetComponent<Bloon>().Hit();
             Destroy(other.gameObject);
        }
        else if(other.CompareTag("obstacle"))
        {
            Debug.Log("CBExplosion obstacle collision");
            //Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        Debug.Log("CBExplosion update");
        _life -= Time.deltaTime;
        if(_life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
