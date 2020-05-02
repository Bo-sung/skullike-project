using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMon : MonoBehaviour {

    Animator ani;
	// Use this for initialization
	void Start ()
    {
        ani = GetComponent<Animator>();
        ani.SetTrigger("Test0");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
