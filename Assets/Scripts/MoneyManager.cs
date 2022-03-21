using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text _moneyText;
    private int _balance;

    void Start()
    {
        _balance = 200;
       
        updateText();
    }

    public void updateText()
    {
        _moneyText.text = "Gold: " + $"{_balance}";
    }

    public int balance { get { return _balance; } set { _balance = value; } }
}
