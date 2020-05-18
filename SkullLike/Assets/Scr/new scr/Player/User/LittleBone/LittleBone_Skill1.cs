using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyData;
using MyData.PlayerScr;

public class LittleBone_Skill1 : Skill {
    public enum head_state { Idle, Flying, Waiting, delaytime};
    public head_state state;
    public float _power;
    private Vector2 dirVec2;
    void Start()
    {
        state = head_state.Idle;
    }
    public override void Active()
    {
        if(state == head_state.Idle)
        {
            state = head_state.Flying;
            switch (GetAttack_Info().direction)
            {
                case Dir.Left:
                    {
                        dirVec2 = Vector2.left;
                    }
                    break;
                case Dir.Right:
                    {
                        dirVec2 = Vector2.right;
                    }
                    break;
            }
        }
    }
    private void Update()
    {
        if(state == head_state.Flying)
        {
            transform.Translate(dirVec2 * Time.deltaTime * _power);
        }
    }
    public void OnCollisionEnter2D(Collision2D _col)
    {
        state = head_state.Waiting;
        if(_col.transform.tag == "Player")
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
