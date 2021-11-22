using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
    public GameObject testprojectile;
    public Vector2 dir;
    [HideInInspector] public GameObject currentProjectile;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            spawnProjectile(dir);
        }        
    }

    public GameObject spawnProjectile(Vector2 newdir)
    {
        currentProjectile = Instantiate(testprojectile, this.gameObject.transform.position, Quaternion.identity);
        currentProjectile.GetComponent<TestProjectile>().Move(newdir);
        return currentProjectile;
    }
}
