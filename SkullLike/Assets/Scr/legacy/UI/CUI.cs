using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUI : MonoBehaviour {

    CMain main;
    CCam cam;
    GameObject nextblock;
    GameObject nowblock;
    GameObject holdblock;

    const float Block_Z = -2;
    public struct UI_Display
    {
        public float now_x;
        public float now_y;
        public float next_x;
        public float next_y;
        public float next_gap;
    }
    UI_Display UID;
    public void SetUID_now_x(float _x) { UID.now_x = _x; }
    public void SetUID_now_y(float _y) { UID.now_y = _y; }
    public void SetUID_next_x(float _x) { UID.next_x = _x; }
    public void SetUID_next_y(float _y) { UID.next_y = _y; }
    public void SetUID_next_gap(float _gap) { UID.next_gap = _gap; }

    public float GetUID_now_x() { return UID.now_x; }
    public float GetUID_now_y() { return UID.now_y; }
    public float GetUID_next_x() { return UID.next_x; }
    public float GetUID_next_y() { return UID.next_y; }
    public float GetUID_next_gap() { return UID.next_gap; }

    public void UID_Refresh()
    {
        UID.now_x = main.GetCellSize() * 8.5f;
        UID.now_y = cam.GetCamY() + 3.5f;
        UID.next_x = 13.5f;
        UID.next_y = cam.GetCamY() + 0.54f;
        UID.next_gap = 1.6f;
    }

    private void Awake()
    {
        main = GameObject.Find("CMain").GetComponent<CMain>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CCam>();
        UID_Refresh();
    }
    // Use this for initialization
   
    List<GameObject> GB = new List<GameObject>();
    public void HoldBlock()
    {
        float hold_x = 1.45f;
        float hold_y = 10f;
        GameObject _temp;

        _temp = nowblock;
        nowblock = holdblock;
        holdblock = _temp;

        holdblock.GetComponent<CBlock>().BlockTeleport(hold_x, hold_y, -2);
        holdblock.GetComponent<CBlock>().SetBlocks_DISPLAY(true);
        holdblock.GetComponent<CBlock>().SetBlocks_STATE(false);

        nowblock.GetComponent<CBlock>().BlockTeleport(UID.now_y,UID.now_y, -2);
        nowblock.GetComponent<CBlock>().SetBlocks_DISPLAY(false);
        nowblock.GetComponent<CBlock>().SetBlocks_STATE(true);
    }
    public void NextBlockArr()
    {
        UID_Refresh();
        for (int i = 0; i < GB.Count; ++i)
        {
            GB[i].GetComponent<CBlock>().BlockTeleport(UID.next_x, UID.next_y + (UID.next_gap * (2 - i)), -2);
            if (GB[i].GetComponent<CBlock>().GetBlocks_TYPE() == 0)
            {
                GB[i].GetComponent<CBlock>().BlockTeleport(UID.next_x - 0.3f, UID.next_y + (UID.next_gap * (2 - i)) - 0.2f, -2);
            }
            if (GB[i].GetComponent<CBlock>().GetBlocks_TYPE() == 2)
            {
                GB[i].GetComponent<CBlock>().BlockTeleport(UID.next_x, UID.next_y + (UID.next_gap * (2 - i)) - 0.3f, -2);
            }
        }
    }    
    public void Generating_Block(float _x, float _y, float _z)
    {
        int _order = main.GetBlockOrder();
        if (_order == 0)
        {
            nextblock = Instantiate(main.tetris0, new Vector3(_x - 0.3f, _y - 0.2f, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            cblock.SetBlocks_TYPE(_order);
        }
        if (_order == 1)
        {
            nextblock = Instantiate(main.tetris1, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            cblock.SetBlocks_TYPE(_order);
        }
        if (_order == 2)
        {
            nextblock = Instantiate(main.tetris2, new Vector3(_x, _y - 0.3f, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            cblock.SetBlocks_TYPE(_order);
        }
        if (_order == 3)
        {
            nextblock = Instantiate(main.tetris3, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            cblock.SetBlocks_TYPE(_order);
        }
        if (_order == 4)
        {
            nextblock = Instantiate(main.tetris4, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            cblock.SetBlocks_TYPE(_order);
        }
        if (_order == 5)
        {
            nextblock = Instantiate(main.tetris5, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            cblock.SetBlocks_TYPE(_order);
        }
        if (_order == 6)
        {
            nextblock = Instantiate(main.tetris6, new Vector3(_x, _y, _z), Quaternion.identity);
            CBlock cblock = nextblock.GetComponent<CBlock>();
            cblock.SetBlocks_Gen();
            cblock.SetBlocks_DISPLAY(true);
            cblock.SetBlocks_TYPE(_order);
        }
    }
    public void Save_BlockArr()
    {
        GB.Add(nextblock);
    }
    List<GameObject> blockArr = new List<GameObject>();
    private bool blockadd = true;
    public bool GetBlockAdd() { return blockadd; }
    public void SetBlockAdd(bool _on) { blockadd = _on; }
    public void SpawnBlock()
    {
        if (blockadd)
        {
           Generating_Block(GetUID_next_x(),GetUID_next_y(), Block_Z);
           Save_BlockArr();
        }
    }

    List<GameObject> TetrisArr = new List<GameObject>();
    public void AddTetrisArr(GameObject _data)
    {
        TetrisArr.Add(_data);
    }
    public void TetrisArr_Remove(int _arr)
    {
        TetrisArr.RemoveAt(_arr);
    }
    private float blockstop()
    {
        float top = 0.0f;
        for (int i = 0; i < TetrisArr.Count - 1; ++i)
        {
            if (TetrisArr[i].transform.position.y >= TetrisArr[i + 1].transform.position.y)
            {
                top = TetrisArr[i].transform.position.y;
            }
        }
        Debug.Log("top : " + top);
        top = top - (top % 0.5f);
        return top;
    }


    void Start()
    {
        nowblock = Instantiate(main.tetris0, new Vector3(0,0,0), Quaternion.identity);
        CBlock cblock = nextblock.GetComponent<CBlock>();
        cblock.SetBlocks_Gen();
        cblock.SetBlocks_DISPLAY(false);
        cblock.SetBlocks_TYPE(999);
        for (int i = 0; i < 3; ++i)
        {
            Generating_Block(UID.next_x, UID.next_y, Block_Z);
            Save_BlockArr();
            //NextBlockArr();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            cam.CamMove(1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            cam.CamMove(-1);
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            HoldBlock();
        }
        if (!nowblock.GetComponent<CBlock>().GetBlocks_STATE())
        {
            TetrisArr.Add(nowblock);
            GB[0].GetComponent<CBlock>().BlockTeleport(UID.now_x, UID.now_y, -2);
            GB[0].GetComponent<CBlock>().SetBlocks_DISPLAY(false);
            GB[0].GetComponent<CBlock>().SetBlocks_STATE(true);
            nowblock = GB[0];
            GB.RemoveAt(0);
            SpawnBlock();
        }
    }
    
    private void LateUpdate()
    {
        NextBlockArr();
        // UID_Refresh();
    }
}
