using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpgrade : Ability
{
    [Tooltip("First index of the array is level 2, default values start at level 1")]
    public float[] rangePerLevel;

    public override void execute(GameObject caller)
    {
        Debug.Log("Upgrade");
        int level = caller.GetComponent<EntityLeveling>().getLevel();
        BaseTower tower = caller.GetComponent<DartTower>();
        if (level < rangePerLevel.Length)
            tower.upgradeRange(rangePerLevel[level]);
    }
}
