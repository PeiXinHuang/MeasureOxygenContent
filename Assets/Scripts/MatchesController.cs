using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesController : ModelController
{
    private GameObject FireObj; //火
    private GameObject matchChild; //火柴

    public bool hasFireMatch = false; //是否已经点燃火柴



    private Vector3 newMatchPos;
    private Vector3 oldMatchPos;
    private int ShakeNum = 0; //甩动次数

    private void Start()
    {
        FireObj = this.transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
        matchChild = this.transform.GetChild(0).GetChild(0).gameObject;
        newMatchPos = oldMatchPos = matchChild.transform.position;
    }



    private void Update()
    {
        UnFireMatch();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject== matchChild&&!hasFireMatch)
        {
            //Debug.Log("点燃火柴");
            Vector3 pos = other.gameObject.transform.position;

            GameObject.Find("SenceController").GetComponent<SenceController>().SetCanNotControl(2);

     

            matchChild.transform.position =  this.transform.GetChild(1).transform.position; 
            LeanTween.move(matchChild, this.transform.GetChild(2).transform.position, 0.5f);
            hasFireMatch = true;

            FireObj.SetActive(true);
        }

        
    }

 


   

    //甩动熄灭火柴
    public void UnFireMatch()
    {
        //火柴没有被点燃，直接退出
        if (!hasFireMatch)
            return;

        newMatchPos = matchChild.transform.position;
        //Debug.Log(Vector3.Distance(newMatchPos, oldMatchPos));
        if (Vector3.Distance(newMatchPos, oldMatchPos) > 1.5)
        {
            oldMatchPos = newMatchPos;
            ShakeNum += 1;
        }
        else
        {
            ShakeNum = 0;
        }

        if (ShakeNum > 3) //熄灭
        {
            ShakeNum = 0;
            FireObj.SetActive(false);
            hasFireMatch = false;
        }

    }




}
