using System;
using scr.PlayerScr;
using UnityEngine;
using System.Collections.Generic;
using MyData.PlayerScr;
using MyData.Item;
using MyData.Data;
using System.Collections;

namespace PlayerScr
{
    public class User : MonoBehaviour, IPlayer
    {
        [Header("UI")]
        private GameObject gameoverUI;
        [Header("Effects")]
        public List<GameObject> effectList;


        public void Set_Ani(Animator _ani)//애니메이션 변경용.
        {
            ani.Update(1);
        }
        public void Set_Ani(Animator _ani, Mov _Move)//애니메이션 변경용.
        {
            if (_ani.GetInteger("Mov") != (int)_Move)
            {
                _ani.SetInteger("Mov", (int)_Move);
                ani.Update(1);
            }
        }
        public void Set_Ani(Animator _ani, Sta _State)//애니메이션 변경용.
        {
            if (_ani.GetInteger("Sta") != (int)_State)
            {
                _ani.SetInteger("Sta", (int)_State);
                ani.Update(1);
            }
        }
        public void Set_Ani(Animator _ani, Mov _Move, Sta _State)//애니메이션 변경용.
        {//에니메이션 변환과정에서 같은값을 입력받을시 애니메이션이 반복이 되어 중간에 갭이 생김. 중복되지 않도록해주어야함
            if (_ani.GetInteger("Sta") != (int)_State)
            {
                _ani.SetInteger("Sta", (int)_State);
                ani.Update(1);
            }
            if (_ani.GetInteger("Mov") != (int)_Move)
            {
                _ani.SetInteger("Mov", (int)_Move);
                ani.Update(1);
            }
        }
        [Header("Player - Status")]
        public State state;     //행동상태
        public Status stat;     //스텟
        public Inventory inventory; //인벤토리
        public GameObject Mag;      //원거리용 오브젝트 풀
        public GameObject meleeAttackCtr;   //근거리 공격용

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
            attack_Enable = true;
        }
        public void Skill(int _skillNum)
        {

        }
        public bool attack_Enable;

        IEnumerator WaitForAttack()
        {
            yield return new WaitForSeconds(stat.attack_Speed);
            state.Work = Sta.idle;
            attack_Enable = true;
        }
        public void Melee_Attack()
        {
            if(attack_Enable)
            {
                attack_Enable = false;
                state.Work = Sta.Attack;
                Attack_info info = new Attack_info();
                info.ATk = stat.power;
                info.effect = Effects.normal;
                info.Attack_Speed = stat.attack_Speed;
                Attack(meleeAttackCtr, info);
                StartCoroutine(WaitForAttack());
            }
        }
        public void Attack(GameObject _Magazine, Attack_info _Atk_Info)    //상대방에게 공격이 성공할시 호출할 함수
        {
            _Magazine.SendMessage("Fire", _Atk_Info);
        }
        public void Attacked(Attack_info _Atk_Info) //피해를 입을시 호출될 함수
        {
            stat.HP = stat.HP - (_Atk_Info.ATk - stat.DEF);
        }
        public void Jump()  //점프 입력시 호출될 함수
        {            
            if (state.Standing != Mov.Jump)
            {
                rb.velocity = new Vector2(0, stat.jump);
                state.Standing = Mov.Jump;
                state.Work = Sta.idle;
            }
        }
        public void Move_Right()    //우측 이동시 호출될 함수
        {
            state.Dir = Dir.Right;
            if (state.Work != Sta.Attack && state.Standing != Mov.Jump)
                state.Work = Sta.Move;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.right * stat.speed * Time.deltaTime);
            
        }
        public void Move_Left() //좌측 이동시 호출될 함수
        {
            state.Dir = Dir.Left;
            if(state.Work != Sta.Attack && state.Standing != Mov.Jump)
                state.Work = Sta.Move;
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.left * stat.speed * Time.deltaTime);
        }
        public void Crouch()    //웅크리기 시행시 호출될 함수
        {
            state.Work = Sta.idle;
            state.Standing = Mov.Crouch;
        }
        public void Move_Stop()
        {
            state.Work = Sta.idle;
        }
        public void HangJump()
        {
            Jump();
            if (state.Standing != Mov.Jump)
            {
                state.Work = Sta.idle;
                state.Standing = Mov.Hang;
            }

        }
        private void Update()
        {
            Set_Ani(ani, state.Standing, state.Work);
        }
        private void LateUpdate()
        {
            if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                state.Work = Sta.idle;
            }
            if (state.death == true)
            {
                state.Work = Sta.Death;
                state.Standing = Mov.Hang;
            }
            Set_Ani(ani, state.Standing, state.Work);
        }

        public void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.tag == "GROUND")
            {
                if (state.Standing == Mov.Jump)
                {
                    state.Work = Sta.idle;
                    state.Standing = Mov.Stand;
                }
            }
        }
    }
}