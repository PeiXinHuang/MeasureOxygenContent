using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTube2Controller : ModelController
{
    [SerializeField]
    private GameObject GasBottleObjPos = null;


    public Vector3 getGasBottleObjPos()
    {
        return GasBottleObjPos.transform.position;
    }

}
