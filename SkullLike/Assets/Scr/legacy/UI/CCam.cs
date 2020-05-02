using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCam : MonoBehaviour {

    
    // Use this for initialization
    public struct CAMERA
    {
        public float x;
        public float y;
    }
    CAMERA cam;
    public void SetCamX(float _x) { cam.x = _x; }
    public void SetCamY(float _y) { cam.y = _y; }

    public float GetCamX() { return cam.x; }
    public float GetCamY() { return cam.y; }

    public void CamRefresh()
    {
        cam.x = transform.position.x;
        cam.y = transform.position.y;
    }
    void Awake()
    {
        CamRefresh();
    }
    void Start ()
    {
        CamRefresh();
    }
    public void CamMove(float _y)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + _y, transform.position.z);
    }
        
	
	// Update is called once per frame
	void Update ()
    {
        CamRefresh();
    }
    private void LateUpdate()
    {
        
    }
}

