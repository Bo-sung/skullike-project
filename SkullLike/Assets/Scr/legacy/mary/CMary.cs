using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMary : MonoBehaviour
{
    public Rigidbody2D rb;
    Renderer rd;
    Animator ani;
    public CircleCollider2D rc;
    float speed = 3;    
    public static bool GND = false;
    public float mary_x = 0;
    public float mary_y = 0;
    public int mary_state = 0;
    // Use this for initialization
    void Start ()
    {
        
        ani = GetComponent<Animator>();
        ani.SetInteger("state", 5);
        rb = GetComponent<Rigidbody2D>();
        rc = GetComponent<CircleCollider2D>();
        rd = GetComponent<Renderer>();
    }
    void ANI(int _state)
    {        
        ani.SetInteger("state", _state);
        mary_state = _state;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GND)
            {
                ani.SetInteger("state", 8);
                GND = false;
                rb.velocity = new Vector3(0, 4, 0);
            }
            //transform.Translate(Vector3.up * speed * Time.deltaTime);            
            //rb.AddFoe(Vector2.up*500);           
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;

            if (ani.GetInteger("state") != 2)
            {
                if (GND)
                {
                    ANI(6);
                }
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (ani.GetInteger("state") != 2)
            {
                if (GND)
                {
                    ANI(5);
                }
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            if (ani.GetInteger("state") != 2)
            {
                if (GND)
                {
                    ANI(6);
                }
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (ani.GetInteger("state") != 2)
            {
                if (GND)
                {
                    ANI(5);
                }
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (GND)
            {
                ANI(7);
                mary_x = rc.offset.x;
                mary_y = rc.offset.y;
                transform.SetPositionAndRotation(transform.position, Quaternion.EulerAngles(0, 0, -25));                
            }
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (GND)
            {
                ANI(2);
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            // transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (GND)
            {
                ANI(5);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            GameObject Bullet = (GameObject)Instantiate(GameObject.FindWithTag("Bullet"));
            Bullet.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        }
    

        if (Input.GetKey(KeyCode.H))
        {
            //gameObject.SetActive(false);
            //gameObject.hideFlags = HideFlags.HideAndDontSave;
            //game.SetActive(false);
            //rd.enabled = false;
            ANI(7);
        }

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Block"))
        {           
            GND = true;         
        }
        else
        {
            GND = false;
            print(GND);
        }
    }
    void CollisionStay(Collision2D coll)
    {
        Debug.Log("cnd");
    }     
}
