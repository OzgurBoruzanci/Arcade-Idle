using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>())
        {
            EventManager.EarningMoney(1);
            Destroy(this.gameObject);
        }
    }
}
