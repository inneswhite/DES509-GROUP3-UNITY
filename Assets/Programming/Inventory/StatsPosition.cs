using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPosition : MonoBehaviour
{

    void LateUpdate()
    {
        transform.position = Input.mousePosition;
    }
}
