using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fog : MonoBehaviour {
    Texture2D tempTex;

    // Use this for initialization
    void Start()
    {
        int a = Random.Range(0, 3);
        tempTex = new Texture2D(128, 128);
        Texture2D tex2d;
        tex2d = (Texture2D)gameObject.GetComponent<Renderer>().material.GetTexture(0);
        Debug.Log("Tex2d" + tex2d);
        tempTex.SetPixels32(tex2d.GetPixels32());
        tempTex.Apply();
        gameObject.GetComponent<Renderer>().material.mainTexture = tempTex;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v;
        Vector2 v2;

        if (Input.GetMouseButton(0))
        {
            v = Input.mousePosition;

            Debug.Log("mouseDown");

            v2.x = (v.x / Screen.width) * 128;
            v2.y = (v.y / Screen.height) * 128;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tempTex.SetPixel((int)v2.x + i, (int)v2.y + j, new Color(0, 0, 0, 0.0f));
                }
            }
            tempTex.Apply();
            Debug.Log(tempTex);

        }
    }
} 
