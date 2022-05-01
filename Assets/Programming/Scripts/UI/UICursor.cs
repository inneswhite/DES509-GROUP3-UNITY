using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursor : MonoBehaviour
{

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

    }
    private void Update()
    {
        transform.position = Input.mousePosition;

    }
}
