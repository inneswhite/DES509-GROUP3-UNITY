using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursor : MonoBehaviour
{
    private void Update()
    {
        transform.position = Input.mousePosition;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
