using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    MeshRenderer mr;
    BoxCollider bc;
    public struct Information
    {
        public int type_no
        {
            get;set;
        }
        public string type_name
        {
            get;set;
        }
        public int status
        {
            get;set;
        }
        public string name
        {
            get;set;
        }

    }
    public Information info;

    

    // Start is called before the first frame update
    void Start()
    {
        info = new Information();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (info.type_no)
        {
            case 0:
                {
                    mr.enabled = true;
                    bc.enabled = true;
                    break;
                }
            case 1:
                {
                    break;
                }
        }
    }
}
