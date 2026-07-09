using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUIControl : MonoBehaviour
{
    [SerializeField] private Text normalText;
    [SerializeField] private Text hardText;

    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject homeUI;


    [SerializeField] private HomeUIControl homeUIScript;



    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button backBtn;   
    
    private bool isHardMode = false;

    void Start()
    {
        homeUI.gameObject.SetActive(true);
        settingUI.gameObject.SetActive(false);


        leftBtn.onClick.AddListener(Left);
        rightBtn.onClick.AddListener(Right);
        backBtn.onClick.AddListener(Back);
    }

    private void UpdateTextDisplay()
    {
        normalText.gameObject.SetActive(!isHardMode);
        hardText.gameObject.SetActive(isHardMode);
    }

    void Left()
    {
        isHardMode = !isHardMode;
        UpdateTextDisplay();
    }

    void Right()
    {
        isHardMode = !isHardMode;
        UpdateTextDisplay();
    }



    void Back()
    {
        settingUI.gameObject.SetActive(false);
        homeUI.gameObject.SetActive(true);
        homeUIScript.isSetting = false;
    }


}
