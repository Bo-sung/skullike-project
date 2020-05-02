using System;
using scr.PlayerScr;
using UnityEngine;
using MyData.PlayerScr;
using MyData.Item;


namespace PlayerScr
{
    public class User : MonoBehaviour, IPlayer
    {       
        [Header("UI")]
        public GameObject gameoverUI;
        [Header("Effects")]
        public GameObject smoke;
        public GameObject coin;
        public void Set_Ani(Animator _ani, Mov _Move, Sta _State)//애니메이션 변경용.
        {//에니메이션 변환과정에서 같은값을 입력받을시 애니메이션이 반복이 되어 중간에 갭이 생김. 중복되지 않도록해주어야함
            if (_ani.GetInteger("Mov") != (int)_Move || _ani.GetInteger("Sta") != (int)_State)
            {
                _ani.SetInteger("Mov", (int)_Move);
                _ani.SetInteger("Sta", (int)_State);
                _ani.Update(1);
            }
        }       
        [Header("Player - Status")]
        public State state;     //행동상태
        public Status stat;     //스텟
        public Inventory inventory; //인벤토리


        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public Animator ani;
        [HideInInspector] public Renderer rdr;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
            Set_Ani(ani, Mov.Stand, Sta.idle);
            state.Work = Sta.idle;
            state.Standing = Mov.Stand;
        }
        public void Skill(int _skillNum)
        {

        }
        public void Attack(GameObject _target, int _atktype)    //상대방에게 공격이 성공할시 호출할 함수
        {            
            _target.SendMessage("Attacked", stat.power);
        }
        public void Attacked(float _damage) //피해를 입을시 호출될 함수
        {
            stat.HP = stat.HP - (stat.DEF - _damage);
        }
        public void Jump()  //점프 입력시 호출될 함수
        {            
            if (state.Standing != Mov.Jump)
            {
                Set_Ani(ani, Mov.Jump, Sta.idle);
                rb.velocity = new Vector2(0, stat.jump);
                state.Standing = Mov.Jump;
            }
        }
        public void Move_Right()    //우측 이동시 호출될 함수
        {
            state.Dir = Dir.Right;
            state.Work = Sta.Move;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            if (state.Standing != Mov.Jump)
                Set_Ani(ani, Mov.Stand, Sta.Move);
            transform.Translate(Vector3.right * stat.speed * Time.deltaTime);
            
        }
        public void Move_Left() //좌측 이동시 호출될 함수
        {
            state.Dir = Dir.Left;
            state.Work = Sta.Move;
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            if (state.Standing != Mov.Jump)
                Set_Ani(ani, Mov.Stand, Sta.Move);
            transform.Translate(Vector3.left * stat.speed * Time.deltaTime);
        }
        public void Crouch()    //웅크리기 시행시 호출될 함수
        {
            Set_Ani(ani, Mov.Crouch, Sta.idle);
        }
        public void Move_Stop()
        {
            state.Work = Sta.idle;
            Set_Ani(ani, Mov.Stand, Sta.idle);
        }
        public void HangJump()
        {
            Jump();
            if (state.Standing != Mov.Jump)
                Set_Ani(ani, Mov.Hang, Sta.idle);
        }    
        private void LateUpdate()
        {
            if(state.Standing != Mov.Jump)
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                    Set_Ani(ani, Mov.Stand, Sta.idle);                
            if (state.death == true)
            {
                Set_Ani(ani, Mov.Stand, Sta.Death);
            }
        }

        public void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.tag == "GROUND")
            {
                if (state.Standing == Mov.Jump)
                {
                    state.Standing = Mov.Stand;
                    Set_Ani(ani, Mov.Stand, Sta.idle);
                }
            }
        }
    }
}