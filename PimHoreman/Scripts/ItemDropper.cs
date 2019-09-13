using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
	[SerializeField] private List<GameObject> items = new List<GameObject>();

	private void DropItem()
	{
		GameObject _item = Instantiate(items[Random.Range(0, items.Count)], transform.position, Quaternion.identity);
	}

	private void OnEnable()
	{
		Brick.SpawnItemEvent += DropItem;
	}

	private void OnDisable()
	{
		Brick.SpawnItemEvent -= DropItem;
	}
}