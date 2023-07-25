using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SalesDeskManager : MonoBehaviour
{
    public float waitingseconds = 10f;
    Vector3 _salesObjPos;
    public GameObject salesObject;
    public float salesHeight;
    float listLine;
    public List<GameObject> productList;

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
        _salesObjPos=new Vector3(transform.position.x,transform.position.y-(listLine/2), transform.position.z-2);
        StartCoroutine(CreateSalesObject());
    }
    
    IEnumerator CreateSalesObject()
    {
        while (true) 
        {
            GameObject salesObj = Instantiate(salesObject, _salesObjPos, Quaternion.identity);
            productList.Add(salesObj);
            ListEditing();
            yield return new WaitForSeconds(waitingseconds);
        }
    }
    void ListEditing()
    {
        listLine = 0;
        if (productList.Count > 0)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                productList[i].transform.localPosition = new Vector3(_salesObjPos.x, _salesObjPos.y+listLine, _salesObjPos.z);
                listLine += salesHeight;
            }
        }
    }
}
