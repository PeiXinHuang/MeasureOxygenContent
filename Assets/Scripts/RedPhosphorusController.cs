using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPhosphorusController : ModelController
{
    [SerializeField]
    private GameObject HatObj = null;//瓶盖

    private GameObject Spoon = null; //药匙
    
    [SerializeField]
    private GameObject SpoonPos = null; //药匙位置

    [SerializeField]
    private GameObject HatPos = null; //瓶盖位置

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.Find("SenceController").GetComponent<SenceController>().isCreateModel)
            return;
        if(other.GetComponent<ModelController>()
            && other.GetComponent<ModelController>().TYPE == SenceData.MODELTYPE.DISPENSINGSPOON //药匙进入
            && !other.GetComponent<DispensingSpoonController>().hasGetDrug //没有药物
            )
        {
            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(4.5f);
            other.GetComponent<DispensingSpoonController>().hasGetDrug = true;
            Spoon = other.gameObject;
            SetBoxCollider();
            PlayAni(other.gameObject);
            Invoke("ShowSpoonDrag", 2.5f);
        }
    }

    //播放动画
    private void PlayAni(GameObject spoon)
    {
        Vector3 hatOriPos = HatObj.transform.position; 
        LeanTween.rotateY(HatObj, 360, 0.5f);
        LeanTween.move(spoon, SpoonPos.transform.position, 0.5f);
        LeanTween.move(HatObj, new LTBezierPath(new Vector3[]{
            hatOriPos,hatOriPos+new Vector3(0,1,0),
            hatOriPos,hatOriPos+new Vector3(0,1,0),

            hatOriPos+new Vector3(0,1,0), hatOriPos+new Vector3(1,1,0),
            hatOriPos+new Vector3(0,1,0), hatOriPos+new Vector3(1,1,0),

            hatOriPos+new Vector3(1,1,0),HatPos.transform.position,
            hatOriPos+new Vector3(1,1,0),HatPos.transform.position,
        }), 
        1.0f).setDelay(0.5f);

        LeanTween.rotateZ(HatObj, 180, 0.5f).setDelay(0.8f);


        LeanTween.rotateZ(spoon, 80, 0.5f).setDelay(1.5f);
        LeanTween.moveY(spoon, SpoonPos.transform.position.y - 3, 0.5f).setDelay(2.0f);
        LeanTween.moveY(spoon, SpoonPos.transform.position.y, 0.5f).setDelay(2.5f);

        LeanTween.rotateZ(spoon, 0, 0.5f).setDelay(3.0f);

        LeanTween.move(HatObj, new LTBezierPath(new Vector3[]{

            HatPos.transform.position,hatOriPos+new Vector3(1,1,0),
            HatPos.transform.position,hatOriPos+new Vector3(1,1,0),

            hatOriPos+new Vector3(1,1,0),hatOriPos+new Vector3(0,1,0),
            hatOriPos+new Vector3(1,1,0),hatOriPos+new Vector3(0,1,0),

            hatOriPos+new Vector3(0,1,0),hatOriPos,
            hatOriPos+new Vector3(0,1,0),hatOriPos,

        }),
        1.0f).setDelay(3.5f);
        LeanTween.rotateZ(HatObj, 0, 0.5f).setDelay(3.7f);
    }
    
    //设置碰撞体，使得药匙可以进入试剂瓶
    private void SetBoxCollider()
    {
        this.GetComponents<BoxCollider>()[0].enabled = false;
        Invoke("ResetBoxCollider", 4.5f);
    }
    private void ResetBoxCollider()
    {
        this.GetComponents<BoxCollider>()[0].enabled = true;
    }

    //显示药匙药物
    private void ShowSpoonDrag()
    {
        Spoon.GetComponent<DispensingSpoonController>().SetDrugObj(true);
    }
}
