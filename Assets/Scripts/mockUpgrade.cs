using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mockUpgrade : MonoBehaviour
{
    public EntityLeveling monke;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            monke.levelUp();
    }
}
