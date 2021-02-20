using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispensingSpoonController : ModelController
{
    [HideInInspector]
    public bool hasGetDrug = false;

    [SerializeField]
    private GameObject DrugObj = null;

    //设置药物是否显示
    public void SetDrugObj(bool isShow)
    {
        DrugObj.SetActive(isShow);

       
    }

}
