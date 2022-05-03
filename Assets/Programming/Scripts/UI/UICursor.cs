using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICursor : MonoBehaviour
{
    GameObject inspectCursor;
  
    Image image;

    private void Awake()
    {
        inspectCursor = transform.GetChild(0).gameObject;
        image = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        inspectCursor.SetActive(false);


    }
    private void Update()
    {
        transform.position = Input.mousePosition;

    }

    public void ActivateInspectModeCursor()
    {
        inspectCursor.SetActive(true);
        image.enabled = false;
    }

    public void ExitInspectModeCursor()
    {
        image.enabled = true;
        inspectCursor.SetActive(false);
    }
}
