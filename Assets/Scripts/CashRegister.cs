using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public List<GameObject> productList;
    float _listLine;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<OpponentManager>())
        {
            if (productList.Count > 0)
            {
                for (int i = 0; i < productList.Count; i++)
                {
                    if (i < other.GetComponent<OpponentManager>().quantityDemanded)
                    {
                        other.GetComponent<OpponentManager>().AddProductList(productList[i]);
                        productList.RemoveAt(i);
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<OpponentManager>())
        {
            other.GetComponent<OpponentManager>().ListEditing();
            ListEditing();
        }
    }
    void Start()
    {
        EventManager.CashRegisterTarget(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1));
    }

    
    public void ListEditing()
    {
        _listLine = 0;
        if (productList.Count > 0)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                productList[i].transform.parent = transform;
                productList[i].transform.localPosition = new Vector3(-1.5f, 0.25f + _listLine, -1.5f);
                _listLine += productList[i].gameObject.transform.lossyScale.y;
            }
        }
       
    }
}
