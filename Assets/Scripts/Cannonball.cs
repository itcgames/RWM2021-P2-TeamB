using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : TestProjectile
{
    public GameObject _explosion;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("bloon"))
        {
            //other.GetComponent<Bloon>().hit();
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("obstacle"))
        {
            //Debug.Log("aa");
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
