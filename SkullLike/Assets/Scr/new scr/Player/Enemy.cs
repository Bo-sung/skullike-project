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

        public void Set_Ani(Animator _ani, Mov _Move, Sta _State)//�ִϸ��̼� �����.
        {//���ϸ��̼� ��ȯ�������� �������� �Է¹����� �ִϸ��̼��� �ݺ��� �Ǿ� �߰��� ���� ����. �ߺ����� �ʵ������־����
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
        public State state;     //�ൿ����
        public Status stat;     //����
        public GameObject Mag;  //���Ÿ�
        public GameObject meleeAttackCtr;   //�ٰŸ�

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

        public void Move_Right()    //���� �̵��� ȣ��� �Լ�
        {
            state.Dir = Dir.Right;
            if (state.Work != Sta.Attack && state.Standing != Mov.Jump)
                state.Work = Sta.Move;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.right * stat.speed * Time.deltaTime);

        }
        public void Move_Left() //���� �̵��� ȣ��� �Լ�
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
        public void Jump()//����
        { }
        public void Crouch()//��������
        { }


        public void Skill(int _skillNum)//��ų���(�ε���)
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
        public void Attack(GameObject _Magazine, Attack_info _Atk_Info)    //���濡�� ������ �����ҽ� ȣ���� �Լ�
        {
            _Magazine.SendMessage("Fire", _Atk_Info);
        }
        public void Attacked(Attack_info _Atk_Info) //���ظ� ������ ȣ��� �Լ�
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