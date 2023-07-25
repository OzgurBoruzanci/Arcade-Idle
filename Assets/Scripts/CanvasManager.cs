using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    private void OnEnable()
    {
        EventManager.MoneyText += MoneyText;
    }
    private void OnDisable()
    {
        EventManager.MoneyText -= MoneyText;
    }
    void MoneyText(int money)
    {
        moneyText.text = "Money: " + money;

    }
}
