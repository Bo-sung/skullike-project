
using Statuses;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace MyData
{
    namespace PlayerScr
    {
        public interface IPlayer
        {
            void Move_Right();//현제 위치에서 우측으로 이동
            void Move_Left();//현제 위치에서 좌측으로 이동
            void Jump();//점프
            void Crouch();//수구리기
            void Move_Stop();
            void Skill(int _skillNum);//스킬사용(인덱스)
            void Attack(GameObject _target, int _atktype);//공격. 데미지를 리턴함
            void Attacked(float _damage);//공격받음(데미지). 데미지를 입력받아 현제 스텟에 따른 피해량을 저장
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
            public Mov Standing;
            public Sta Work;
            public Dir Dir;

            public bool death; //생사
        }
        [Serializable]
        public struct Status
        {
            public float jump;
            public float power;
            public float speed;
            public float HP;
            public float DEF;
            public float stamina;
            public float stamina_time;
        }
        [Serializable]
        public struct Inventory
        {
            public struct GemInventory
            {
                public GameObject HeadGem;
                public GameObject BodyGem;
                public GameObject BottomGem;
                public GameObject WeaponGem;
                public GameObject ShoesGem;
            }
            public struct etc_inventory
            {
                public List<GameObject> Inventory;
            }

        }
        //현제 상태 지정용 열거형들.
        public enum Mov { Stand, Jump, Crouch, Hang };//이동 상황
        public enum Dir { Left, Right, Up};//방향
        public enum Sta { idle, Move, Attack, Death };//상태
    }
    namespace Item
    {
        public enum equipmentSpace { Head, Body, Bottom, Weapon, Shoes, etc};
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
            public equipmentSpace Part;//착용부위
            public int Rare;// 레어도
            public List<float> stat;//레어도에 따른 고정스텟 수
        }
    }
}

