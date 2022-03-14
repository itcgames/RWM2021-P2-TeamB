using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    int _balance = 200;

    public int balance { get { return _balance; } set { _balance = value; } }
}
