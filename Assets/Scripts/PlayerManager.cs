using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int _money = 5;
    float _listLine;
    Animator _playerAnim;
    public List<GameObject> productList;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BuyArea>())
        {
            EventManager.TotalMoney(_money);
            _playerAnim.SetBool("Walking", true);
        }
        if (other.GetComponent<Product>())
        {
            _listLine = 0;
            productList.Add(other.gameObject);
            other.transform.parent = transform;
            ListEditing();
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BuyArea>())
        {
            _playerAnim.SetBool("Walking", false);
        }
    }

    void Start()
    {
        _playerAnim = transform.GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        Debug.Log(productList.Count);
    }
    void ListEditing()
    {
        if (productList.Count > 0)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                if (productList[i].transform.parent==transform)
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
