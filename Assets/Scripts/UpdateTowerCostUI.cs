using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTowerCostUI : MonoBehaviour
{
    private Text _archerTitle;
    private Text _trebTitle;

    public DartTower _archer;
    private int _archerCost;

    public TrebuchetTower _treb;
    private int _trebCost;

    void Awake()
    {
        _archerTitle = GameObject.Find("Canvas/TowerPurchase/ArcherTitle").GetComponent<Text>();
        _trebTitle = GameObject.Find("Canvas/TowerPurchase/TrebuchetTitle").GetComponent<Text>();

        _archerCost = _archer.cost;
        _trebCost = _treb.cost;
        updateCosts();
    }

    public void updateCosts()
    {
        _archerTitle.GetComponent<Text>().text = "Archer: " + _archerCost ;
        _trebTitle.GetComponent<Text>().text = "Trebuchet: " + _trebCost;
    }
}
