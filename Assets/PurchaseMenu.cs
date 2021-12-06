using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseMenu : MonoBehaviour
{
    public int money;
    [SerializeField] int dartMonkeyPrice;
    [SerializeField] int tackShooterPrice;
    [SerializeField] int CanonPrice;

    public void PurchaseTower(string tower)
    {
        switch (tower)
        {
            case "TackShooter" :
            break;

            case "DartMonkey" :
            break;

            case "Canon" :
            break;
            
            default:
        }
    }
}
