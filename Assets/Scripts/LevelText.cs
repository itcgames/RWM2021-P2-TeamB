using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    [SerializeField] Text _rangeText;
    [SerializeField] Text _fireRateText;

    [SerializeField] Text _fireRateButtonText;
    [SerializeField] Text _rangeButtonText;

    public void UIUpdate()
    {
        TowerManager towerManager = GetComponent<TowerManager>();
        if (towerManager.getActiveTower())
        {
            float rangeLevel = towerManager.getActiveTower().transform.GetChild(1).GetComponent<EntityLeveling>().getLevel();
            _rangeText.text = "Range Lv" + rangeLevel;

            float fireRateLevel = towerManager.getActiveTower().transform.GetChild(2).GetComponent<EntityLeveling>().getLevel();
            _fireRateText.text = "Fire Rate Lv" + fireRateLevel;

            _fireRateButtonText.text = "Upgrade $" + towerManager.getActiveTower().GetComponent<BaseTower>().getFireRateCost();
            _rangeButtonText.text = "Upgrade $" + towerManager.getActiveTower().GetComponent<BaseTower>().getRangeCost();
        }
    }
}
