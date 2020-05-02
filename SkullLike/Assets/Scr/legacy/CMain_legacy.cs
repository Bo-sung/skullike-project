//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//public class CMain : MonoBehaviour
//{
//    enum mapType
//    {

//    }
//    const int mapX = 20;
//    const int mapY = 15;
//    int[,] map = {
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//        { 1,1,1,1,1,1,0,0,1,1,0,0,1,1,1,1,1,1,0,0},
//    };

//    List<string> m_tile = new List<string>();
//    // Use this for initialization
//    List<GameObject> blockArr = new List<GameObject>();
//    void Start()
//    {

//        TextAsset data = Resources.Load("map", typeof(TextAsset)) as TextAsset;
//        StringReader sr = new StringReader(data.text);

//        string line;
//        line = sr.ReadLine();
//        while (line != null)
//        {
//            //Debug.Log(line);
//            if (line.Equals("TileStart"))
//            {
//                while (true)
//                {
//                    line = sr.ReadLine();
//                    if (line.Equals("TileEnd"))
//                        break;
//                    line = line.Replace(".png", "");
//                    m_tile.Add(line);

//                }
//            }

//            line = sr.ReadLine();
//        }



//        for (int y = 0; y < mapY; ++y)
//        {
//            for (int x = 0; x < mapX; ++x)
//            {
//                if (map[y, x] == 0)
//                    continue;
//                GameObject block = (GameObject)Instantiate(GameObject.FindWithTag("blockTag"));
//                block.transform.SetPositionAndRotation(new Vector3(x * 0.5f, (mapY - y - 1) * 0.5f, -1), Quaternion.identity);

//                SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();
//                Sprite sp = Resources.Load<Sprite>(m_tile[map[y, x]]);

//                Sprite sameSprite = Sprite.Create(sp.texture, sp.rect, new Vector2(0, 0), 64);
//                spriteRenderer.sprite = sameSprite;
//                Debug.Log(spriteRenderer.sprite.pivot);

//                blockArr.Add(block);
//            }
//        }



//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CMain_legacy : MonoBehaviour
{
    enum mapType
    {
   
    }
    const int mapX = 20;
    const int mapY = 15;
    int[,] map = {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //1
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //2
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //3
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //4
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //5
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //6
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //7
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //8
        { 0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0},       //9
        { 0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0},       //10
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //11
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //12
        { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //13
        { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       //14
        { 1,1,1,1,1,1,0,0,1,1,0,0,1,1,1,1,1,1,0,0},       //15
    };
   
    List<GameObject> blockArr = new List<GameObject>();
	// Use this for initialization
	void Start ()
    {
        //TextAsset data = Resources.Load("map", typeof(TextAsset)) as TextAsset;
        //StringReader sr = new StringReader(data.text);

        //string line;
        //line = sr.ReadLine();
        //while (line != null)
        //{
        //    Debug.Log(line);
        //    line = sr.ReadLine();
        //}
        Debug.Log(1);
		for(int y = 0; y < mapY; ++y)
        {
            for(int x = 0; x < mapX; ++x)
            {
                if(map[y,x] == 0)
                {
                    continue;
                }
                GameObject block = (GameObject)Instantiate(GameObject.FindWithTag("Block"));
                block.transform.SetPositionAndRotation(new Vector3(x * 0.5f, (mapY - y - 1) * 0.5f, -1), Quaternion.identity);
                blockArr.Add(block);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
