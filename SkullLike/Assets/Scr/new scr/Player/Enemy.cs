using System;
using PlayerScr;
using UnityEngine;

namespace scr.PlayerScr
{
   // public class Enemy : Player
   // {
   //     public base_loop map;
   //     public float m_power;
   //     private Collider head;
   //     void Start()
   //     {
   //         head = transform.GetChild(0).GetComponent<Collider>();
   //         this.GetComponent<destroySelf>().activation = false;
   //         speed = map.speed;
   //         power = m_power;
   //     }
   //
   //     public override void Update()
   //     {
   //         base.Update();
   //         speed = map.speed;
   //         Move();
   //     }
   //
   //     //public override void Attack(GameObject target)
   //     //{
   //     //    target.GetComponent<User>().Attacked(power);
   //     //    Destroy_self();
   //     //}
   //
   //     public override void Attacked()
   //     {
   //         Destroy_self();
   //     }
   //
   //     private void Destroy_self()
   //     {
   //         gameObject.SetActive(false);
   //         //this.GetComponent<destroySelf>().activation = true;
   //     }
   //     
   //     
   //     
   //
   //     void Move()
   //     {
   //         transform.Translate(Vector3.right * speed * Time.deltaTime);
   //     }
   //
   //     
   //
   //     public override void OnColEnter(Collision col)
   //     {
   //         base.OnColEnter(col);
   //         if (col.transform.CompareTag("Player"))
   //         {
   //             //Attack(col.gameObject);
   //         }
   //         
   //     }
   // }
}