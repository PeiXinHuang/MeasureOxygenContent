using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{//移动旋转缩放速度
    public float translateSpeed = 0.5f;

    public float zoomSpeed = 5.0f;

    public float rotateSpeed = 0.5f;


    private Vector3 pos;
    private Quaternion qua;
    private float view;
    private void Start()
    {
        pos = this.transform.position;
        qua = this.transform.rotation;
        view = this.GetComponent<Camera>().fieldOfView;
    }


    private void Update()
    {
        cameraTranslate();
        // cameraRotate();
        cameraZoom();
    }


    private void cameraTranslate()  //摄像机中键平移
    {

        var mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴移动
        var mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴移动

        if (Input.GetKey(KeyCode.Mouse2))
        {

            transform.Translate(Vector3.left * (mouse_x * translateSpeed) * Time.deltaTime);
            transform.Translate(Vector3.up * (mouse_y * translateSpeed) * Time.deltaTime);
        }
    }

    //private void cameraRotate()
    //{
    //    var mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴移动
    //    var mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴移动

    //    if (Input.GetKey(KeyCode.Mouse1))
    //    {
    //        transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, mouse_x * rotateSpeed);
    //        transform.RotateAround(new Vector3(0, 0, 0), transform.right, mouse_y * rotateSpeed);
    //    }
    //}



    private void cameraZoom() //摄像机滚轮缩放
    {

        //平行相机
        //if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.GetComponent<Camera>().orthographicSize < 15)
        //    this.GetComponent<Camera>().orthographicSize += zoomSpeed;


        //if (Input.GetAxis("Mouse ScrollWheel") < 0 && this.GetComponent<Camera>().orthographicSize > 9)
        //    this.GetComponent<Camera>().orthographicSize -= zoomSpeed;


        //if (Input.GetAxis("Mouse ScrollWheel") > 0)
        //    transform.Translate(Vector3.forward * zoomSpeed);


        //if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //    transform.Translate(Vector3.forward * -1 * zoomSpeed);


        if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.GetComponent<Camera>().fieldOfView <= 100)
            this.GetComponent<Camera>().fieldOfView += zoomSpeed;
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && this.GetComponent<Camera>().fieldOfView >= 20)
            this.GetComponent<Camera>().fieldOfView -= zoomSpeed;

    }


    public void ResetCameraPos()
    {
        this.transform.position = pos;
        this.transform.rotation = qua;
        this.GetComponent<Camera>().fieldOfView = view;
    }
}
