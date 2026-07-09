using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class MenuUIControl : MonoBehaviour
{


    RectMask2D UImask;          //遮罩
    float paddingNum = 465;


    Button FirstBtn;
    Button SecondBtn;
    Button ThirdBtn;
    Button BackBtn;          //再想加什么功能跟我说

    private void Awake()
    {

        FirstBtn = transform.GetChild(0).GetComponent<Button>();        //进入选关
        SecondBtn = transform.GetChild(1).GetComponent<Button>();      //设置
        ThirdBtn = transform.GetChild(2).GetComponent<Button>();         //制作人名单
        BackBtn = transform.GetChild(3).GetComponent<Button>();      //退出游戏（大退）


        FirstBtn.onClick.AddListener(First);
        SecondBtn.onClick.AddListener(Second);
        ThirdBtn.onClick.AddListener(Third);
        BackBtn.onClick.AddListener(Back);     //初始化
    }
    void Start()
    {
        UImask = GetComponent<RectMask2D>();//初始化
    }

    void Update()
    {
        UImask.padding = new Vector4(0, paddingNum, 0, 0);//初始化
        paddingNum -= 5;
        paddingNum = (paddingNum < 0) ? 0 : paddingNum;
    }

    void First()
    {
        SceneManager.LoadScene("SampleScene");
    }


    void Second()
    {
        SceneManager.LoadScene("Game2");
    }

    void Third()
    {
        SceneManager.LoadScene("Game3");
    }

    void Back()
    {
        SceneManager.LoadScene("Home");
    }


}
