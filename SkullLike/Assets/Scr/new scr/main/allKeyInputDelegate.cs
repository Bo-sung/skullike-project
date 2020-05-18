using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using CamMove;
using PlayerScr;

public class allKeyInputDelegate : MonoBehaviour
{
    Dictionary<KeyCode, Action> keyDownDictionary;
    Dictionary<KeyCode, Action> keyUpDictionary;
    [Header("Object")]
    public User player;
    public GameObject UI;


    // Start is called before the first frame update
    void Start()
    {        
        keyDownDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Q, KeyDown_Q },
            { KeyCode.W, KeyDown_W },
            { KeyCode.E, KeyDown_E },
            { KeyCode.R, KeyDown_R },
            { KeyCode.A, KeyDown_A },
            { KeyCode.S, KeyDown_S },
            { KeyCode.D, KeyDown_D },
            { KeyCode.F, KeyDown_F },
            { KeyCode.Z, KeyDown_Z },
            { KeyCode.X, KeyDown_X },
            { KeyCode.C, KeyDown_C },
            { KeyCode.V, KeyDown_V },
            { KeyCode.Space, KeyDown_Space },
            { KeyCode.UpArrow, KeyDown_UpArrow },
            { KeyCode.DownArrow, KeyDown_DownArrow },
            { KeyCode.LeftArrow, KeyDown_LeftArrow },
            { KeyCode.RightArrow, KeyDown_RightArrow },
        };
        keyUpDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Q, KeyUp_Q },
            { KeyCode.W, KeyUp_W },
            { KeyCode.E, KeyUp_E },
            { KeyCode.R, KeyUp_R },
            { KeyCode.A, KeyUp_A },
            { KeyCode.S, KeyUp_S },
            { KeyCode.D, KeyUp_D },
            { KeyCode.F, KeyUp_F },
            { KeyCode.Z, KeyUp_Z },
            { KeyCode.X, KeyUp_X },
            { KeyCode.C, KeyUp_C },
            { KeyCode.V, KeyUp_V },
            { KeyCode.Space, KeyUp_Space },
            { KeyCode.UpArrow, KeyUp_UpArrow },
            { KeyCode.DownArrow, KeyUp_DownArrow },
            { KeyCode.LeftArrow, KeyUp_LeftArrow },
            { KeyCode.RightArrow, KeyUp_RightArrow },
        };

    }
    private void KeyDown_Q() {  }
    private void KeyDown_W() { }
    private void KeyDown_E() { }
    private void KeyDown_R() { }
    private void KeyDown_A() { player.Skill(1); }
    private void KeyDown_S() { player.Skill(2); }
    private void KeyDown_D() { }
    private void KeyDown_F() { }
    private void KeyDown_Z() { player.Dash(); }
    private void KeyDown_X() { player.Melee_Attack(); }
    private void KeyDown_C() { player.Jump(); }
    private void KeyDown_V() { }
    private void KeyDown_Space() { }
    private void KeyDown_UpArrow() { }
    private void KeyDown_DownArrow() { }
    private void KeyDown_LeftArrow() { player.Move_Left(); }
    private void KeyDown_RightArrow() { player.Move_Right(); }
    
    private void KeyUp_Q() { }
    private void KeyUp_W() { }
    private void KeyUp_E() { }
    private void KeyUp_R() { }
    private void KeyUp_A() { }
    private void KeyUp_S() { }
    private void KeyUp_D() { }
    private void KeyUp_F() { }
    private void KeyUp_Z() { }
    private void KeyUp_X() { }
    private void KeyUp_C() { }
    private void KeyUp_V() { }
    private void KeyUp_Space() {  }
    private void KeyUp_UpArrow() { }
    private void KeyUp_DownArrow() {  }
    private void KeyUp_LeftArrow() { player.Move_Stop(); }
    private void KeyUp_RightArrow() { player.Move_Stop(); }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            foreach (var dic in keyDownDictionary)
            {
                if (Input.GetKey(dic.Key))
                {
                    dic.Value();
                }
            }
            foreach (var dic in keyUpDictionary)
            {
                if (Input.GetKeyUp(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }
}
