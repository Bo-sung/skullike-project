using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlock : MonoBehaviour {

    CUI UI;
    CompositeCollider2D rp;
    Rigidbody2D rb;
    Renderer rd;
    Animator ani;

    GameObject child_block0;
    GameObject child_block1;
    GameObject child_block2;
    GameObject child_block3;

    public bool destroy = false;
    private int time = 0;
    private struct Blocks
    {
        public float x; //x pos
        public float y; //y pos
        public int rot; //블럭 방향
        public float mass;  //무계
        public bool state;  //상태
        public bool display;    //출력
        public bool Genesis;
        public int type;
    }
    Blocks blocks;
    public float GetBlocks_X() { return blocks.x; }
    public float GetBlocks_Y() { return blocks.y; }
    public float GetBlocks_MASS() { return blocks.mass; }
    public int GetBlocks_ROT() { return blocks.rot; }
    public bool GetBlocks_STATE() { return blocks.state; }
    public bool GetBlocks_DISPLAY() { return blocks.display; }
    public int GetBlocks_TYPE() { return blocks.type; }

    public void SetBlocks_X(float _x) { blocks.x = _x; }
    public void SetBlocks_Y(float _y) { blocks.y = _y; }
    public void SetBlocks_MASS(float _mass) { blocks.mass = _mass; }
    public void SetBlocks_ROT(int _rot) { blocks.rot = _rot; }
    public void SetBlocks_STATE(bool _state) { blocks.state = _state; }
    public void SetBlocks_Gen() { blocks.Genesis = false; }
    public void SetBlocks_TYPE(int _type) { blocks.type = _type; }
    public void SetBlocks_DISPLAY(bool _disp)
    {
        blocks.display = _disp;
        if (_disp)
        {
            SetBlocks_STATE(false);
        }
    }




    private struct Maps
    {
        public float max_x;
        public float min_x;
        public float Cellsize;
    }
    Maps map;    
    // Use this for initialization
    
    public void objDestroy()
    {
        Destroy(gameObject);
    }
    public void BlockTeleport(float _x, float _y, float _z)
    {
        transform.position = new Vector3(_x, _y, _z);
    }
    void Awake()
    {
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<CUI>();
        map.max_x = 10.64f;
        map.min_x = 0.64f;
        map.Cellsize = 0.64f;//cmain.GetCellSize();
        SetBlocks_STATE(false);
        SetBlocks_ROT(1);
        blocks.Genesis = true;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rp = GetComponent<CompositeCollider2D>();
        rb.gravityScale = 0;
        rp.isTrigger = true;
        //Debug.Log("awake");
    }

    void Start ()
    {
    }
    public void toggle_4_P()
    {
        if (blocks.rot == 4)
        {
            blocks.rot = 1;
        }
        else
        {
            blocks.rot++;
        }
    }
    public void toggle_4_M()
    {
        if (blocks.rot == 4)
        {
            blocks.rot = 1;
        }
        else
        {
            blocks.rot--;
        }
    }
    private void BlockColl()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
        blocks.x = transform.position.x;
        blocks.y = transform.position.y;
        if (blocks.display)
        {
            rb.gravityScale = 0;
            blocks.state = false;
        }
        else if (!blocks.Genesis)
        {
            if (transform.position.x <= 0.64)
            {
                transform.position = new Vector3(blocks.x + (map.Cellsize / 2), blocks.y, transform.position.z);
            }
            if (transform.position.x >= 10.64)
            {
                transform.position = new Vector3(blocks.x - (map.Cellsize / 2), blocks.y, transform.position.z);
            }
            //if(blocks.state)
            //{
            //    transform.localScale = new Vector3(0.98f, 0.98f, 1);
            //}
            //if (!blocks.state)
            //{
            //    transform.localScale = new Vector3(1,1,1);
            //}
        }
        if (blocks.state)
        {
            transform.position = new Vector3(blocks.x, blocks.y - 0.02f, transform.position.z);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.position = new Vector3(blocks.x - (map.Cellsize / 2), blocks.y, transform.position.z);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.position = new Vector3(blocks.x + (map.Cellsize / 2), blocks.y, transform.position.z);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                rb.gravityScale = 0.2f;
            if (Input.GetKeyUp(KeyCode.DownArrow))
                rb.gravityScale = 0f;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 90 * blocks.rot);
                toggle_4_P();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.rotation = Quaternion.Euler(0, 0, -90 * blocks.rot);
                toggle_4_M();
            }
            if (Input.GetKeyDown(KeyCode.U))
                transform.position = new Vector3(blocks.x - (map.Cellsize), blocks.y, transform.position.z);
            if (Input.GetKeyDown(KeyCode.I))
                transform.position = new Vector3(blocks.x + (map.Cellsize), blocks.y, transform.position.z);

        }
        
    }
}
