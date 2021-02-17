using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTube1Controller : ModelController
{

    [SerializeField]
    private GameObject BeakerObjPos = null;


    public Vector3 getBeakerObjPos()
    {
        return BeakerObjPos.transform.position;
    }




}
