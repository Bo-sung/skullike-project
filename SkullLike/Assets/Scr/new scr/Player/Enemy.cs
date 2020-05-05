using System;
using scr.PlayerScr;
using UnityEngine;
using System.Collections.Generic;
using MyData.PlayerScr;
using MyData.Item;
using MyData.Data;
using System.Collections;

namespace scr.PlayerScr
{
    public class Enemy : MonoBehaviour,IPlayer
    {
        [Header("Effects")]
        public List<GameObject> effectList;

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
        public GameObject Mag;  //원거리
        public GameObject meleeAttackCtr;   //근거리

        private float attackSpeedTimer;


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
            if (state.Work != Sta.Attack && state.Standing != Mov.Jump)
                state.Work = Sta.Move;
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.left * stat.speed * Time.deltaTime);
        }
        public void Move_Stop()
        {
            state.Work = Sta.idle;
        }
        public void Jump()//점프
        { }
        public void Crouch()//수구리기
        { }


        public void Skill(int _skillNum)//스킬사용(인덱스)
        { }


        public bool attack_Enable;

        IEnumerator WaitForAttack()
        {
            yield return new WaitForSeconds(stat.attack_Speed);
            state.Work = Sta.idle;
            attack_Enable = true;
        }
        public void Melee_Attack()
        {
            if (attack_Enable)
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
        private void Update()
        {
            if(stat.HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}