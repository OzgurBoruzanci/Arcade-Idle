using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesDeskManager : MonoBehaviour
{
    [SerializeField] SalesDeskUpdateScriptableObject salesDeskSC = null;
    public float waitingseconds;
    Vector3 _salesObjPos;
    public GameObject salesObject;
    float _salesHeight;
    float _listLine;
    int _updateFee = 10;
    int _limitToBeProduced;
    int _productSalesFee;
    bool _clicked;
    public List<GameObject> productList;
    Color _firstColor;

    private void OnEnable()
    {
        EventManager.UpdteSalesDeskBtn += UpdteSalesDeskBtn;
    }
    private void OnDisable()
    {
        EventManager.UpdteSalesDeskBtn -= UpdteSalesDeskBtn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            if (productList.Count > 0)
            {
                for (int i = 0; i < productList.Count; i++)
                {
                    other.GetComponent<PlayerManager>().productList.Add(productList[i]);
                }
                productList.Clear();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            other.GetComponent<PlayerManager>().ListEditing();
        }
    }

    void Start()
    {
        salesDeskSC.LevelSelection();
        _salesHeight = salesObject.transform.lossyScale.y;
        _firstColor =transform.GetComponent<Renderer>().material.color;
        waitingseconds=salesDeskSC.waitingseconds;
        _limitToBeProduced=salesDeskSC.limitToBeProduced;
        _productSalesFee=salesDeskSC.productSalesFee;
        _salesObjPos =new Vector3(transform.position.x,transform.position.y-(_listLine/2), transform.position.z-2);
        StartCoroutine(CreateSalesObject());
        
        EventManager.UnlockedProduct(salesObject.name);
    }
    
    IEnumerator CreateSalesObject()
    {
        while (true) 
        {
            GameObject salesObj = Instantiate(salesObject, _salesObjPos, Quaternion.identity);
            productList.Add(salesObj);
            salesObj.GetComponent<Product>().productFee = _productSalesFee;
            ListEditing();
            yield return new WaitForSeconds(waitingseconds);
        }
    }
    void ListEditing()
    {
        _listLine = 0;
        if (productList.Count > 0)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                productList[i].transform.localPosition = new Vector3(_salesObjPos.x, _salesObjPos.y+_listLine, _salesObjPos.z);
                _listLine += _salesHeight;
            }
        }
    }
    public void UpdteSalesDeskBtn()
    {
        if (PlayerPrefs.GetInt("Money")>=_updateFee && _clicked)
        {
            waitingseconds /= 2;
            EventManager.PayMoney(_updateFee);
            _updateFee +=2;
            EventManager.UpdateSalesDeskText(_updateFee);
        }
    }
    public void Selected()
    {
        _clicked = true;
        transform.GetComponent<Renderer>().material.color = Color.red;
    }
    public void NotSelected()
    {
        _clicked=false;
        transform.GetComponent<Renderer>().material.color = _firstColor;
    }
}
