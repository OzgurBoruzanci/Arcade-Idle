using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyArea : MonoBehaviour
{
    int _playerMoney;
    public int salesFee;
    TextMeshPro salesText;

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
        salesText = GetComponent<TextMeshPro>();

    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (_playerMoney >= salesFee)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            salesText.text = " ";
        }
    }
}
