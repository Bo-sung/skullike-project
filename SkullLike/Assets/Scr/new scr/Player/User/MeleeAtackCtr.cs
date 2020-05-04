using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyData.Data;

public class MeleeAtackCtr : MonoBehaviour {

    Collider2D col2D;
    Renderer rd;
    Attack_info atk_info;
	// Use this for initialization
	void Awake () {
        col2D = GetComponent<Collider2D>();
        rd = GetComponent<Renderer>();
    }
    IEnumerator WaitForAttack(float _while)
    {
        yield return new WaitForSeconds(_while);
        col2D.enabled = false;
        rd.enabled = false;
    }
    public void Fire(Attack_info _atk_info)
    {
        col2D.enabled = true;
        rd.enabled = true;
        atk_info = _atk_info;
        StartCoroutine(WaitForAttack(atk_info.Attack_Speed));
    }
}
