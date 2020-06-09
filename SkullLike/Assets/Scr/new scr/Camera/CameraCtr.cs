using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScr;

public class CameraCtr : MonoBehaviour
{private static CameraCtr instance;

	public void CamWait(float _time)
	{
		isWaiting = true;
		StartCoroutine(WaitForCamLock(_time));
	}

	IEnumerator WaitForCamLock(float _time)
	{
		yield return new WaitForSeconds(_time);
		isWaiting = false;
	}
	
	
	public static CameraCtr Instance
	{
		get
		{
			if (instance == null)
			{
				var obj = FindObjectOfType<CameraCtr>();
				if (obj != null)
				{
					instance = obj;
				}
				else
				{
					var newSingleton = new GameObject("main Camera").AddComponent<CameraCtr>();
					instance = newSingleton;
				}
			}
			return instance;
		}
		private set { instance = value; }
	}

	private void Awake()
	{
		var objs = FindObjectsOfType<CameraCtr>();

		if (objs.Length != 1)
		{
			Destroy(gameObject);
			return;
		}
		
	}

	public Player _user;

	public Vector3 CamPos;
	public float CamLimitLeft;
	public float CamLimitRight;
	public float CamLimitUp;
	public float CamLimitDown;
	private bool isWaiting;
	private float[] LimitPosXArr = new float[2]{-2f,24f};
	private float[] LimitPosYArr = new float[2]{0.5f,10f};

	
	void CheckCamPos()
	{
		if(transform.position.z != CamPos.z)
			transform.position = new Vector3(transform.position.x,transform.position.y,CamPos.z);
		if(transform.position.x < CamLimitLeft)
			transform.position = new Vector3(CamLimitLeft, transform.position.y, transform.position.z);
		if(transform.position.x > CamLimitRight)
			transform.position = new Vector3(CamLimitRight, transform.position.y, transform.position.z);
		if(transform.position.y > CamLimitUp)
			transform.position = new Vector3(transform.position.x, CamLimitUp, transform.position.z);
		if(transform.position.y < CamLimitDown)
			transform.position = new Vector3(transform.position.x, CamLimitDown, transform.position.z);
		
		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isWaiting)
		{
			transform.position = _user.transform.position;
		}
		CheckCamPos();

	}
}
