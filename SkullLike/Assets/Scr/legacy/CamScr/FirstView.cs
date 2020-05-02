using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CamMove;

public class FirstView : camMove
{
    [Header("FirstView")]
    public bool Selected;
    public override void Forward() { transform.Translate(new Vector3(0, 0, speed)); }//직진키 입력시 호출   
    public override void Backward() { transform.Translate(new Vector3(0, 0, -speed)); }//후진키 입력시 호출 
    public override void Left() { transform.Translate(new Vector3(-speed, 0, 0)); }//좌측키 입력시 호출 
    public override void Right() { transform.Translate(new Vector3(speed, 0, 0)); }//우측키 입력시 호출 
    public override void Up() { transform.Translate(new Vector3(0, speed, 0)); }//상승키 입력시 호출 
    public override void Down() { transform.Translate(new Vector3(0, -speed, 0)); }//하강키 입력시 호출 
    //카메라 각도조절  //머리
    public float axis;  //각도 감도
    public void RollLeft() { transform.Rotate(0, 0, axis); }//좌측 기울이기키 입력시 호출
    public void RollRight() { transform.Rotate(0, 0, -axis); }//우측 기울이기키 입력시 호출

    /*드래그해서 카메라 각도조절코드.*/
    Vector2 mousePos;   //현재 마우스 포인터의 위치
    Vector2 lastPos;    //마지막으로 마우스포인터가 있던 위치
    Vector3 firstPosition { get; set; } //초기 카메라 각도(드래그중)
    Vector3 firstRotation { get; set; }  //초기 카메라 각도(드래그중)
    public override void MouseDown()//마우스 버튼 클릭 시작시 호출
    {
        firstPosition = transform.position;
        firstRotation = transform.eulerAngles;
        lastPos = Input.mousePosition;
    }
    public override void MouseMoved(Vector2 touchPosition)//드래그중일시 호출
    {
        /*
        //마지막으로 마우스포인터가 위치한 자리에서부터 현재 위치를 뺀후 그 값에 따라 각도조정

        mousePos = touchPosition;//마우스포인터의 위치를 매개변수로 받아서 저장.
        float angle = (lastPos.y - mousePos.y) / 10;    //각도 = 마우스 포인터 마지막 위치의 y값 - 현재 마우스 포인터 위치의 y값 나누기 10
        transform.Rotate(angle, 0, 0);//주의!! 아랫줄과 하나에 넣을시 z축이 움직이며 작동 꼬일수 있음. 따로따로 사용할것!!!
        angle = ((lastPos.x - mousePos.x) / 10);
        //transform.localEulerAngles = new Vector3(0, angle, 0);
        transform.Rotate(0, -angle, 0);
        lastPos = mousePos;
        */
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        transform.localEulerAngles += new Vector3(-v * axis, h * axis, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.position = originalPosition;
        firstRotation = transform.eulerAngles = originalRotation;
        mousePos = lastPos = Vector2.zero;
    }
    private void Update()
    {
        if (transform.position.y <= originalPosition.y)
        {
            transform.Translate(0, speed, 0);
        }
    }
    void LateUpdate()
    {
        
    }
}
