using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScr;
using MyData.Data;

public class Magazine : MonoBehaviour {
    public GameObject Bullet;
    public List<GameObject> magazine;
    public float atkRange;
    public float atkSpeed;
    void Fire(AttackInfo _Atk_Info)
    {
        foreach (var bullet in magazine)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                _Atk_Info.AttackRange = atkRange;
                _Atk_Info.AttackSpeed = atkSpeed;
                bullet.SendMessage("Fire", _Atk_Info);
                return;
            }
        }
    }
    void Awake()
    {
        for(int i = 0; i < magazine.Count; ++i)
        {
            GameObject bullet = Instantiate(Bullet, transform);
            magazine[i] = bullet;
        }
    }
}
