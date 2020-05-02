using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMain : MonoBehaviour
{

    CCam cam;
    CUI UI;

    public GameObject Cameras;
    public GameObject tetris0;
    public GameObject tetris1;
    public GameObject tetris2;
    public GameObject tetris3;
    public GameObject tetris4;
    public GameObject tetris5;
    public GameObject tetris6;
    public GameObject tetrisNULL;
    
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CCam>();
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<CUI>();
    }

    enum mapType
    {

    }
    const int mapX = 19;
    const int mapY = 15;
    const float Cellsize = 0.64f;
    const float Block_Z = -2;
    int[,] map = {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //1
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //2
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //3
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //4
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //5
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //6
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //7
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //8
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //9
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //10
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //11
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},       //12
        { 0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,},       //13
        { 0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,},       //14
        { 0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0 }        //15
      //{ 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},       //15
      //{ 1,9,8,7,6,5,4,3,2,1,2,3,4,5,6,7,8,9,1,},
    };
    public float GetCellSize() { return Cellsize; }
    public int GetMapSize_X() { return mapX; }
    public int GetMapSize_Y() { return mapY; }

    List<GameObject> blockArr = new List<GameObject>();
    List<int> BlockOrder = new List<int>();//7번쨰 자리마다 -1;
    public int GetBlockOrder()
    {
        if(BlockOrder[0] == 7)
        {
            Random7();
            BlockOrder.RemoveAt(0);
        }
        int _return = BlockOrder[0];
        BlockOrder.RemoveAt(0);
        return _return;
    }
    public void DelBlockOrder()
    {
        BlockOrder.RemoveAt(0);
    }
    List<int> randarr = new List<int>();
    // Use this for initialization
    //int k = 0;
    void Random7()
    {
        randarr.Add(0); randarr.Add(1); randarr.Add(2);
        randarr.Add(3); randarr.Add(4); randarr.Add(5);
        randarr.Add(6);
        for (int i = 0; i < 7; ++i)
        {
            int rand = Random.Range(0, randarr.Count);
            BlockOrder.Add(randarr[rand]);
            randarr.RemoveAt(rand);
            Debug.Log(BlockOrder[i]);
        }
        BlockOrder.Add(7);
    }
    void Start()
    {
        for (int y = 0; y < mapY; ++y)
        {
            for (int x = 0; x < mapX; ++x)
            {
                if (map[y, x] == 0)
                {
                    continue;
                }
                GameObject block = (GameObject)Instantiate(GameObject.FindWithTag("wallblock1"));
                block.transform.SetPositionAndRotation(new Vector3(x * Cellsize, (mapY - y - 1) * Cellsize, -1), Quaternion.identity);
                blockArr.Add(block);
            }
        }
        Random7();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            UI.SpawnBlock();
        }
    }
}

