using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using System.IO;

namespace Assets
{
    public class Factory : MonoBehaviour
    {
        public virtual GameObject Product(GameObject PreFab)
        {
            GameObject rtn = PreFab;
            return rtn;
        }
    }
    public class groundFac : Factory
    {
        public  GameObject Product(GameObject PreFab,int type_no, string type_name, string name)
        {            
            GameObject ground = Instantiate(PreFab, new Vector3(0,0,0), new Quaternion(0, 0, 0, 1));
            ground.GetComponent<Ground>().info.type_no = type_no;
            ground.GetComponent<Ground>().info.type_name = type_name;
            ground.GetComponent<Ground>().info.name = name;
            ground.GetComponent<Ground>().info.status = 0;
            return ground;
        }
    }

    public class FileReader
    {
        public TextAsset textAsset;
        public struct DATA
        {
            public string name;
            public Vector2 center;
            public int sizeX;
            public int sizeY;
            public int[,] mapArr;

        }
        public DATA data;
        public List<string> textList;
        public FileReader(TextAsset txt)
        {
            data = Parse(txt);
            textAsset = txt;
            Input(txt);
        }
        public void Input(TextAsset textAsset)
        {
            textList = new List<string>();
            StringReader reader = new StringReader(textAsset.text);
            while (true)
            {
                string data = reader.ReadLine();
                if (data == null)
                    break;
                textList.Add(data);
            }

        }
        public DATA Parse(TextAsset textAsset)
        {
            DATA data;
            StringReader reader = new StringReader(textAsset.text);
            data.name = reader.ReadLine();
            string[] size = reader.ReadLine().Split(' ');
            data.sizeX = int.Parse(size[0]);
            data.sizeY = int.Parse(size[1]);
            data.mapArr = new int[data.sizeY, data.sizeX];
            data.center = new Vector2(data.sizeX / 2.0f, data.sizeY / 2.0f);
            for (int y = 0; y < data.sizeY; ++y)
            {
                string[] arr = reader.ReadLine().Split(' ');
                for (int x = 0; x < data.sizeX; ++x)
                {
                    data.mapArr[y, x] = int.Parse(arr[x]);
                }
            }
            return data;
        }

    }
}
