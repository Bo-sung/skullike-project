using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScr
{
    public class User : Player,IUser
    {

        [Header("User - Only")]
        public float DashRange;
        public int DashTime = 2;
        public override void Initiallize()
        {
            base.Initiallize();
        }

        public override void Move(Dir _dir)
        {
            if ((state.work == Sta.Attack && state.standing == Mov.Jump) || (state.work != Sta.Attack && state.work != Sta.Skill))
            {
                base.Move(_dir);
                if (state.standing != Mov.Jump)
                    state.work = Sta.Move;
            }
        }

        
        
        
        public void Dash()
        {
            if (DashTime > 0)
            {
                //레이케스트로 벽 관통해서 안지나가도록 검사후 대쉬하도록 추가할것. 그리고 대쉬 시행시 카메라에 메시지를 보내 대기하도록 추가할것
                switch (state.dir)
                {
                    case Dir.Left:
                    {
                        CameraCtr.Instance.CamWait(0.125f);
                        transform.Translate(DashRange * 0.125f * Vector2.left);
                        DashTime--;
                    }
                        break;
                    case Dir.Right:
                    {
                        CameraCtr.Instance.CamWait(0.125f);
                        transform.Translate(DashRange * 0.125f * Vector2.right);
                        DashTime--;
                    }
                        break;
                }
            }
            else
            {
                DashTime = 2;
            }
        }
        public void Dash(Vector3 _position)
        {
            if (DashTime > 0)
            {
                transform.position = _position;
                DashTime--;
            }
            else
            {
                DashTime = 2;
            }
        }
        
        public void DownJump()    //웅크리기 시행시 호출될 함수
        {
            state.standing = Mov.Crouch;
            state.work = Sta.Idle;
            Set_Ani();
            
            GetComponent<Collider2D>().isTrigger = true;
            StartCoroutine(WaitforDownJump());
        }
        IEnumerator WaitforDownJump()
        {
            yield return new WaitForSeconds(0.35f);
            GetComponent<Collider2D>().isTrigger = false;
        }
        private bool IsDownJumpEnable;
        public int JumpCount;
        public void Jump()
        {
            if (JumpCount > 0)
            {
                if(Input.GetKey(KeyCode.DownArrow) && IsDownJumpEnable)
                {
                    DownJump();
                }
                else
                {
                    state.standing = Mov.Jump;
                    state.work = Sta.Idle;
                    Set_Ani();
                    rb.velocity = new Vector2(0, stat.jump);
                    JumpCount = JumpCount - 1;
                }
            }       
        }

        private RaycastHit _raycastHit;

        public override void Set_Update()
        {
            base.Set_Update();
            if (state.standing == Mov.Jump)
            {
                //Debug.DrawRay(transform.position,Vector3.down,Color.black,1000);
                Debug.DrawLine(transform.position, Vector3.down * stat.jump + transform.position, Color.red,1000f);
                if (Physics.Raycast(transform.position, Vector3.down, out _raycastHit, 1000f,1<<LayerMask.NameToLayer("Default")))
                {
                    if (_raycastHit.transform.CompareTag("GROUND"))
                    {
                        state.work = Sta.Idle;
                        state.standing = Mov.Stand;
                        IsDownJumpEnable = true;
                        JumpCount = 2;
                    }
                
                }
            }
        }

        public override void Set_LateUpdate()
        {
            base.Set_LateUpdate();
            if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                if(state.work != Sta.Attack)
                    state.work = Sta.Idle;
            }
            if (state.death == true)
            {
                state.work = Sta.Death;
            }
            Set_Ani(ani, state.standing, state.work);
        }

        public override  void OnCollisionEnter2D(Collision2D _col)
        {
            base.OnCollisionEnter2D(_col);
            if (_col.transform.tag == "GROUND" && _col.transform.name == "surface")
            {                
                
            }
        }
        public void OnCollisionStay2D(Collision2D _col)
        {
            base.OnCollisionStay2D(_col);
            if(_col.transform.tag == "GROUND" && _col.transform.name == "surface")
            {
                
            }
        }
        public void OnCollisionExit2D(Collision2D _col)
        {
            base.OnCollisionExit2D(_col);
            if (_col.transform.tag == "GROUND" && _col.transform.name == "surface")
            {
                IsDownJumpEnable = false;
            }
        }
    }
}