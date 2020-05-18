using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScr;
using MyData.PlayerScr;

public class LittleBoneCtr : User {

    public LittleBone_Skill1 skill_1;
    public Skill skill_2;
    void Start()
    {
        //skill_1.InitInformation()
    }
    public void Skill(int _skillNum)
    {
        switch (_skillNum)
        {
            case 1:
                {
                    skill_1.Active();
                }break;
        }

    }
}
