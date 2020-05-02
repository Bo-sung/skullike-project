using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CamMove;

public class TopView : camMove
{
    public override void Forward() { transform.Translate(new Vector3(0, 0, speed)); }//직진키 입력시 호출   
    public override void Backward() { transform.Translate(new Vector3(0, 0, -speed)); }//후진키 입력시 호출 
    public override void Left() { transform.Translate(new Vector3(-speed, 0, 0)); }//좌측키 입력시 호출 
    public override void Right() { transform.Translate(new Vector3(speed, 0, 0)); }//우측키 입력시 호출 
    public override void Up() { transform.Translate(new Vector3(0, speed, 0)); }//상승키 입력시 호출 
    public override void Down() { transform.Translate(new Vector3(0, -speed, 0)); }//하강키 입력시 호출 

    [Header("TopView")]
    public bool Selected;
    private Quaternion LockAngle;
    /*드래그 코드.*/
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
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        transform.position += new Vector3(-v  + speed * 0.01f, h * speed * 0.01f, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = firstPosition = originalPosition;
        transform.eulerAngles = firstRotation = originalRotation;
        mousePos = lastPos = Vector2.zero;
    }
    private void Update()
    {
        if (transform.position.y <= 3)
        {
            transform.Translate(0, speed, 0);
        }
    }
}
