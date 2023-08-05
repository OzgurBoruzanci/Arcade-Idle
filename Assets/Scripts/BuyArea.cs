using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyArea : MonoBehaviour
{
    int _playerMoney;
    public int salesFee;
    TextMeshPro _salesText;
    public bool isItLocked=true;

    private void OnEnable()
    {
        EventManager.TotalMoney += TotalMoney;
    }
    private void OnDisable()
    {
        EventManager.TotalMoney -= TotalMoney;
    }
    void TotalMoney(int playerMoney)
    {
        _playerMoney = playerMoney;

    }
    void Start()
    {
        _salesText = GetComponent<TextMeshPro>();
        _salesText.text = "SPHERE UNLOCK FOR " + salesFee + " MONEY";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            IsTheMoneyEnough();
        }
    }

    void IsTheMoneyEnough()
    {
        if (_playerMoney >= salesFee && isItLocked)
        {
            isItLocked = false;
            transform.GetChild(0).gameObject.SetActive(true);
            _salesText.text = " ";
            EventManager.PayMoney(salesFee);
        }
    }
}
