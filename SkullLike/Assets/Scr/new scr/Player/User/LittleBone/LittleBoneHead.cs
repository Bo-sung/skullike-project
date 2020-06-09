using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MyData;
using PlayerScr;
using LittleBone;
using UnityEngine.UI;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class LittleBoneHead : MonoBehaviour
{
	private static LittleBoneHead instance;

	public static LittleBoneHead Instance
	{
		get
		{
			if (instance == null)
			{
				var obj = FindObjectOfType<LittleBoneHead>();
				if (obj != null)
				{
					instance = obj;
				}
				else
				{
					var newSingleton = new GameObject("Head").AddComponent<LittleBoneHead>();
					instance = newSingleton;
				}
			}
			return instance;
		}
		private set { instance = value; }
	}

	private void Awake()
	{
		transform.position = new Vector3(999,999,0);
		_circleCollider2D = GetComponent<CircleCollider2D>();
		_circleCollider2D.radius = 0.075f;
		_circleCollider2D.offset = new Vector2(0,0);
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spriteRenderer.sprite = Resources.Load<Sprite>("PixelCharAnimhead");
		_spriteRenderer.drawMode = SpriteDrawMode.Sliced;
		_spriteRenderer.size = new Vector2(0.18f,0.18f);
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
		_circleCollider2D.sharedMaterial = Resources.Load<PhysicsMaterial2D>("PhysicsMaterial/Bounceable");
		DontDestroyOnLoad(gameObject);
		gameObject.SetActive(false);
		
	}

	private CircleCollider2D _circleCollider2D;
	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rigidbody2D;
	
	private Vector3 disablezone = new Vector3(9999,9999,0); 
	public Head_State state;
	public float Head_speed = 5;
	private AttackInfo Head_Damage;
	private Vector2 dirVec2;

	public LittleBone_Skill1 Skill_1;
	public LittleBone_Skill2 Skill_2;

	public void initiallize(Vector3 _position, AttackInfo _head_Damage)
	{
		Head_Damage = _head_Damage;
		if (isActiveAndEnabled)
		{
			state = Head_State.Flying;
			switch (FindObjectOfType<LittleBoneCtr>().state.dir)
			{
				case Dir.Left:
				{
					dirVec2 = Vector2.left;
				}
					break;
				case Dir.Right:
				{
					dirVec2 = Vector2.right;
				}
					break;
			}

			transform.position = _position + new Vector3(0, 0.11f, 0);
			_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
			
			StartCoroutine(WaitForDelay());
		}
		
	}
	
	void Start()
	{
		
	}
	IEnumerator WaitForDelay()
	{
		yield return new WaitForSeconds(7f);
		DisableHead();
	}

	IEnumerator DisableHead()
	{
		yield return new WaitForSeconds(0.02f);
		transform.position = disablezone;
		transform.rotation = Quaternion.Euler(0,0,0);
		gameObject.SetActive(false);
	}

	public Vector3 Return_Position()
	{
		if (state == Head_State.Flying)
			return transform.position;
		//else if (state == Head_State.Waiting)
		//{
		//	if(Physics.Raycast(transform.position, Vector3.up,0.25f)||
		//	   Physics.Raycast(transform.position, Vector3.down,0.25f)||
		//	   Physics.Raycast(transform.position, Vector3.right,0.25f)||
		//	   Physics.Raycast(transform.position, Vector3.left,0.25f))
		//	{
		//		
		//	}
		//}
		return transform.position;
	}
	
	
	
	
	public void Skill_2_Active()
	{
		
	}

	private RaycastHit _raycastHit;
	private void Update()
	{
		if (isActiveAndEnabled && state == Head_State.Flying)
		{
			if (Physics.Raycast(transform.position, dirVec2, out _raycastHit, Time.deltaTime * Head_speed))
			{
				Debug.Log("HitOnWall");
			}
			else
			{
				transform.Translate(Time.deltaTime * Head_speed * dirVec2);
			}
		}
	}

	public void OnCollisionEnter2D(Collision2D _col)
	{
		state = Head_State.Waiting;
		_rigidbody2D.constraints = RigidbodyConstraints2D.None;
		if (_col.transform.CompareTag("Player"))
		{
			StartCoroutine("DisableHead");
		}

		if (_col.transform.CompareTag("Enemy"))
		{
			_col.transform.SendMessage("Attacked", Head_Damage);
		}
		
	}
}
