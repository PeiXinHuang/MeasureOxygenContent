using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 模型基类,保存模型共同数据和方法
/// </summary>
public class ModelController : MonoBehaviour
{
    public SenceData.MODELTYPE TYPE; //模型类型

    public List<GameObject> LinkModels = new List<GameObject>(); //连接的模型
    
    private Dictionary<GameObject, Vector3> LinkModelOffsets = new Dictionary<GameObject, Vector3>();//连接模型关于主模型之间的偏移量

    protected Vector3 GetScreenPos(Vector3 worldPos)
    {
        return Camera.main.WorldToScreenPoint(worldPos);
    }

    [HideInInspector]
    public int collisionNum = 0; //当前碰撞到的物体数目
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetType().Equals(typeof(BoxCollider)))
            collisionNum += 1;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetType().Equals(typeof(BoxCollider)))
            collisionNum -= 1;
    }

    //查找连接模型是否存在
    public bool FindLinkModel(GameObject obj)
    {
        foreach (GameObject item in LinkModels)
        {
            if (obj == item)
                return true;
        }
        return false;
    }

  

    //移除连接模型
    public void ReMoveLinkModel(GameObject obj)
    {
        LinkModels.Remove(obj);
        LinkModelOffsets.Remove(obj);
    }

    //添加连接模型
    public void AddLinkModel(GameObject obj)
    {
        LinkModels.Add(obj);
        LinkModelOffsets.Add(obj, obj.transform.position - this.transform.position);
    }

    //获取对应模型的偏移量
    public Vector3 GetOffset(GameObject obj)
    {
        return LinkModelOffsets[obj];
    }


    //模型销毁前，删除对应连接模型中的连接模型中的本对象,并且去除偏移字典值
    private void OnDestroy()
    {

        foreach (GameObject item in LinkModels)
        {
            if (item) //确保该对象存在
            {
                item.GetComponent<ModelController>().LinkModels.Remove(this.gameObject);
                item.GetComponent<ModelController>().LinkModelOffsets.Remove(this.gameObject);
            }
        }
    }


    private void FixedUpdate()
    {

        if (GameObject.Find("SenceController").GetComponent<SenceController>().isMoveModel
            && GameObject.Find("SenceController").GetComponent<SenceController>().target == this.gameObject

            ) //本模型正在移动
            UpdateLinkModelPos();
    }

    //该模型正在移动时，递归刷新连接模型的位置,传入主模型的位置
    public void UpdateLinkModelPos(GameObject obj = null)
    {
        
        //更新连接对象的位置
        foreach (GameObject item in LinkModels)
        {
            if (obj == item)//剔除本模型，防止来回调用，只适用于树形结构，图形结构需要重写
                continue;
            item.transform.position  = Vector3.MoveTowards(item.transform.position, 
                this.transform.position + LinkModelOffsets[item], 
                GameObject.Find("SenceController").GetComponent<SenceController>().mouseSpeed * 10 * Time.deltaTime);


            item.GetComponent<ModelController>().UpdateLinkModelPos(this.gameObject);
        }
        
    }


}
