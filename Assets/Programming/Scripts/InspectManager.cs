using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D inspectcursor;
    [SerializeField]
    private GameObject inspectorPanel;
    [SerializeField]
    private GameObject[] items;
    private int itemid;
    [SerializeField]
    private Text itemTitle;
    [SerializeField]
    private Text itemDescription;


    //ROTATE OBJECT 
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    private bool isRotated;
    private float speed = 1f;


    public bool isInspect;
    

    // Start is called before the first frame update
    void Start()
    {
        inspectorPanel.SetActive(false);
        items[0].SetActive(false);
        items[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RotateItem();

        if (isInspect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("asa");
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.collider.gameObject.tag == "Medicine")
                    {
                        ShowMedicine();
                    }
                    if (hit.collider.gameObject.tag == "Syringe")
                    {
                        ShowSyringe();
                        
                    }
                }
            }
        }
    }

    public void ShowMedicine()
    {
        itemid = 0;
        Time.timeScale = 0f;
        inspectorPanel.SetActive(true);
        items[0].SetActive(true);
        string title = "Medicine";
        itemTitle.text = title;
        string description = "I am used to treat wounds";
        itemDescription.text = description;
    }

    public void ShowSyringe()
    {
        itemid = 1;
        Time.timeScale = 0f;
        inspectorPanel.SetActive(true);
        items[1].SetActive(true);
        string title2 = "Syringe";
        itemTitle.text = title2;
        string description2 = "I am used as an injection";
        itemDescription.text = description2;
    }

    public void Continue()
    {
        // DISABLE ITEMS 
        items[0].SetActive(false);
        items[1].SetActive(false);
        inspectorPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void InspectMode()
    {
        isInspect = !isInspect;
        if(isInspect)
        {
            Cursor.SetCursor(inspectcursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public void RotateItem()
    {
        if (itemid == 0)
        {
            if (isRotated)
            {
                mPosDelta = Input.mousePosition - mPrevPos;
                items[0].transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
            mPrevPos = Input.mousePosition;
        }
        if (itemid == 1)
        {
            if (isRotated)
            {
                mPosDelta = Input.mousePosition - mPrevPos;
                items[1].transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
            mPrevPos = Input.mousePosition;
        }
    }


    public void TogglePressed(bool value)
    {
        value = true;
        isRotated = value;
    }
    public void Released(bool value)
    {
        value = false;
        isRotated = value;
    }
}
