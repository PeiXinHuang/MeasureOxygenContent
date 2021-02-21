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
    
    public Dictionary<GameObject, Vector3> LinkModelOffsets = new Dictionary<GameObject, Vector3>();//连接模型关于主模型之间的偏移量


    private Vector3 toPos = Vector3.zero; //模型要到达的位置

    protected Vector3 GetScreenPos(Vector3 worldPos)
    {
        return Camera.main.WorldToScreenPoint(worldPos);
    }

    public List<GameObject> collisionModels = new List<GameObject>();//碰撞到的模型
    [HideInInspector]
    public int collisionNum = 0; //当前碰撞到的物体数目
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetType().Equals(typeof(BoxCollider)))
        {
            collisionModels.Add(collision.gameObject);
            collisionNum += 1;
        }
           
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetType().Equals(typeof(BoxCollider)))
        {
            collisionModels.Remove(collision.gameObject);
            collisionNum -= 1;
        }   
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
    public GameObject FindLinkModel(SenceData.MODELTYPE type)
    {
        foreach (GameObject item in LinkModels)
        {
            if (item.GetComponent<ModelController>().TYPE == type)
                return item;
        }
        return null;
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


    //模型销毁前，删除对应连接模型中的连接模型中的本对象,并且去除偏移字典值,去除碰撞到的模型
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

        foreach(GameObject item in collisionModels)
        {
            if (item)
            {
                item.GetComponent<ModelController>().collisionModels.Remove(this.gameObject);
                item.GetComponent<ModelController>().collisionNum -= 1;
            }
        }
    }


    //该模型正在移动时，递归刷新连接模型的位置,传入主模型的位置
    public void UpdateLinkModelPos(GameObject obj = null)
    {

        //更新连接对象的位置
        foreach (GameObject item in LinkModels)
        {
            if (obj == item)//剔除本模型，防止来回调用，只适用于树形结构，图形结构需要重写
                continue;
            item.transform.position = this.transform.position + LinkModelOffsets[item];
            item.GetComponent<ModelController>().UpdateLinkModelPos(this.gameObject);
            //item.GetComponent<ModelController>().UpdateCollisionModels();
        }      
    }

    //刷新碰撞到的物体和它的子物体位置和碰撞到的子物体
    public void UpdateCollisionModels(GameObject obj = null)
    {
        foreach (GameObject item in collisionModels)
        {
            if (obj == item)//剔除本模型，防止来回调用，只适用于树形结构，图形结构需要重写
                continue;
            item.GetComponent<ModelController>().UpdateLinkModelPos();
            item.GetComponent<ModelController>().UpdateCollisionModels(this.gameObject);
        }
    }

    //获取模型最右端位置
    public Vector3 GetRightCenterPos()
    {
        Vector3 rightPos = new Vector3(
            this.GetComponent<BoxCollider>().bounds.max.x, this.GetComponent<BoxCollider>().bounds.center.y, 20);

        foreach (BoxCollider item in this.GetComponents<BoxCollider>())
        {
            if (item.bounds.max.x > rightPos.x)
            {
                rightPos = new Vector3(
                    item.bounds.max.x, item.bounds.center.y, 20);
            }
        }

        return rightPos;
        
    }


    //获取模型最左端位置
    public Vector3 GetLeftCenterPos()
    {
        Vector3 leftPos = new Vector3(
            this.GetComponent<BoxCollider>().bounds.min.x, this.GetComponent<BoxCollider>().bounds.center.y, 20);

        foreach (BoxCollider item in this.GetComponents<BoxCollider>())
        {
            if (item.bounds.min.x < leftPos.x)
            {
                leftPos = new Vector3(
                    item.bounds.min.x, item.bounds.center.y, 20);
            }
        }

        return leftPos;

    }


    //获取模型最顶端位置
    public Vector3 GetTopCenterPos()
    {
        Vector3 topPos = new Vector3(
            this.GetComponent<BoxCollider>().bounds.center.x, this.GetComponent<BoxCollider>().bounds.max.y, 20);

        foreach (BoxCollider item in this.GetComponents<BoxCollider>())
        {
            if (item.bounds.max.y > topPos.y)
            {
                topPos = new Vector3(
                    item.bounds.center.x, item.bounds.max.y, 20);
            }
        }
        return topPos;
    }


    //获取模型最底端位置
    public Vector3 GetBottomCenterPos()
    {
        Vector3 bottomPos = new Vector3(
            this.GetComponent<BoxCollider>().bounds.center.x, this.GetComponent<BoxCollider>().bounds.min.y, 20);

        foreach (BoxCollider item in this.GetComponents<BoxCollider>())
        {
            if (item.bounds.min.y < bottomPos.y)
            {
                bottomPos = new Vector3(
                    item.bounds.center.x, item.bounds.min.y, 20);
            }
        }
        return bottomPos;
    }


    private void FixedUpdate()
    {
        //刷新移动的模型的连接子模型位置，和它碰撞到的模型的子模型位置
        if (GameObject.Find("SenceController").GetComponent<SenceController>().isMoveModel
            && GameObject.Find("SenceController").GetComponent<SenceController>().target == this.gameObject
          ) //本模型正在移动,移动连接的物体
        {
            UpdateLinkModelPos();
        }

  
    }

    //将模型移动到pos
    public void MoveModelToPos(Vector3 pos)
    {
        toPos = pos;
        InvokeRepeating("MoveModel", 0, 0.02f);
       
    }
    private void MoveModel()
    {

        if (Vector3.Distance(this.transform.position, toPos) < 0.01f)
        {
            CancelInvoke("MoveModel");
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, toPos, 0.1f);
        UpdateLinkModelPos();
    }
}
