﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyData;
using PlayerScr;

namespace PlayerScr
{
    public class MeleeAtackCtr : MonoBehaviour
    {

        Collider2D col2D;
        Renderer rd;

        AttackInfo atk_info;

        // Use this for initialization
        void Awake()
        {
            col2D = GetComponent<Collider2D>();
            rd = GetComponent<Renderer>();
        }

        IEnumerator WaitForAttack(float _while)
        {
            yield return new WaitForSeconds(_while);
            col2D.enabled = false;
            rd.enabled = false;
        }

        public void Fire(AttackInfo _atk_info)
        {
            atk_info = _atk_info;
            col2D.enabled = true;
            rd.enabled = true;
            transform.localScale = new Vector3(_atk_info.AttackRange * 0.2f, transform.localScale.y, 1);
            StartCoroutine(WaitForAttack(atk_info.AttackSpeed));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.tag == "Enemy")
            {
                col.SendMessage("Attacked", atk_info);
            }
        }
    }
}
