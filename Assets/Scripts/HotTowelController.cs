using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotTowelController : ModelController
{
    [SerializeField]
    private GameObject HotTowelObj = null; //热毛巾模型

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.GetComponent<ModelController>()
            &&other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GASBOTTEL //集气瓶进入
            && !other.GetComponent<GasBottleController>().hasP
            && this.GetRightCenterPos().x<other.GetComponent<ModelController>().GetLeftCenterPos().x
            )
        {
            
            if (other.GetComponent<GasBottleController>().GetConnect()) //装置连接正确
            {
                GameObject obj = GameObject.Find("SenceController").GetComponent<SenceController>().
                    senceData.GetModel(SenceData.MODELTYPE.WATERSTOPCLIP);

                GameObject obj1 = GameObject.Find("SenceController").GetComponent<SenceController>().
                    senceData.GetModel(SenceData.MODELTYPE.BEAKER);

                GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(4);

                PlayAni(other.GetComponent<GasBottleController>().GetHotTowelPos());

                if (obj
                    && obj.GetComponent<WaterStopClipController>().isOpen //止水夹是打开的
                    && obj1
                    && obj1.GetComponent<BeakerController>().hasGetWater //烧杯有水
                    )
                {
                    obj1.GetComponent<BeakerController>().ShowBubble(); //显示气泡

                }


            }
        }
    }

    //播放热毛巾移动到集气瓶动画
    private void PlayAni(Vector3 pos)
    {
        Vector3 oriPos = this.gameObject.transform.position;

        LeanTween.move(this.gameObject, new LTBezierPath(new Vector3[]
        {
            oriPos,new Vector3(oriPos.x,pos.y,pos.z),
            oriPos,new Vector3(oriPos.x,pos.y,pos.z),

            new Vector3(oriPos.x,pos.y,pos.z),pos,
            new Vector3(oriPos.x,pos.y,pos.z),pos

        }), 0.5f);
       
        LeanTween.rotateZ(this.gameObject,90,0.2f);
        LeanTween.rotateY(this.gameObject,90,0.2f);
        

        LeanTween.move(this.gameObject, new LTBezierPath(new Vector3[]
        {
            pos,new Vector3(oriPos.x,pos.y,pos.z),
            pos,new Vector3(oriPos.x,pos.y,pos.z),

            new Vector3(oriPos.x,pos.y,pos.z), oriPos+new Vector3(-2,0,0),
            new Vector3(oriPos.x,pos.y,pos.z), oriPos+new Vector3(-2,0,0)

        }), 0.5f).setDelay(3.5f);

        LeanTween.rotateZ(this.gameObject, 0, 0.2f).setDelay(3.7f);
        LeanTween.rotateY(this.gameObject, 0, 0.2f).setDelay(3.7f);

    }
}
