using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScr;

public class HangOn : MonoBehaviour {
    Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "GROUND")
        {
            GetComponentInParent<User>().SendMessage("HangJump");
        }
    }
}
