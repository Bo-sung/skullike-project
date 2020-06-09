using System;
using System.Collections;
using System.Collections.Generic;
using LittleBone;
using UnityEngine;
using PlayerScr;

namespace LittleBone
{
    public enum Head_State { Flying, Waiting, delaytime};
}
public class LittleBoneCtr : User {
    [Header("LittleBone - Only")]
    public bool headEnable;

    [SerializeField]private bool teleportable = true;
    public float headDamage;
    private CapsuleCollider2D _capsuleCollider2D;
    public override void Initiallize()
    {
        base.Initiallize();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void SetHeadState()
    {
        if (!headEnable)
        {
            _capsuleCollider2D.offset = new Vector2(0.01f,0.075f);
            _capsuleCollider2D.size = new Vector2(0.175f, 0.3f);
        }
        else
        {
            _capsuleCollider2D.offset = new Vector2(0.0f,0.175f);
            _capsuleCollider2D.size = new Vector2(0.175f, 0.5f);
        }

        
    }
    public override void Skill(int _skillNum)
    {
        switch (_skillNum)
        {
            case 1:
            {
                if (!LittleBoneHead.Instance.isActiveAndEnabled)
                {
                    
                    StartCoroutine(waitforHeadShot(state.work));
                    state.work = Sta.Skill;
                    headEnable = false;
                    rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                    //StartCoroutine(WaitForSkill(1));
                    AttackInfo _atkinfo = new AttackInfo();
                    _atkinfo.Direction = state.dir;
                    _atkinfo.ATk = headDamage;
                    SetHeadState();
                    LittleBoneHead.Instance.gameObject.SetActive(true);
                    LittleBoneHead.Instance.initiallize(transform.position + new Vector3(0,0.2f,0),_atkinfo );
                    rb.velocity = new Vector2();
                    StartCoroutine(WaitForHead(6f));
                }
            }break;
            case 2:
            {
                if (LittleBoneHead.Instance.isActiveAndEnabled && teleportable)
                {
                    Teleport(LittleBoneHead.Instance.transform.position);
                    teleportable = false;
                    StartCoroutine(WaitForSkill(3));
                    rb.velocity = new Vector2();
                    headEnable = true;
                    SetHeadState();
                }
            }
                break;
        }
    }
    IEnumerator WaitForSkill(float _time)
    {
        yield return new WaitForSeconds(_time);
        teleportable = true;

    }
    IEnumerator waitforHeadShot(Sta _originSta)
    {
        yield return new WaitForSeconds(0.25f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        state.work = _originSta;
    }
    
    IEnumerator WaitForHead(float time)
    {
        yield return new WaitForSeconds(time);
        headEnable = true;
        SetHeadState();
    }
    

    public void Teleport(Vector3 _pos)
    {
        transform.position = _pos;// + new Vector3(0,1,0);
        RaycastHit _raycastHit;
    }

    public  override void OnCollisionEnter2D(Collision2D _col)
    {
        base.OnCollisionEnter2D(_col);
        //if (_col.transform.position.y >= transform.position.y && _col.transform.CompareTag("GROUND"))
        //{
        //    var transform1 = transform;
        //    var position = transform1.position;
        //    position = new Vector3(position.x, _col.transform.position.y + 0.75f, position.z);
        //    transform1.position = position;
        //}
        
    }
}
