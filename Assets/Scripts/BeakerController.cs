using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakerController : ModelController
{
    public bool hasGetWater = false;

    [SerializeField]
    private GameObject WaterObj = null; //水

    [SerializeField]
    private GameObject DistilledWaterPos = null; //蒸馏水倾倒位置

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ModelController>() //玻璃管进入
            && other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE1
            && !this.FindLinkModel(other.gameObject)
            && other.GetComponent<ModelController>().GetRightCenterPos().x<this.GetRightCenterPos().x //碰撞到的是之间碰撞器
            )
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(1);
            this.transform.position = other.GetComponent<GlassTube1Controller>().getBeakerObjPos();

            other.GetComponent<ModelController>().UpdateLinkModelPos();

            this.AddLinkModel(other.gameObject);
            other.GetComponent<ModelController>().AddLinkModel(this.gameObject);
        }
        else if(other.GetComponent<ModelController>() //蒸馏水进入
            &&other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.DISTILLEDWATER
            &&!this.hasGetWater
            )
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(4.5f);
            this.hasGetWater = true;
            Invoke("ShowWater", 2.8f);

            other.GetComponent<DistilledWaterController>().PlayAni(
                      DistilledWaterPos.transform.position);

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (
            other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE1 //短导管离开
           && FindLinkModel(other.gameObject) //已连接
           )
        {
            //去除连接模型
            this.ReMoveLinkModel(other.gameObject);
            other.GetComponent<ModelController>().ReMoveLinkModel(this.gameObject);
        }
    }

    //显示水模型
    private void ShowWater()
    {
        WaterObj.SetActive(true);
    }
}
