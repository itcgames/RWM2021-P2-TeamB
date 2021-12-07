using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateOfFireUpgrade : Ability
{
    [Tooltip("First index of the array is level 2, default values start at level 1.")]
    public float[] reloadTimePerLevel;

    public override void execute(GameObject caller)
    {
        Debug.Log("Upgrade");
        int level = caller.GetComponent<EntityLeveling>().getLevel();
        BaseTower tower = caller.GetComponent<DartTower>();
        if (level < reloadTimePerLevel.Length)
            tower.upgradeFireRate(reloadTimePerLevel[level]);
    }
}
