using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 场景控制器，包括了场景中模型的基本操作
/// 选中高亮模型
/// 创建模型
/// 移动模型
/// 获取场景数据SenceData
/// </summary>

public class SenceController : MonoBehaviour
{

    public SenceData senceData; //场景数据

    [HideInInspector]
    public GameObject target;//选择中的物体

    private GameObject hightLighModel;//旧的高亮模型

    [HideInInspector]
    public bool canMoveModel;//能否移动模型
    [HideInInspector]
    public bool isMoveModel; //是否正在移动模型


    private GameObject ModelToCreate;//要创建的模型
    private GameObject ModelCreated; //被创建的模型
    public bool isCreateModel;//是否正在创建模型
    private bool hasCreateModel; //是否已经创建模型


    private Vector3 oldPos = Vector3.zero;
    private Vector3 newPos = Vector3.zero;
    public float mouseSpeed = 0f; //鼠标移动速度

    private Vector3 offset = Vector3.zero; //鼠标点击位置与模型之间的偏移量



    private void Start()
    {
        canMoveModel = true;
        isMoveModel = false;
        isCreateModel = false;
        hasCreateModel = false;
    }



    private void FixedUpdate()
    {
        SetMouseSpeed();
        if (canMoveModel)
            MoveModel();



        if (isCreateModel)
            CreateModel(ModelToCreate);
    }

    private void Update()
    {

    }

    //设置鼠标速度
    private void SetMouseSpeed()
    {

        newPos = Input.mousePosition;
        mouseSpeed = Vector3.Distance(newPos, oldPos);
        oldPos = newPos;

    }


    //获取选中物体
    private GameObject GetTarget(out RaycastHit hit)
    {
        target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        LayerMask mask = 1 << (LayerMask.NameToLayer("Thermometer"));


        if (Physics.Raycast(ray, out hit, 100000.0f, mask.value))
        {
            if (hit.collider.GetType().Equals(typeof(BoxCollider))) //只用BoxCollider控制物体移动
                target = hit.collider.gameObject;

            return target;
        }

        mask = 1;

        if (Physics.Raycast(ray, out hit, 100000.0f, mask.value))
        {
            if (hit.collider.GetType().Equals(typeof(BoxCollider))) //只用BoxCollider控制物体移动
                target = hit.collider.gameObject;
        }


        return target;
    }

    //移动模型
    private void MoveModel()
    {
        //鼠标抬起，直接返回
        if (!Input.GetMouseButton(0))
        {
            isMoveModel = false;
            target = null;
            offset = Vector3.zero;
            return;
        }

       


        //鼠标按下，开始移动模型
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hitInfo;
            target = GetTarget(out hitInfo);

            if (target)
                HighLightModel(target);

            if (target != null)
            {

                isMoveModel = true;
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }
        }


        if (isMoveModel && target)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            Vector3 mousePos = Input.mousePosition;
            //mousePos.z = screenPos.z;
            mousePos.z = 20;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos)+offset;
            

            //碰撞到物体，移动减慢
            if (target.GetComponent<ModelController>().collisionNum > 0)
            {
                target.transform.position = Vector3.MoveTowards(target.transform.position, worldPos, 10.0f * Time.deltaTime);
            }
            else
            {
                // target.transform.position = worldPos;
                target.transform.position = Vector3.MoveTowards(target.transform.position, worldPos, mouseSpeed * 10 * Time.deltaTime);
            }


        }
        

    }

    //高亮选中的物体，取消旧物体的高亮
    private void HighLightModel(GameObject obj)
    {
        if (hightLighModel && hightLighModel.GetComponent<HighlightableObject>())
        {
            hightLighModel.GetComponent<HighlightableObject>().Off();
        }

        hightLighModel = obj;
        if (hightLighModel.GetComponent<HighlightableObject>())
        {

            hightLighModel.GetComponent<HighlightableObject>().ConstantOn(Color.blue);
        }
    }


    //获取鼠标位置
    private Vector3 getMousePos()
    {


        Vector3 mousePos = Input.mousePosition;

        //mousePos.z = MagnesiumBarPrefab.transform.position.z;
        mousePos.z = 20;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPos;


    }


    //开始创建模型
    public void BeginCreateModel(GameObject model)
    {
        ModelToCreate = model; //要创建的模型的预置体
        isCreateModel = true;
        hasCreateModel = false;
    }

    //结束创建
    public void EndCreateModel()
    {
        isCreateModel = false;
        hasCreateModel = false;
        ModelToCreate = null;
        ModelCreated = null;

    }

    //创建模型
    private void CreateModel(GameObject model)
    {

        if (!EventSystem.current.IsPointerOverGameObject() && !hasCreateModel) //没有创建模型,并且没有在UI上面，即可以创建模型
        {

            SenceData.MODELTYPE modelType = model.GetComponent<ModelController>().TYPE;
            if (this.senceData.GetModelMaxNum(modelType) > this.senceData.GetModelNum(modelType)) //创建模型
            {

                ModelCreated = Instantiate(ModelToCreate, getMousePos(), ModelToCreate.transform.rotation);

                this.senceData.AddModel(ModelCreated);

                hasCreateModel = true;

            }

        }

        else if (hasCreateModel && ModelCreated) //已经创建了模型，让模型跟随鼠标运动
        {

            //ModelCreated.transform.position = getMousePos();

            //碰撞到物体，移动减慢
            if (ModelCreated.GetComponent<ModelController>().collisionNum > 0)
            {
                ModelCreated.transform.position = Vector3.MoveTowards(ModelCreated.transform.position, getMousePos(), 10.0f * Time.deltaTime);
            }
            else
            {
                // target.transform.position = worldPos;
                ModelCreated.transform.position = Vector3.MoveTowards(ModelCreated.transform.position, getMousePos(), mouseSpeed * 10 * Time.deltaTime);
            }


        }


    }

    //删除模型
    public void RemoveModel()
    {
        if (hightLighModel)
            this.senceData.RemoveModel(hightLighModel);
    }

    //重置场景
    public void ResetSence()
    {
        hightLighModel = null;
        this.senceData.ResetData();

    }

    //设置模型不可以移动，second s后可以移动，用于控制模型的吸附
    public void SetCanNotControl(float second = 1.0f)
    {
        this.canMoveModel = false;
        target = null;
        Invoke("SetCanControl", second);
    }
    private void SetCanControl()
    {
        this.canMoveModel = true;
    }


  
}
