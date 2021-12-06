using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseMenu : MonoBehaviour
{
    public int money;
    [SerializeField] GameObject dartTower;
    [SerializeField] GameObject CanonTower;
    [SerializeField] GameObject TackShooter;

    [SerializeField] int dartMonkeyPrice;
    [SerializeField] int tackShooterPrice;
    [SerializeField] int CanonPrice;
    ObjectPlacement objectPlacement;
    [SerializeField] public Text moneyText;


    
    void Start()
    {
        objectPlacement = GetComponent<ObjectPlacement>();
        UpdateMoneyText();
    }

    public void PurchaseTower(string tower)
    {
        switch (tower)
        {
            case "TackShooter" :
            if(money >= tackShooterPrice)
            {
                SubtractMoney(tackShooterPrice);
                objectPlacement.prefab2D = TackShooter;
                objectPlacement.setPreviewObject(TackShooter);
                objectPlacement.setPlacementMode(true);
                // Purchase tower and enable placement mode
            }
            else
            {
                // Not able ot purchase
                Debug.LogWarning(" Not enough money");
            }
            break;

            case "DartMonkey" :
            if(money >= dartMonkeyPrice)
            {
                SubtractMoney(dartMonkeyPrice);
                objectPlacement.prefab2D = dartTower;
                objectPlacement.setPreviewObject(dartTower);
                objectPlacement.setPlacementMode(true);
                // Purchase tower and enable placement mode
            }
            else
            {
                Debug.LogWarning(" Not enough money");
                // Not able ot purchase
            }
            break;

            case "Canon" :
            if(money >= CanonPrice)
            {
                SubtractMoney(CanonPrice);
                objectPlacement.prefab2D = CanonTower;
                objectPlacement.setPreviewObject(CanonTower);
                objectPlacement.setPlacementMode(true);
                // Purchase tower and enable placement mode
            }
            else
            {
                Debug.LogWarning(" Not enough money");
                // Not able ot purchase
            }
            break;
            
            default:
            break;
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
         moneyText.text = $"${ money}";
    }
}
