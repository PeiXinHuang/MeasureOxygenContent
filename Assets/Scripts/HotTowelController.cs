using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotTowelController : ModelController
{
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.GetComponent<ModelController>()
            &&other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.GASBOTTEL //集气瓶进入
            && !other.GetComponent<GasBottleController>().hasP
            )
        {
            if (other.GetComponent<GasBottleController>().GetConnect()) //装置连接正确
            {
                GameObject obj = GameObject.Find("SenceController").GetComponent<SenceController>().
                    senceData.GetModel(SenceData.MODELTYPE.WATERSTOPCLIP);

                GameObject obj1 = GameObject.Find("SenceController").GetComponent<SenceController>().
                    senceData.GetModel(SenceData.MODELTYPE.BEAKER);

                GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(1);

                if (obj
                    && obj.GetComponent<WaterStopClipController>().isOpen //止水夹是打开的
                    && obj1
                    && obj1.GetComponent<BeakerController>().hasGetWater //烧杯有水
                    )
                {

                    Debug.Log("气泡");
                }


            }
        }
    }
}
