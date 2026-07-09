using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUIControl : MonoBehaviour
{
    [SerializeField] private Text Normaled;
    [SerializeField] private Text Hard;



    Button LeftBtn;
    Button RightBtn;
    Button BackBtn;          

    private void Awake()
    {

        Normaled = GetComponent<Text>();
        Hard = GetComponent<Text>();

        LeftBtn = transform.GetChild(0).GetComponent<Button>();        
        RightBtn = transform.GetChild(1).GetComponent<Button>();      
        BackBtn = transform.GetChild(2).GetComponent<Button>();      


        LeftBtn.onClick.AddListener(Left);
        RightBtn.onClick.AddListener(Right);
        BackBtn.onClick.AddListener(Back);     
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Left()
    {
        SceneManager.LoadScene("SampleScene");
    }


    void Right()
    {
        SceneManager.LoadScene("Game2");
    }


    void Back()
    {
        gameObject.SetActive(false);
    }


}
