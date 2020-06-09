using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using CamMove;
using PlayerScr;

public class allKeyInputDelegate : MonoBehaviour
{
    private static allKeyInputDelegate instance;
    public static allKeyInputDelegate Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<allKeyInputDelegate>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject("Gamemanager").AddComponent<allKeyInputDelegate>();
                    instance = newSingleton;
                }
                
            }
            return instance;
        }
        private set { instance = value; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    Dictionary<KeyCode, Action> keyDownDictionary;
    Dictionary<KeyCode, Action> keyUpDictionary;
    Dictionary<KeyCode, Action> keyDictionary;
    [Header("Object")]
    public User player;
    public GameObject UI;
    public GateCtr Gate;


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
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Q, Key_Q },
            { KeyCode.W, Key_W },
            { KeyCode.E, Key_E },
            { KeyCode.R, Key_R },
            { KeyCode.A, Key_A },
            { KeyCode.S, Key_S },
            { KeyCode.D, Key_D },
            { KeyCode.F, Key_F },
            { KeyCode.Z, Key_Z },
            { KeyCode.X, Key_X },
            { KeyCode.C, Key_C },
            { KeyCode.V, Key_V },
            { KeyCode.Space, Key_Space },
            { KeyCode.UpArrow, Key_UpArrow },
            { KeyCode.DownArrow, Key_DownArrow },
            { KeyCode.LeftArrow, Key_LeftArrow },
            { KeyCode.RightArrow, Key_RightArrow },
        };

    }
    private void KeyDown_Q() {  }
    private void KeyDown_W() { }
    private void KeyDown_E() { }
    private void KeyDown_R() { }
    private void KeyDown_A() { player.Skill(1); }
    private void KeyDown_S() { player.Skill(2); }
    private void KeyDown_D() { }
    private void KeyDown_F() { Gate.NextScene(Gate.NextSceneName); }
    private void KeyDown_Z() { player.Dash(); }
    private void KeyDown_X() {  }
    private void KeyDown_C() { player.Jump(); }
    private void KeyDown_V() { }
    private void KeyDown_Space() { }
    private void KeyDown_UpArrow() { }
    private void KeyDown_DownArrow() { }
    private void KeyDown_LeftArrow() {  }
    private void KeyDown_RightArrow() {  }
    
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
    
    private void Key_Q() { }
    private void Key_W() { }
    private void Key_E() { }
    private void Key_R() { }
    private void Key_A() { }
    private void Key_S() { }
    private void Key_D() { }
    private void Key_F() { }
    private void Key_Z() { }
    private void Key_X() {player.Melee_Attack(); }
    private void Key_C() { }
    private void Key_V() { }
    private void Key_Space() { }
    private void Key_UpArrow() { }
    private void Key_DownArrow() { }
    private void Key_LeftArrow() {player.Move_Left(); }
    private void Key_RightArrow() {player.Move_Right(); }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            foreach (var dic in keyDownDictionary)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
            foreach (var dic in keyDictionary)
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
