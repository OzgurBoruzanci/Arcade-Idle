using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public void DestroyProduct()
    {
        Destroy(this.gameObject, 2);
    }
}
