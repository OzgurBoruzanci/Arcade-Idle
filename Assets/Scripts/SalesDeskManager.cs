using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SalesDeskManager : MonoBehaviour
{
    public float waitingseconds = 10;
    public int salesFee;
    int _playerMoney;
    Vector3 _salesObjPos;
    public GameObject salesObject;
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
        IsTheMoneyEnough();
        GameObject salesObj = Instantiate(salesObject, _salesObjPos, Quaternion.identity);
        //StartCoroutine(createSalesObject());
    }
    void Start()
    {
        salesText=GetComponent<TextMeshPro>();
        _salesObjPos=new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z-1);
    }

    
    void Update()
    {
        
    }
    void IsTheMoneyEnough()
    {
        if (_playerMoney >= salesFee)
        {
            salesText.text = " ";
        }
    }
    IEnumerator createSalesObject()
    {
        GameObject salesObj= Instantiate(salesObject,_salesObjPos,Quaternion.identity);
        yield return new WaitForSeconds(waitingseconds);
    }
}
