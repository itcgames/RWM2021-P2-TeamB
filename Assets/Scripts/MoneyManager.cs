using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text _moneyText;
    int _balance;
    int _totalSpent;
    int _towersPurchased;

    void Start()
    {
        _balance = 200;
        _totalSpent = 0;
        _towersPurchased = 0;
       
        updateText();
    }

    public void gainMoney(int t_gained)
    {
        _balance += t_gained;
        updateText();
    }

    public void updateText()
    {
        _moneyText.text = "Gold: " + $"{_balance}";
    }

    public void purchasedTower(int t_cost)
    {
        _balance -= t_cost;
        _totalSpent += t_cost;
        _towersPurchased += 1;
        updateText();
    }

    public bool inquire(int t_cost)
    {
        return _balance >= t_cost;
    }

    public int getTotalSpent()
    {
        return _totalSpent;
    }

    public int getTowersPurchased()
    {
        return _towersPurchased;
    }
}
