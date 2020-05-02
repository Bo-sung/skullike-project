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
    private string state;
    [Header("Object")]
    public User player;
    public GameObject UI;


    // Start is called before the first frame update
    void Start()
    {        
        keyDownDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.A, KeyDown_A },
            { KeyCode.S, KeyDown_S },
            { KeyCode.D, KeyDown_D },
            { KeyCode.W, KeyDown_W },
            { KeyCode.Space, KeyDown_Space },
            { KeyCode.C, KeyDown_C },
            { KeyCode.Q, KeyDown_Q },
            { KeyCode.E, KeyDown_E },
            { KeyCode.R, KeyDown_R },
            { KeyCode.Z, KeyDown_Z },
            { KeyCode.UpArrow, KeyDown_UpArrow },
            { KeyCode.DownArrow, KeyDown_DownArrow },
            { KeyCode.LeftArrow, KeyDown_LeftArrow },
            { KeyCode.RightArrow, KeyDown_RightArrow },
        };
        keyUpDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.A, KeyUp_A },
            { KeyCode.S, KeyUp_S },
            { KeyCode.D, KeyUp_D },
            { KeyCode.W, KeyUp_W },
            { KeyCode.Space, KeyUp_Space },
            { KeyCode.C, KeyUp_C },
            { KeyCode.Q, KeyUp_Q },
            { KeyCode.E, KeyUp_E },
            { KeyCode.R, KeyUp_R },
            { KeyCode.Z, KeyUp_Z },
            { KeyCode.UpArrow, KeyUp_UpArrow },
            { KeyCode.DownArrow, KeyUp_DownArrow },
            { KeyCode.LeftArrow, KeyUp_LeftArrow },
            { KeyCode.RightArrow, KeyUp_RightArrow },
        };

    }
    private void KeyDown_Q() { }
    private void KeyDown_W() { }
    private void KeyDown_E() { }
    private void KeyDown_R() { }
    private void KeyDown_A() { }
    private void KeyDown_S() { }
    private void KeyDown_D() { }
    private void KeyDown_F() { }
    private void KeyDown_Z() { }
    private void KeyDown_X() { }
    private void KeyDown_C() { }
    private void KeyDown_V() { }
    private void KeyDown_Space() { player.Jump(); }
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
    private void KeyUp_DownArrow() { }
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
            //foreach (var dic in keyUpDictionary)
            //{
            //    if (Input.GetKey(dic.Key))
            //    {
            //        dic.Value();
            //    }
            //}
        }
    }
}
