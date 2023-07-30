using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI updateSalesDText;
   
    private void OnEnable()
    {
        EventManager.MoneyText += MoneyText;
        EventManager.UpdateSalesDeskText += UpdateSalesDeskText;
        
    }
    private void OnDisable()
    {
        EventManager.MoneyText -= MoneyText;
        EventManager.UpdateSalesDeskText -= UpdateSalesDeskText;
       
    }
    void MoneyText(int money)
    {
        moneyText.text = "Money: " + money;

    }
    void UpdateSalesDeskText(float salesFee)
    {
        updateSalesDText.text = "UPDATE SALES DESK " + salesFee + " MONEY";
    }

}
