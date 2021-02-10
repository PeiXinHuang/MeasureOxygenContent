using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 橡胶管控制器
/// </summary>

public class RubberHoseController : ModelController
{
    //[HideInInspector]
    //public GameObject GlassTubeObj1 = null; //长导管
    //[HideInInspector]
    //public GameObject GlassTubeObj2 = null; //短导管
    //[HideInInspector]
    //public GameObject WaterStopClipObj = null; //止水夹



    private Vector3 GlassTubeObj1Offset = Vector3.zero; //连接模型之间位置的偏移量

    public void SetGlassTubeObj1Offset(Vector3 pos) //设置模型之间的偏移量
    {
        GlassTubeObj1Offset =  pos - this.transform.position;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.Find("SenceController").GetComponent<SenceController>().isCreateModel)
            return;



        if(other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GLASSTUBE1 //长导管进入
            && !FindLinkModel(other.gameObject) //长导管还未连接
            )
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(1);

            this.transform.position = other.GetComponent<GlassTube1Controller>().GetRubberHodePos();

            //设置连接模型
            this.AddLinkModel(other.gameObject);
            other.GetComponent<ModelController>().AddLinkModel(this.gameObject);
           // GlassTubeObj1 = other.gameObject;
            //other.GetComponent<GlassTube1Controller>().RubberHoseObj = this.gameObject;

            //设置连接模型之间的偏移位置
            //SetGlassTubeObj1Offset(other.gameObject.transform.position);
            //other.GetComponent<GlassTube1Controller>().SetRubberHoseObjOffset(this.transform.position);

        }
    }

    //private void OnDestroy()
    //{
    //    if (GlassTubeObj1)
    //        GlassTubeObj1.GetComponent<GlassTube1Controller>().RubberHoseObj = null ;
    //}

    //private void Update()
    //{
    //    UpdateLinkModelPos();
    //}

    ////该模型正在移动时，刷新连接模型的位置
    //private void UpdateLinkModelPos()
    //{
    //    if (GameObject.Find("SenceController").GetComponent<SenceController>().isMoveModel
    //        && GameObject.Find("SenceController").GetComponent<SenceController>().target == this.gameObject
    //        ) //本模型正在移动
    //    {
    //        if(GlassTubeObj1) //连接的玻璃管一起移动
    //            GlassTubeObj1.transform.position = this.transform.position + GlassTubeObj1Offset;
    //    }

    //}


}
