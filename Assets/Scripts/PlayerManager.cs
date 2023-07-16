using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int _money=5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SalesDeskManager>())
        {
            EventManager.TotalMoney(_money);
            
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
