using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rock : MonoBehaviour
{
	public RockPool rockPool;

	private void OnTriggerEnter(Collider _other) 
	{
		if(_other.tag == "Player") 
		{
			_other.gameObject.SetActive(false);
			_other.GetComponentInParent<Player>().Health();
		}

		rockPool.ReturnToPool(gameObject);
	}
}
