using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentTarget : MonoBehaviour
{
    void Start()
    {
        EventManager.OpponentTargetPos(transform.position);
    }
}
