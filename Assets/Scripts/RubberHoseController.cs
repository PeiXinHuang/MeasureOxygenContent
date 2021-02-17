using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 橡胶管控制器
/// </summary>

public class RubberHoseController : ModelController
{

    [SerializeField]
    private GameObject Tube1ObjPos = null; 
    [SerializeField]
    private GameObject Tube2ObjPos = null;
    [SerializeField]
    private GameObject WaterStopClipObjPos = null;


    private void OnTriggerEnter(Collider other)
    {
        //if (GameObject.Find("SenceController").GetComponent<SenceController>().isCreateModel)
        //    return;



        if (other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE1 //长导管进入
            && !FindLinkModel(other.gameObject) //长导管还未连接
            &&Vector3.Distance(this.GetRightCenterPos(),other.GetComponent<ModelController>().GetLeftCenterPos())<0.8f //与接触点距离较近
            )
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(1);

            other.transform.position = Tube1ObjPos.transform.position;
            other.GetComponent<ModelController>().UpdateLinkModelPos();
            //this.transform.position = other.GetComponent<GlassTube1Controller>().GetRubberHodePos();

            //设置连接模型
            this.AddLinkModel(other.gameObject);
            other.GetComponent<ModelController>().AddLinkModel(this.gameObject);
        }
        else if(other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE2 //短导管进入
            && !FindLinkModel(other.gameObject) //长导管还未连接
            && Vector3.Distance(this.GetLeftCenterPos(), other.GetComponent<ModelController>().GetRightCenterPos()) < 0.8f //与接触点距离较近)
            )
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(1);
            other.transform.position = Tube2ObjPos.transform.position;
            other.GetComponent<ModelController>().UpdateLinkModelPos();
            this.AddLinkModel(other.gameObject);
            other.GetComponent<ModelController>().AddLinkModel(this.gameObject);
        }
        else if (other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.WATERSTOPCLIP //止水夹进入
           && !FindLinkModel(other.gameObject) //止水夹还未连接
           && Vector3.Distance(this.GetTopCenterPos(), other.GetComponent<ModelController>().GetBottomCenterPos()) < 0.8f //与接触点距离较近)
           )
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(1);
            other.transform.position = WaterStopClipObjPos.transform.position;
            other.GetComponent<ModelController>().UpdateLinkModelPos();
            this.AddLinkModel(other.gameObject);
            other.GetComponent<ModelController>().AddLinkModel(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (
            (other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE1 //长导管离开
            || other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE2 //短导管离开
            || other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.WATERSTOPCLIP //止水夹离开
            )
           && 
           FindLinkModel(other.gameObject) //已连接
           )
        {
            //去除连接模型
            this.ReMoveLinkModel(other.gameObject);
            other.GetComponent<ModelController>().ReMoveLinkModel(this.gameObject);
        }
    }




   

}
