using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUIControl : MonoBehaviour//这个代码就放在这，关联性很低的
{
    // Start is called before the first frame update


    public GameObject ListUI;
    
    public GameObject HomeUI;

    public HomeUIControl UIScript;
    void Start()
    {
        HomeUI.gameObject.SetActive(true);
        ListUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (UIScript.isList && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            UIScript.gameObject.SetActive(true);
            ListUI.gameObject.SetActive(false);
        }
    }
}
