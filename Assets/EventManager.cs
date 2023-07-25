using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action<Vector3> OpponentTargetPos;
    public static Action<Vector3> CashRegisterTarget;
    public static Action<int> TotalMoney;
    public static Action<int> EarningMoney;
    public static Action<int> MoneyText;
    public static Action<int> PayMoney;
}