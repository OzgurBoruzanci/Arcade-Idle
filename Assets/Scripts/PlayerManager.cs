using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int _money;
    float _listLine;
    Animator _playerAnim;
    public List<GameObject> productList;

    private void OnEnable()
    {
        EventManager.EarningMoney += EarningMoney;
        EventManager.PayMoney += PayMoney;
    }
    private void OnDisable()
    {
        EventManager.EarningMoney -= EarningMoney;
        EventManager.PayMoney -= PayMoney;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BuyArea>())
        {
            EventManager.TotalMoney(_money);
            _playerAnim.SetBool("Waiting", true);
        }
        if (other.GetComponent<CashRegister>())
        {
            if (productList.Count > 0)
            {
                for (int i = 0; i < productList.Count; i++)
                {
                    other.GetComponent<CashRegister>().productList.Add(productList[i]);
                }
                productList.Clear();
                other.GetComponent<CashRegister>().ListEditing();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BuyArea>())
        {
            _playerAnim.SetBool("Waiting", false);
        }
        if (other.GetComponent<CashRegister>())
        {
            other.GetComponent<CashRegister>().ListEditing();
        }
    }
    void EarningMoney(int getMoney)
    {
        _money += getMoney;
        EventManager.MoneyText(_money);
    }
    void PayMoney(int payM)
    {
        _money -= payM;
        EventManager.MoneyText(_money);
    }
    void Start()
    {
        _playerAnim = transform.GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        AnimControl();
        if (transform.GetComponentInChildren<Animator>())
        {
            transform.GetChild(0).transform.localPosition = Vector3.zero;
        }
    }
    public void ListEditing()
    {
        _listLine = 0;
        if (productList.Count > 0)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                productList[i].transform.parent = transform;
                productList[i].transform.localPosition = new Vector3(0, 0.25f + _listLine, -1.5f);
                _listLine += productList[i].gameObject.transform.lossyScale.y;
            }
        }
    }
    void AnimControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerAnim.SetBool("Walking", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _playerAnim.SetBool("Walking", false);
        }
    }
}
