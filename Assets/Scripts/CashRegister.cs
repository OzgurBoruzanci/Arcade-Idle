using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public List<GameObject> productList;
    float _listLine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Product>())
        {
            _listLine = 0;
            productList.Add(other.gameObject);
            other.transform.parent = transform;
            ListEditing();
        }
    }
    void Start()
    {
        
    }

    void Update()
    {

    }
    void ListEditing()
    {
        if (productList.Count > 0)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                if (productList[i].transform.parent == transform)
                {
                    productList[i].transform.localPosition = new Vector3(0, 0.25f + _listLine, -1.5f);
                    _listLine += 0.5f;
                }
                else
                {
                    productList.RemoveAt(i);
                }
            }
        }
    }
}
