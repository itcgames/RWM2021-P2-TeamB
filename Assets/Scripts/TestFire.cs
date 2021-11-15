using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
    public GameObject testprojectile;
    public Vector2 dir;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject newProjectile = Instantiate(testprojectile, this.gameObject.transform.position, Quaternion.identity);
            newProjectile.GetComponent<TestProjectile>().Move(dir);
        }        
    }
}
