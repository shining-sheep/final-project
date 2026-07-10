using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUIControl : MonoBehaviour
{


    [SerializeField] private GameObject ListUI;
    [SerializeField] private GameObject HomeUI;
    [SerializeField] private HomeUIControl UIScript;
    void Start()
    {
        HomeUI.gameObject.SetActive(true);
        ListUI.gameObject.SetActive(false);
    }

    void Update()
    {

        if (UIScript.isList && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            UIScript.gameObject.SetActive(true);
            ListUI.gameObject.SetActive(false);
            UIScript.isList = false;
        }
    }
}
