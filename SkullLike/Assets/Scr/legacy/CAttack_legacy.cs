using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAttack_legacy : MonoBehaviour {

    const int Mag = 35;
	// Use this for initialization
	void Start () {
        GameObject Bullet = (GameObject)Instantiate(GameObject.FindWithTag("Bullet"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
