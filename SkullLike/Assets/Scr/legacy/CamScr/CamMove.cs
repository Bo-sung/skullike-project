using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CamMove
{
    public class camMove : MonoBehaviour
    {
        [Header("Settings")]
        //카메라 이동    //몸통
        public float speed; //위치 감도
        public Vector3 originalPosition; //생성시 카메라 위치
        public Vector3 originalRotation; //생성시 카메라 각도
        public GameObject Cam;//카메라 포함하고있는 오브젝트
        public virtual void Forward() { }//직진키 입력시 호출
        public virtual void Backward() { }//후진키 입력시 호출
        public virtual void Left() { }//좌측키 입력시 호출
        public virtual void Right() { }//우측키 입력시 호출
        public virtual void Up() { }//상승키 입력시 호출
        public virtual void Down() { }//하강키 입력시 호출
        public virtual void MouseDown() { }//마우스 버튼 클릭 시작시 호출
        public virtual void MouseMoved(Vector2 touchPosition) { }//드래그중일시 호출
        public void pReset()//카메라 리셋 호출
        {
            transform.position = originalPosition;
            transform.eulerAngles = originalRotation;
        }
    }
}
