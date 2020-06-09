using System;
using UnityEngine;
using System.Collections.Generic;
using PlayerScr;
using MyData.Item;
using MyData.Data;
using System.Collections;
using Random = System.Random;

namespace PlayerScr
{
    public class Enemy : Player
    {
        [Header("Enemy - Only")]
        public Sight Sight;
        private Vector3 Target;
        public enum TypeofMob {RandomMov,FollowPlayer}
        public TypeofMob typeofmobmove;
        private IEnumerator PatternCoroutine;

        public override void Initiallize()
        {
            base.Initiallize();
            PatternCoroutine = RandomChangeSideMovement();

            StartCoroutine(PatternCoroutine);
        }

        IEnumerator RandomChangeSideMovement()
        {
            Random MoveFlag = new Random();
            if (Sight.IsColOnWall)
            {
                state.dir = Swap(state.dir);
            }
            else if (MoveFlag.Next(0, 2) == 0)
            {
                state.dir = Swap(state.dir);
            }
            yield return new WaitForSeconds(1f);
            StartCoroutine("RandomChangeSideMovement");
        }

        IEnumerator FollowPlayerMovement()
        {
            if (Sight.IsPlayerOnSight)
            {
                Target = Sight.GetPlayerPos();
                transform.Translate(Target.normalized * Time.deltaTime);
            }
            yield return new WaitForSeconds(0.1f);
            StartCoroutine("FollowPlayerMovement");
        }
        public override void Set_Update()
        {
            if (Sight.IsPlayerOnSight)
            {
                StopCoroutine(PatternCoroutine);
                PatternCoroutine = FollowPlayerMovement();
                StartCoroutine(PatternCoroutine);
            }
            base.Set_Update();
            Move(state.dir);
        }
    }
}
        
     