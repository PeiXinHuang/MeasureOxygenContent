using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTube1Controller : ModelController
{
    [SerializeField]
    private GameObject RubberHodePos = null; //橡胶塞位置

  

  

    private Vector3 RubberHoseObjOffset = Vector3.zero; //连接模型之间位置的偏移量


    public void SetRubberHoseObjOffset(Vector3 pos) //设置模型之间的偏移量
    {
        RubberHoseObjOffset = pos - this.transform.position;
    }

  


    public Vector3 GetRubberHodePos() //获取橡胶管位置
    {
        return RubberHodePos.transform.position;
    }

    


}
