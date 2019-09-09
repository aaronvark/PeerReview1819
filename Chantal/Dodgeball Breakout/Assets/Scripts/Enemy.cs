using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed, minThrowDelay, maxThrowDelay;
	[SerializeField]
	private Transform rockPool, rockSpawn, player;
	[SerializeField]
	private Transform[] movePoints;

    void Start()
    {
		StartCoroutine(Throw());
    }

	void Movement() 
	{

	}

	Transform GetRockFromPool() 
	{
		Transform _rock = rockPool.GetChild(0);
		_rock.parent = null;
		_rock.localPosition = rockSpawn.localPosition;
		return _rock;
	}

	IEnumerator Throw() 
	{
		float _throwDelay = Random.Range(minThrowDelay, maxThrowDelay);
		yield return new WaitForSeconds(_throwDelay);


		Vector3 _destination = player.position;
		float time = 0;
		float delay = 2;

		while(time < delay) {
			GetRockFromPool().position = Vector3.Lerp(rockSpawn.localPosition, _destination, (time / delay));
			time += Time.deltaTime;

			// Yield here
			yield return null;
		}
	}
    
}
