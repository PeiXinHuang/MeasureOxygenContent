using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// UI控制器
/// </summary>
public class UIController : MonoBehaviour
{

    public InputField input;
    public Button[] modelBtn;
    public Image tip;
    public GameObject WaterStopClipTip;

    //搜索模型
    public void SearchModels()
    {
        string key = input.text;
        foreach (Button item in modelBtn)
        {
            if (item.transform.GetChild(0).GetComponent<Text>().text.Contains(key))
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }

    }

    //显示提示
    public void ShowTip(Vector3 pos)
    {
        
        tip.transform.position = pos;
        tip.gameObject.SetActive(true);
        Invoke("HideTip", 2);
    }

    private void HideTip()
    {
        tip.gameObject.SetActive(false);
    }


    //退出程序
    public void ExitApp()
    {
        Application.Quit();
    }

    //显示止水夹开关面板，3s后隐藏
    public void SetWaterStopClipTip(Vector3 pos, bool isOpen)
    {
        WaterStopClipTip.transform.position = pos;
        if (isOpen) //是打开着的
            WaterStopClipTip.transform.GetChild(0).GetComponent<Text>().text = "止水夹已经打开";
        else
            WaterStopClipTip.transform.GetChild(0).GetComponent<Text>().text = "止水夹已经关闭";
       
        WaterStopClipTip.SetActive(true);
        CancelInvoke("HideWaterStopClipTip");
        Invoke("HideWaterStopClipTip", 1.0f); 
    }
    public void UpdateWaterStopClipTip(Vector3 pos)
    {
        WaterStopClipTip.transform.position = pos;
    }
    private void HideWaterStopClipTip()
    {
        WaterStopClipTip.SetActive(false);
    }
}
