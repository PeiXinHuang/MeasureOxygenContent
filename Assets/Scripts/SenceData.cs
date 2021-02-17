using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenceData : MonoBehaviour
{
  
    public enum MODELTYPE //模型类型
    {
        //根据项目需求修改
        GASBOTTEL,
        GLASSTUBE1,
        GLASSTUBE2,
        RUBBERHOSE,
        WATERSTOPCLIP,
        BEAKER,
        MAKINGPEN,
        REDPHOSPHORUS,
        HOTTOWEL,
        COMBUSTIONSPOON,
        MATCHES,
        ALOOHOLLAMP,
        DISPENSINGSPOON,
        DISTILLEDWATER
    }

   
    public Dictionary<MODELTYPE, int> modelMaxNum = new Dictionary<MODELTYPE, int>(); //模型最大值字典
    private Dictionary<MODELTYPE, int> modelNum = new Dictionary<MODELTYPE, int>(); //模型数目字典

    public List<GameObject> models; //保存模型对象到列表之中

    
   

    private void Start()
    {
        InitData();
    }

    //初始化数据,默认每种模型最大数量为1，开始数量为0
    private void InitData()
    {

        foreach (MODELTYPE item in Enum.GetValues(typeof(MODELTYPE)))
        {
            modelMaxNum.Add(item, 1);
            modelNum.Add(item, 0);
        }  
    }

    //重置场景数据
    public void ResetData()
    {
        foreach (GameObject item in models)
        {
            
            Destroy(item);
        }

        models.Clear();
        foreach (MODELTYPE item in Enum.GetValues(typeof(MODELTYPE)))
        {
            modelNum[item] = 0;
        }
    }

    //获取模型的最大值
    public int GetModelMaxNum(MODELTYPE TYPE)
    {
        return modelMaxNum[TYPE];
    }

    //获取模型数目
    public int GetModelNum(MODELTYPE TYPE)
    {
        return modelNum[TYPE];
    }

    //增加模型数量
    private void AddModelNum(MODELTYPE TYPE)
    {
      
        modelNum[TYPE]++;
      
    }

    //减少模型数量
    private void DelModelNum(MODELTYPE TYPE)
    {
        
        if(GetModelNum(TYPE)>0)
            modelNum[TYPE]--;
    }

    //删除模型
    public void RemoveModel(GameObject obj)
    {
        
        models.Remove(obj);
        DelModelNum(obj.GetComponent<ModelController>().TYPE);
        Destroy(obj);

    }

    //添加模型
    public void AddModel(GameObject obj)
    {
        
        models.Add(obj);
        AddModelNum(obj.GetComponent<ModelController>().TYPE);
  
    }

    //获取模型
    public GameObject GetModel(MODELTYPE type)
    {
        foreach (GameObject item in models)
        {
            if (item.GetComponent<ModelController>().TYPE == type)
                return item;
        }
        return null;
    }
}
