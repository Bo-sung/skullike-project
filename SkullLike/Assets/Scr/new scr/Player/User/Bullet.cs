using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyData.Data;
using MyData.PlayerScr;

public class Bullet : MonoBehaviour {
    private Attack_info info;
    private float atkSpeed;
    Dir direction;
    float Lifecycle;
    public void Fire(Attack_info _info)
    {
        info = _info;
        Debug.Log(_info.direction.ToString());
        atkSpeed = info.Attack_Speed;
        
    }
    private void Update()
    {
        if(isActiveAndEnabled)
        {
                transform.Translate(Vector2.right * atkSpeed * Time.deltaTime);
            if(transform.localPosition.x >= info.Attack_Range || transform.localPosition.x <= -info.Attack_Range)
            {
                transform.localPosition = new Vector2(0, 0);
                gameObject.SetActive(false);
                info = new Attack_info();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {

        }
    }
}
