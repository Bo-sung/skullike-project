using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlock_child : MonoBehaviour
{

    CBlock block;
    Renderer rd;
    PolygonCollider2D pc;

    private struct Child_Blocks
    {
        public bool state;  //상태
    }
    Child_Blocks blocks;
    // Use this for initialization
    public bool GetBlocks_STATE() { return blocks.state; }

    public void SetBlocks_STATE(bool _state) { blocks.state = _state; }

    void Start()
    {
        block = GetComponent<CBlock>();
        pc = GetComponent<PolygonCollider2D>();

    }
    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("wallblock1"))//바닥과 충돌
        {
            if (blocks.state)
            {

            }
            blocks.state = false;
            pc.isTrigger = false;

        }
        if (coll.gameObject.tag.Equals("tetris_block"))//블럭과 충돌
        {
            if (blocks.state)
            {
            }
            blocks.state = false;
            pc.isTrigger = false;
        }

    }
}

