using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistilledWaterController : ModelController
{
    [SerializeField]
    private GameObject hatObj = null; //瓶盖
    [SerializeField]
    private GameObject bodyObj = null;//瓶身
    [SerializeField]
    private GameObject hatObjPos = null;//帽子放置位置


    public void PlayAni(Vector3 pos1)
    {

        Vector3 hatObjPos2 = hatObj.transform.position; //帽子原始位置
        Vector3 bodyObjPos2 = bodyObj.transform.position;//瓶身原始位置

        //帽子放置动画
        LeanTween.move(hatObj, new LTBezierPath(new Vector3[]
        {
            hatObjPos2, hatObjPos2 + new Vector3(0, 2, 0),
            hatObjPos2, hatObjPos2 + new Vector3(0, 2, 0),

            hatObjPos2 + new Vector3(0, 2, 0), hatObjPos2 + new Vector3(3, 2, 0),
            hatObjPos2 + new Vector3(0, 2, 0), hatObjPos2 + new Vector3(3, 2, 0),


            hatObjPos2 + new Vector3(3, 2, 0) ,hatObjPos.transform.position,
            hatObjPos2 + new Vector3(3, 2, 0) ,hatObjPos.transform.position,
        }),
        1.0f);
        LeanTween.rotateZ(hatObj, -180, 0.5f).setDelay(0.3f);


        //瓶身动画
        LeanTween.move(bodyObj, pos1, 0.5f).setDelay(1.0f);
        LeanTween.rotateZ(bodyObj, -70, 0.3f).setDelay(1.5f);
        LeanTween.rotateZ(bodyObj, 0, 0.3f).setDelay(2.7f);
        LeanTween.move(bodyObj, bodyObjPos2, 0.5f).setDelay(3.0f);

        //帽子复原动画
        LeanTween.move(hatObj, new LTBezierPath(new Vector3[]
        {
            hatObjPos.transform.position, hatObjPos2 + new Vector3(3, 2, 0),
            hatObjPos.transform.position, hatObjPos2 + new Vector3(3, 2, 0),

            hatObjPos2 + new Vector3(3, 2, 0),hatObjPos2 + new Vector3(0, 2, 0),
            hatObjPos2 + new Vector3(3, 2, 0),hatObjPos2 + new Vector3(0, 2, 0),

             hatObjPos2 + new Vector3(0, 2, 0), hatObjPos2,
             hatObjPos2 + new Vector3(0, 2, 0), hatObjPos2
        }), 1.0f).setDelay(3.5f);
        LeanTween.rotateZ(hatObj, -360, 0.5f).setDelay(3.7f);



    }

}
