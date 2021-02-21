using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombustionSpoonController : ModelController
{
    [HideInInspector]
    public bool hasGetDrag = false;

    [SerializeField]
    private GameObject DragObj = null;

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.Find("SenceController").GetComponent<SenceController>().isCreateModel)
            return;

        if (other.GetComponent<ModelController>()
            && other.GetComponent<DispensingSpoonController>() //药匙进入
            && other.GetComponent<DispensingSpoonController>().hasGetDrug //药匙带药品

            && !hasGetDrag //没有获取到药物

            && other.GetComponent<ModelController>().GetLeftCenterPos().x>this.GetLeftCenterPos().x  //药匙位置正确
            && other.GetComponent<ModelController>().GetBottomCenterPos().y > this.GetBottomCenterPos().y

            )
        {
            //隐藏药匙药物
            other.GetComponent<DispensingSpoonController>().hasGetDrug = false;
            other.GetComponent<DispensingSpoonController>().SetDrugObj(false);

            //显示药物
            this.hasGetDrag = true;
            DragObj.SetActive(true);

        }
    }



}
