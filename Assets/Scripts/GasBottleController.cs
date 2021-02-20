using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBottleController : ModelController
{
    [HideInInspector]
    public  bool hasP = false; //是否有白磷

    [SerializeField]
    private GameObject HotTowelPos = null; //热毛巾位置

    private void OnTriggerEnter(Collider other)
    {
         if (other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE2 //短导管进入
           && !FindLinkModel(other.gameObject) //短导管还未连接
           )
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(1);
            this.transform.position = other.GetComponent<GlassTube2Controller>().getGasBottleObjPos();

            //other.GetComponent<ModelController>().MoveModelToPos(Tube2ObjPos.transform.position);
            other.GetComponent<ModelController>().UpdateLinkModelPos();
            this.AddLinkModel(other.gameObject);
            other.GetComponent<ModelController>().AddLinkModel(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (
            other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE2 //短导管离开
           &&FindLinkModel(other.gameObject) //已连接
           )
        {
            //去除连接模型
            this.ReMoveLinkModel(other.gameObject);
            other.GetComponent<ModelController>().ReMoveLinkModel(this.gameObject);
        }
    }

    //判断装置是否连接完成
    public bool GetConnect()
    {
        GameObject obj = FindLinkModel(SenceData.MODELTYPE.GLASSTUBE2);
        if (!obj)
            return false;

        obj = obj.GetComponent<ModelController>().FindLinkModel(SenceData.MODELTYPE.RUBBERHOSE);
        if (!obj)
            return false;

        GameObject obj1 = obj.GetComponent<ModelController>().FindLinkModel(SenceData.MODELTYPE.WATERSTOPCLIP);
        if (!obj1)
            return false;

        obj = obj.GetComponent<ModelController>().FindLinkModel(SenceData.MODELTYPE.GLASSTUBE1);
        if (!obj)
            return false;

        obj = obj.GetComponent<ModelController>().FindLinkModel(SenceData.MODELTYPE.BEAKER);
        if (!obj)
            return false;

        return true;
    }

    //获取热毛巾的位置
    public Vector3 GetHotTowelPos()
    {
        return this.HotTowelPos.transform.position;
    }
}
