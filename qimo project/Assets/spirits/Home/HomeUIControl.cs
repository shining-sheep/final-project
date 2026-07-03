using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HomeUIControl : MonoBehaviour
{


    RectMask2D UImask;          //遮罩
    float paddingNum = 684;

    [Header("制作人名单管理")]
    public bool isList = false;
    public GameObject ListUI;

    Button startBtn;
    Button settingBtn;
    Button listBtn;
    Button bigbackBtn;          //再想加什么功能跟我说

    private void Awake()
    {

        startBtn = transform.GetChild(0).GetComponent<Button>();        //进入选关
        settingBtn = transform.GetChild(1).GetComponent<Button>();      //设置
        listBtn = transform.GetChild(2).GetComponent<Button>();         //制作人名单
        bigbackBtn = transform.GetChild(3).GetComponent<Button>();      //退出游戏（大退）


        startBtn.onClick.AddListener(start);
        settingBtn.onClick.AddListener(Setting);
        listBtn.onClick.AddListener(List);
        bigbackBtn.onClick.AddListener(BigBack);     //初始化
    }
    void Start()
    {
        UImask = GetComponent<RectMask2D>();//初始化
        ListUI.gameObject.SetActive(false);//初始化
    }

    void Update()
    {
        UImask.padding = new Vector4(0, paddingNum, 0, 0);//初始化
        paddingNum -= 6;
        paddingNum = (paddingNum < 0) ? 0 : paddingNum;
    }

    void start()
    {
        SceneManager.LoadScene("Menu");//到时候可以创建一个Menu选关界面
    }


    void Setting()
    {
        //先不用写，我感觉。
    }

    void List()
    {
        ListUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
        isList = true;
    }

    void BigBack()
    {
        Application.Quit();
        Debug.Log("退出游戏");
    }

}