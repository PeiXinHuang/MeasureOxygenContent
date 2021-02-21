using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholLampController : ModelController
{
    public GameObject fireObj;//火
    public GameObject hatObj;//帽子

    public GameObject HatPos; //帽子位置

    [HideInInspector]
    public bool hasSetHat = false; //是否已经盖上帽子
    [HideInInspector]
    public bool hasSetFire = false;//是否已经点燃火焰


    //private bool hasShowTip = false;



    private Vector3 oldPos;
    private int shakeNum;

    private void OnMouseDrag()
    {
        if (Vector3.Distance(this.transform.position, oldPos) > 1)
        {
            shakeNum++;
            oldPos = this.transform.position;
            if (shakeNum > 3) //摇晃熄灭酒精灯
            {
                //Debug.Log("shaking");
                fireObj.SetActive(false);
                hasSetFire = false;
                shakeNum = 0;
            }
        }
        else
            shakeNum = 0;

    }

    private void OnMouseDown()
    {
        oldPos = this.transform.position;
    
    }

    private void OnMouseUp()
    {
        shakeNum = 0;
    }





    private void OnTriggerEnter(Collider other)
    {

        if (GameObject.Find("SenceController").GetComponent<SenceController>().isCreateModel)
            return;

        if(other.gameObject == hatObj)
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl();
            other.transform.position = HatPos.transform.position;
            this.SetFire(false);
        }

        if (other.name == "matchchild" &&
            other.transform.parent.parent.GetComponent<MatchesController>().hasFireMatch //火柴进入
            && other.GetComponent<ModelController>().GetLeftCenterPos().x>this.transform.position.x)
        {
            this.SetFire(true);
        }

        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == hatObj)
        {
            hasSetHat = false;
        }
    }

    //设置火焰
    private void SetFire(bool isFired)
    {
        if (isFired && !hasSetHat)
        {
            fireObj.SetActive(true);
            hasSetFire = true;
        }

        else if (!isFired&&hasSetFire) //有火焰，盖灭,开始进行错误检查
        {
            fireObj.SetActive(false);
            hasSetFire = false;
            hasSetHat = true;
            Invoke("BeginChargeError", 3);
        }
        else if (!isFired && !hasSetFire)
        {
            hasSetHat = true;
            CancelInvoke(); //没有火焰，盖灭，取消错误检查

        }
    }

    //开始检查是否有错误（灯帽没有盖两次）
    private void BeginChargeError()
    {
        if (hasSetHat)
        {
            //Debug.Log("灯帽应该盖灭两次");
            GameObject.Find("SenceController").GetComponent<UIController>().
                ShowLampTip(Camera.main.WorldToScreenPoint(HatPos.transform.position));
            //hasShowTip = true;
        }
        
    }

    

}
