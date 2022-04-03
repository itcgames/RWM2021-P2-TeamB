using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpgrade : Ability
{
    [Tooltip("First index of the array is level 2, default values start at level 1")]
    public float[] rangePerLevel;

    public override void execute(GameObject caller)
    {
        GameObject towerObj = caller.transform.parent.gameObject;
        int level = caller.GetComponent<EntityLeveling>().getLevel() - 2;
        BaseTower tower = towerObj.GetComponent<BaseTower>();
        if (level < rangePerLevel.Length)
            tower.upgradeRange(rangePerLevel[level]);
    }
}
