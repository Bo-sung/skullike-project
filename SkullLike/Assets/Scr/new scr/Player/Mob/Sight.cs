using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScr
{
	public class Sight : MonoBehaviour
	{
		private bool m_IsPlayerOnSight;
		private Vector3 m_PlayerPos;

		public Vector3 GetPlayerPos()
		{
			if (IsPlayerOnSight)
			{
				return m_PlayerPos;
			}

			return new Vector3();
		}


		public bool IsPlayerOnSight
		{
			get { return m_IsPlayerOnSight; }
			set { m_IsPlayerOnSight = value; }
		}

		private bool m_IsColOnWall;

		public bool IsColOnWall
		{
			get { return m_IsColOnWall; }
			set { m_IsColOnWall = value; }
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.transform.CompareTag("Player"))
			{
				IsPlayerOnSight = true;
				m_PlayerPos = other.transform.position;

			}

			if (other.transform.CompareTag("GROUND"))
				IsColOnWall = true;
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.transform.CompareTag("GROUND"))
				IsColOnWall = false;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.transform.CompareTag("Player"))
			{
				IsPlayerOnSight = true;
				m_PlayerPos = other.transform.position;
			}
		}
	}
	

}
