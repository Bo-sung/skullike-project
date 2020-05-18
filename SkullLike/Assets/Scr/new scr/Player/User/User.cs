using MyData.PlayerScr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScr
{
    public class User : MonoBehaviour, IUser
    {
        [Header("Effects")]
        public List<GameObject> effectList;

        private void Set_Ani()
        {
            if (ani.GetInteger("Sta") != (int)state.Work)
            {
                ani.SetInteger("Sta", (int)state.Work);
                ani.Update(1);
            }
            if (ani.GetInteger("Mov") != (int)state.Standing)
            {
                ani.SetInteger("Mov", (int)state.Standing);
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
        private bool IsDownJumpEnable;
        public Inventory inventory; //인벤토리
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
        public bool attack_Enable;

        public void Skill(int _skillNum)
        {

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
                info.Attack_Range = stat.attack_Range;
                Attack(meleeAttackCtr, info);
                Move(state.Dir, 0.1f);
                StartCoroutine(WaitForAttack());
            }
        }
        IEnumerator WaitForAttack()
        {
            yield return new WaitForSeconds(stat.attack_Speed);
            state.Work = Sta.idle;
            attack_Enable = true;
        }

        public void Attack(GameObject _Magazine, Attack_info _Atk_Info)    //상대방에게 공격이 성공할시 호출할 함수
        {
            _Magazine.SendMessage("Fire", _Atk_Info);
        }
        public void Attacked(Attack_info _Atk_Info) //피해를 입을시 호출될 함수
        {
            stat.HP = stat.HP - (_Atk_Info.ATk - stat.DEF);
        }
        public void Dash()
        {
            switch (state.Dir)
            {
                case Dir.Left:
                    {
                        transform.Translate(Vector2.left * stat.speed * 0.125f);
                    }
                    break;
                case Dir.Right:
                    {
                        transform.Translate(Vector2.right * stat.speed * 0.125f);
                    }
                    break;
            }
        }
        public void Jump()  //점프 입력시 호출될 함수
        {
            if (state.Standing != Mov.Jump)
            {
                if(Input.GetKey(KeyCode.DownArrow) && IsDownJumpEnable)
                {
                    DownJump();
                }
                else
                {
                    state.Standing = Mov.Jump;
                    state.Work = Sta.idle;
                    Set_Ani();
                    rb.velocity = new Vector2(0, stat.jump);
                }
            }            
        }
        public void DownJump()    //웅크리기 시행시 호출될 함수
        {
            state.Standing = Mov.Crouch;
            state.Work = Sta.idle;
            Set_Ani();
            GetComponent<Collider2D>().isTrigger = true;
            StartCoroutine(WaitforDownJump());
        }
        IEnumerator WaitforDownJump()
        {
            yield return new WaitForSeconds(0.25f);
            GetComponent<Collider2D>().isTrigger = false;
        }
        public void Move(Dir _dir)
        {
            if (state.Work == Sta.Attack && state.Standing == Mov.Jump)
            {
                Vector3 dirVec3 = new Vector3();
                Vector3 scale = transform.localScale;
                switch (_dir)
                {
                    case Dir.Left:
                        {
                            state.Dir = Dir.Left;
                            scale.x = -Mathf.Abs(scale.x);
                            dirVec3 = Vector3.left;
                        }
                        break;
                    case Dir.Right:
                        {
                            state.Dir = Dir.Right;
                            scale.x = Mathf.Abs(scale.x);
                            dirVec3 = Vector3.right;
                        }
                        break;
                }
                transform.localScale = scale;
                transform.Translate(dirVec3 * stat.speed * Time.deltaTime);
            }
            else if(state.Work != Sta.Attack)
            {
                Vector3 dirVec3 = new Vector3();
                Vector3 scale = transform.localScale;
                switch (_dir)
                {
                    case Dir.Left:
                        {
                            state.Dir = Dir.Left;
                            scale.x = -Mathf.Abs(scale.x);
                            dirVec3 = Vector3.left;
                        }
                        break;
                    case Dir.Right:
                        {
                            state.Dir = Dir.Right;
                            scale.x = Mathf.Abs(scale.x);
                            dirVec3 = Vector3.right;
                        }
                        break;
                }
                transform.localScale = scale;
                transform.Translate(dirVec3 * stat.speed * Time.deltaTime);
                if (state.Standing != Mov.Jump)
                    state.Work = Sta.Move;
            }
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
        float timeForMov;
        public void Move(Dir _dir,float _time)
        {
            timeForMov = Time.time;
            StartCoroutine(LoopForMove(_dir,_time));
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
            state.Work = Sta.idle;
        }
        public void HangJump()
        {
            //Jump();
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
                if(state.Work != Sta.Attack)
                    state.Work = Sta.idle;
            }
            if (state.death == true)
            {
                state.Work = Sta.Death;
                state.Standing = Mov.Hang;
            }
            Set_Ani(ani, state.Standing, state.Work);
        }

        public void OnCollisionEnter2D(Collision2D _col)
        {
            if (_col.transform.tag == "GROUND")
            {                
                if (state.Standing == Mov.Jump || state.Standing == Mov.Hang)
                {
                    state.Work = Sta.idle;
                    state.Standing = Mov.Stand;
                }
            }
        }
        public void OnCollisionStay2D(Collision2D _col)
        {
            if(_col.transform.tag == "GROUND" && _col.transform.name == "halfGround")
            {
                IsDownJumpEnable = true;
            }
        }
        public void OnCollisionExit2D(Collision2D _col)
        {
            if (_col.transform.tag == "GROUND" && _col.transform.name == "halfGround")
            {
                IsDownJumpEnable = false;
            }
        }
    }
}