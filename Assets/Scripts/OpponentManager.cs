using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentManager : MonoBehaviour
{
    public GameObject cashRegisterTarget;
    public GameObject money;
    public List<GameObject> productList;
    NavMeshAgent _agent;
    Animator _animator;
    Vector3 _startPosition;
    int _quantityDemanded;
    float _listLine;
    public bool _shoppingIsOver;

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
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CashRegister>())
        {
            _animator.SetBool("Walking", true);
            if (transform.childCount == _quantityDemanded)
            {
                _shoppingIsOver = true;

            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CashRegister>())
        {
            _animator.SetBool("Walking", false);
        }
    }
    void Start()
    {
        _animator=GetComponentInChildren<Animator>();
        _agent=GetComponent<NavMeshAgent>();
        _quantityDemanded = Random.Range(2, 10);
        _startPosition = transform.position;
    }

    
    void Update()
    {
        if (!_shoppingIsOver)
        {
            _agent.SetDestination(cashRegisterTarget.transform.position);
        }
        else
        {
            _agent.SetDestination(_startPosition);
        }
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
