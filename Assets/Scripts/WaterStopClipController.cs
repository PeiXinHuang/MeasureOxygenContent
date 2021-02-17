using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStopClipController : ModelController
{
    public bool isOpen = false; //止水夹是否打开


    private Vector3 downPos = Vector3.zero;
    private Vector3 upPos = Vector3.zero;

    private void OnMouseDown()
    {
     

        downPos = this.transform.position;

    }

    private void OnMouseUp()
    {
        upPos = this.transform.position;

        if (Vector3.Distance(downPos, upPos) < 0.05f) //点击而不是拖动模型时才改变止水夹状态
        {
            isOpen = !isOpen; //更改止水夹开关状态

            Vector3 pos = this.transform.position;
            pos = GetScreenPos(pos+new Vector3(0,2.5f,0));

            GameObject.Find("SenceController").GetComponent<UIController>().SetWaterStopClipTip(pos,isOpen);
        }
    }

   
}
