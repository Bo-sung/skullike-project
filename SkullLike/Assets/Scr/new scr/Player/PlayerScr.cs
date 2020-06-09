
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PlayerScr
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
        public interface IPlayer
        {
            void Move_Right();//현제 위치에서 우측으로 이동
            void Move_Left();//현제 위치에서 좌측으로 이동
            void Jump();//점프
            void Move_Stop();
            void Skill(int _skillNum);//스킬사용(인덱스)
            void Attack(GameObject _bullet, AttackInfo _Atk_Info);//공격. 데미지를 리턴함
            void Attacked(AttackInfo _Atk_Info);//공격받음(데미지). 데미지를 입력받아 현제 스텟에 따른 피해량을 저장
        }
        public interface IUser : IPlayer
        {
            void Dash();
            void DownJump();//아래점프
            void Melee_Attack();//근접공격
        }
        public class Skill : MonoBehaviour
        {
            private AttackInfo m_attack_Info;
            public void InitInformation(AttackInfo _attack_Info)
            {
                m_attack_Info = _attack_Info;
            }
            public AttackInfo GetAttack_Info()
            {
                return m_attack_Info;
            }

            public virtual void Passive()
            {

            }
            public virtual void Active()
            {

            }
        }


        [Serializable]
        public struct State     //상태
        {
            //[Serializable]
            //public struct standing
            //{
            //    public bool jump;
            //    public bool crouch;
            //    public bool stand;
            //    public bool hang;
            //}
            //[Serializable]
            //public struct Do
            //{
            //    public bool idle;
            //    public bool Move;
            //    public bool attack;
            //    public bool skill;
            //}
            public Mov standing;
            public Sta work;
            public Dir dir;

            public bool death; //생사
        }
        [Serializable]
        public struct Status
        {
            public float jump;
            public float power;
            public float speed;
            public float hp;
            public float def;
            public float attackRange;
            public float attackSpeed;
        }
        [Serializable]
              
        public enum Effects { normal, burn, blind, bleeding, slow }
        public struct AttackInfo
        {
            public float ATk;
            public float Attribute;
            public Effects Effect;
            public Dir Direction;
            public float AttackRange;
            public float AttackSpeed;
        }

        //현제 상태 지정용 열거형들.
        public enum Mov { Stand, Jump, Crouch };//이동 상황
        public enum Dir { Left, Right, Up};//방향
        public enum Sta { Idle, Move, Attack, Skill, Death };//상태
    
        
        [RequireComponent(typeof(Collider2D))]
        [RequireComponent(typeof(Rigidbody2D))]
        public class Player : MonoBehaviour, IPlayer
    {
        
        [Header("Effects")]
        public List<GameObject> effectList;

        public void Set_Ani()
        {
            if (ani.GetInteger("Sta") != (int)state.work)
            {
                ani.SetInteger("Sta", (int)state.work);
                ani.Update(1);
            }
            if (ani.GetInteger("Mov") != (int)state.standing)
            {
                ani.SetInteger("Mov", (int)state.standing);
                ani.Update(1);
            }
        }
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
        public List<GameObject> inventory; //인벤토리

        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public Animator ani;
        [HideInInspector] public Renderer rdr;
        [HideInInspector] public Collider2D col2D;

        public Dir Swap(Dir _dir)
        {
            if (_dir == Dir.Left)
                _dir = Dir.Right;
            else if (_dir == Dir.Right)
                _dir = Dir.Left;
            return _dir;
        }
        public virtual void Initiallize()
        {
            rb = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
            if (ani == null)
            {
                ani = GetComponentInChildren<Animator>();
            }
            Set_Ani(ani, Mov.Stand, Sta.Idle);
            state.work = Sta.Idle;
            state.standing = Mov.Stand;
            attack_Enable = true;
        }

        public virtual void Set_Update()
        {
            Set_Ani();
        }
        public virtual void Set_FixedUpdate()
        {
            
        }

        public virtual void Set_LateUpdate()
        {
            if (state.death == true)
            {
                state.work = Sta.Death;
            }
            Set_Ani(ani, state.standing, state.work);
        }
        void Start()
        {
            Initiallize();
            //Invoke("Set_Ani",0.1f);
        }
        private void Update()
        {
            Set_Update();
        }
        private void FixedUpdate()
        {
            Set_FixedUpdate();
        }
        private void LateUpdate()
        {
            Set_LateUpdate();
            Check_Death();
        }
        

        public virtual void Skill(int _skillNum)
        {

        }


        public void Attack(GameObject _Magazine, AttackInfo _Atk_Info)    //상대방에게 공격이 성공할시 호출할 함수
        {
            _Magazine.SendMessage("Fire", _Atk_Info);
        }
        public void Attacked(AttackInfo _Atk_Info) //피해를 입을시 호출될 함수
        {
            stat.hp = stat.hp - (_Atk_Info.ATk - stat.def);
        }
        

        public GameObject meleeAttackCtr;   //근거리 공격용
        public bool attack_Enable;
        public void Melee_Attack()
        {
            if(attack_Enable)
            {
                attack_Enable = false;
                state.work = Sta.Attack;
                AttackInfo info = new AttackInfo();
                info.ATk = stat.power;
                info.Effect = Effects.normal;
                info.AttackSpeed = stat.attackSpeed;
                info.AttackRange = stat.attackRange;
                Attack(meleeAttackCtr, info);
                Move(state.dir, 0.1f);
                StartCoroutine(WaitForAttack());
            }
        }
        IEnumerator WaitForAttack()
        {
            yield return new WaitForSeconds(stat.attackSpeed);
            state.work = Sta.Idle;
            attack_Enable = true;
        }


        public void Jump() //점프 입력시 호출될 함수
        {
            if (state.standing != Mov.Jump)
            {
                state.standing = Mov.Jump;
                state.work = Sta.Idle;
                Set_Ani();
                rb.velocity = new Vector2(0, stat.jump);
            }
        }

        public virtual void Move(Dir _dir)
        {
            Vector3 dirVec3 = new Vector3();
            Vector3 scale = transform.localScale;
            switch (_dir)
            {
                case Dir.Left:
                {
                    state.dir = Dir.Left;
                    scale.x = -Mathf.Abs(scale.x);
                    dirVec3 = Vector3.left;
                }
                    break;
                case Dir.Right:
                {
                    state.dir = Dir.Right;
                    scale.x = Mathf.Abs(scale.x);
                    dirVec3 = Vector3.right;
                }
                    break;
            }
            transform.localScale = scale;
            transform.Translate(stat.speed * Time.deltaTime * dirVec3);
        }
        IEnumerator LoopForMove(Dir _dir, float _time)
        {
            while(_time > 0.0f)
            {
                Move(_dir);
                yield return new WaitForSeconds(_time);
                _time -= 0.05f;
            }
        }
        float _timeForMov;
        public void Move(Dir _dir,float _time)
        {
            _timeForMov = Time.time;
            StartCoroutine(routine: LoopForMove(_dir: _dir,_time: _time));
        }
        public void Move_Right()    //우측 이동시 호출될 함수
        {
            Move(Dir.Right);
        }
        public void Move_Left() //좌측 이동시 호출될 함수
        {
            Move(Dir.Left);
        }
        public void Move_Stop()
        {
            state.work = Sta.Idle;
        }


        public virtual void Check_Death()
        {
            if (stat.hp <= 0)
            {
                state.death = true;
                Destroy(this.gameObject);
            }
        }
        

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            
        }
        public virtual void OnTriggerExit2D(Collider2D other)
        {
            
        }
        public virtual void OnTriggerStay2D(Collider2D other)
        {
            
        }

        public virtual void OnCollisionEnter2D(Collision2D _col)
        {
        }
        public virtual void OnCollisionStay2D(Collision2D _col)
        {
        }
        public virtual void OnCollisionExit2D(Collision2D _col)
        {
        }
    }

}
namespace MyData
{
    namespace Item
    {
        public interface IItem
        {
            bool IsEquipable();//장착 가능 여부
            void Equip();//장착
            void DeEquip();//장착 해제
            void Active();//엑티브 효과
            void Passive();//패시브 효과
        }
        [Serializable]
        public struct ItemState
        {
            public int Rare;// 레어도
            public List<float> stat;//레어도에 따른 고정스텟 수
        }
    }
    namespace Data
    {

    }

}

