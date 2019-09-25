using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{
	[SerializeField]
	private GameObject rockPrefab;
	private List<GameObject> rockPoolList = new List<GameObject>();

	//spawn an amount of rocks and add them to the pool
	public void SpawnRock(int _amount) 
	{
		for(int i = 0; i < _amount; i++) 
		{
			GameObject _newRock = Instantiate(rockPrefab);
			_newRock.transform.parent = transform;
			_newRock.SetActive(false);
			rockPoolList.Add(_newRock);
		}
	}

	//gets a rock from the pool
	public Transform GetRockFromPool() 
	{
		if(rockPoolList.Count <= 0) 
		{
			return null;
		}

		Transform _rock = rockPoolList[0].transform;
		_rock.gameObject.SetActive(true);
		rockPoolList.Remove(_rock.gameObject);
		return _rock;
	}

	//returns the object to the pool
	public void ReturnToPool (GameObject _rock)
	{
		rockPoolList.Add(_rock);
		_rock.SetActive(false);
	}

	//makes the list size public while keeping the list itself private
	public int RockPoolSize() {
		if (rockPoolList == null) 
		{
			return 0;
		}

		return rockPoolList.Count;
	}
}
