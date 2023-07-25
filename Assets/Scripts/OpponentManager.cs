using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class OpponentManager : MonoBehaviour
{
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

    private void OnEnable()
    {
        EventManager.CashRegisterTarget += CashRegisterTarget;
        EventManager.OpponentTargetPos += OpponentTargetPos;
    }
    private void OnDisable()
    {
        EventManager.CashRegisterTarget -= CashRegisterTarget;
        EventManager.OpponentTargetPos -= OpponentTargetPos;
    }
    void CashRegisterTarget(Vector3 cashRTarget)
    {
        _cashRegisterTarget = cashRTarget;
    }
    void OpponentTargetPos(Vector3 opponentTarget)
    {
        _opponentTargetPos= opponentTarget;
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
            quantityDemanded = Random.Range(1, 10);
        }
    }

    void Start()
    {
        _animator=GetComponentInChildren<Animator>();
        _agent=GetComponent<NavMeshAgent>();
        _textMeshPro = GetComponent<TextMeshPro>();
        quantityDemanded = Random.Range(2, 10);
        _animator.SetBool("Walking", true);
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
            for (int i = 0; i < quantityDemanded; i++)
            {
                GameObject ýnstantiateMoney = Instantiate(money, transform.position, Quaternion.identity);
            }
        }
    }
    void AnimationControl()
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
    void OpponentTMP()
    {
        _textMeshPro.text = (quantityDemanded-productList.Count).ToString();
    }
}
