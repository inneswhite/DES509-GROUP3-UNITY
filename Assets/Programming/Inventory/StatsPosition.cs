using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPosition : MonoBehaviour
{

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}