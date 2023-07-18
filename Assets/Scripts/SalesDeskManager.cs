using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SalesDeskManager : MonoBehaviour
{
    public float waitingseconds = 10f;
    Vector3 _salesObjPos;
    public GameObject salesObject;
    float yPos = 0.5f;

    void Start()
    {
        _salesObjPos=new Vector3(transform.position.x,transform.position.y-0.5f,transform.position.z-2);
        StartCoroutine(CreateSalesObject());
    }

    
    void Update()
    {
        
    }
    
    IEnumerator CreateSalesObject()
    {
        while(true) 
        {
            _salesObjPos.y += yPos;
            GameObject salesObj = Instantiate(salesObject, _salesObjPos, Quaternion.identity);
            yield return new WaitForSeconds(waitingseconds);
        }
    }
}
