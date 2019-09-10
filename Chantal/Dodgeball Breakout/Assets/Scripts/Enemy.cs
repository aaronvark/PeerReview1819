using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed, minThrowDelay, maxThrowDelay, throwTime;
	[SerializeField]
	private Transform rockSpawn, player;
	[SerializeField]
	private Transform[] movePoints;
	private RockPool rockPool;


	private void Start()
    {
		rockPool = FindObjectOfType<RockPool>();
		StartCoroutine(Throw());
    }

	private void Movement() 
	{
		//allow enemies to move?
	}

	//throws a rock towards the player
	private IEnumerator Throw() 
	{
		while (true) 
		{
			float _throwDelay = Random.Range(minThrowDelay, maxThrowDelay);
			yield return new WaitForSeconds(_throwDelay);

			Transform _rock = rockPool.GetRockFromPool();

			if(_rock != null) 
			{
				_rock.GetComponent<Rock>().rockPool = rockPool;
				_rock.localPosition = rockSpawn.localPosition;
				Vector3 _destination = player.position;
				float time = 0;
			
				while(time < throwTime) 
				{
					_rock.position = Vector3.Lerp(rockSpawn.localPosition, _destination, (time / throwTime));
					time += Time.deltaTime;

					yield return null;
				}
			}
		}
	} 
}
