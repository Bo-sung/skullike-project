using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets;

public class GroundContainer : MonoBehaviour
{
    [Header("Map Settings")]
    public int Gap = 5;

    [Header("Assets")]
    public List<GameObject> typeList;
    public string mapPath;
    public TextAsset Map;
    [Header("DEBUG")]
    int size;
    Factory fac;
    private List<string> mapDataList = new List<string>();
    FileReader file;
    private void Awake()
    {
        size = transform.childCount;
        file= new FileReader(Map);
        mapDataList = file.textList;
    }    
    // Start is called before the first frame update
    void Start()
    {
        fac = new Factory();
        groundFac gfac = new groundFac();
        for (int y = 0; y < file.data.sizeY; ++y)
        {
            for(int x = 0;x < file.data.sizeX; ++x)
            {                            
                GameObject Grounds = gfac.Product(typeList[0], 0, typeList[0].name, "Ground");
                Grounds.transform.SetParent(gameObject.transform);
                Grounds.transform.position = new Vector3(x - file.data.center.x, 0, y - file.data.center.y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
