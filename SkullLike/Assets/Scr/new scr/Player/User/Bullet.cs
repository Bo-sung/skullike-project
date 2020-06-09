using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyData.Data;
using PlayerScr;

public class Bullet : MonoBehaviour {
    private AttackInfo m_info;
    private float _atkSpeed;
    Dir _direction;
    float _lifecycle;
    public void Fire(AttackInfo _info)
    {
        this.m_info = _info;
        _atkSpeed = this.m_info.AttackSpeed;
        
    }
    private void Update()
    {
        if(isActiveAndEnabled)
        {
                transform.Translate(_atkSpeed * Time.deltaTime * Vector2.right);
            if(transform.localPosition.x >= m_info.AttackRange || transform.localPosition.x <= -m_info.AttackRange)
            {
                transform.localPosition = new Vector2(0, 0);
                gameObject.SetActive(false);
                m_info = new AttackInfo();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {

        }
    }
}
