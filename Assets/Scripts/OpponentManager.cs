using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEditor.PackageManager.Requests;
using System.Diagnostics;

public class OpponentManager : MonoBehaviour
{
    List<string> _unlockedProductsList;
    TextMeshPro _textMeshPro;
    Vector3 _cashRegisterTarget;
    public GameObject money;
    public List<GameObject> productList;
    NavMeshAgent _agent;
    Animator _animator;
    Vector3 _opponentTargetPos;
    public int quantityDemanded;
    float _listLine;
    public bool _shoppingIsOver;
    public string chooseProductName;
    int _moneyToBePaid;

    private void OnEnable()
    {
        EventManager.CashRegisterTarget += CashRegisterTarget;
        EventManager.OpponentTargetPos += OpponentTargetPos;
        EventManager.UnlockedProduct += UnlockedProduct;
    }
    private void OnDisable()
    {
        EventManager.CashRegisterTarget -= CashRegisterTarget;
        EventManager.OpponentTargetPos -= OpponentTargetPos;
        EventManager.UnlockedProduct -= UnlockedProduct;
    }
    void CashRegisterTarget(Vector3 cashRTarget)
    {
        _cashRegisterTarget = cashRTarget;
    }
    void OpponentTargetPos(Vector3 opponentTarget)
    {
        _opponentTargetPos= opponentTarget;
    }
    void UnlockedProduct(string product)
    {
        UnityEngine.Debug.Log(product + " " + _unlockedProductsList.Count);
        _unlockedProductsList.Add(product);
        ChooseProduct();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OpponentTarget>())
        {
            if (productList.Count > 0)
            {
                for (int i = 0; i < productList.Count; i++)
                {
                    productList[i].transform.parent = null;
                    productList[i].GetComponent<Product>().DestroyProduct();
                    
                }
                productList.Clear();
                _shoppingIsOver = false;
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CashRegister>())
        {
            _animator.SetBool("Waiting", true);
            if (productList.Count >= quantityDemanded)
            {
                _shoppingIsOver = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CashRegister>())
        {
            _animator.SetBool("Waiting", false);
            PayMoney();
        }
        if (other.GetComponent<OpponentTarget>())
        {
            //quantityDemanded = Random.Range(1, 10);
            ChooseProduct();
        }
    }

    void Start()
    {
        _animator=GetComponentInChildren<Animator>();
        _agent=GetComponent<NavMeshAgent>();
        _textMeshPro = GetComponent<TextMeshPro>();
        _animator.SetBool("Walking", true);
        _unlockedProductsList=new List<string>();
        
    }

    
    void Update()
    {
        AnimationControl();
        OpponentTMP();
    }
    public void AddProductList(GameObject product)
    {
        if (productList.Count< quantityDemanded)
        {
            productList.Add(product);
        }
    }
    public void ListEditing()
    {
        if (productList.Count > 0)
        {
            _listLine = 0;
            for (int i = 0; i < productList.Count; i++)
            {
                productList[i].transform.parent = transform;
                productList[i].transform.localPosition = new Vector3(0, 0.25f + _listLine, -1.5f);
                _listLine += productList[i].gameObject.transform.lossyScale.y;
            }
        }
    }
    void PayMoney()
    {
        if (_shoppingIsOver)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                _moneyToBePaid += productList[i].GetComponent<Product>().productFee;
            }
            for (int i = 0; i < _moneyToBePaid; i++)
            {
                GameObject instantiateMoney = Instantiate(money, transform.position, Quaternion.identity);
            }
        }
    }
    void AnimationControl()
    {
        if (_unlockedProductsList.Count>0)
        {
            if (!_shoppingIsOver)
            {
                _agent.SetDestination(_cashRegisterTarget);
            }
            else
            {
                _agent.SetDestination(_opponentTargetPos);
            }
        }
    }
    void OpponentTMP()
    {
        _textMeshPro.text = (quantityDemanded - productList.Count).ToString() + " " + chooseProductName;
    }

    void ChooseProduct()
    {
        quantityDemanded = Random.Range(2, 10);
        int chooseNum= Random.Range(0, _unlockedProductsList.Count-1);
        UnityEngine.Debug.Log(chooseNum);
        chooseProductName= _unlockedProductsList[chooseNum];
    }
}
