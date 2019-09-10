using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour 
{
	public float CurrentPoints 
	{
		get;
		set;
	}

	[SerializeField]
	private Transform enemyPrefab, enemySpawnSpot;

	[SerializeField]
	private int enemyCount, rocksPerEnemy;

	private List<GameObject> stoneList = new List<GameObject>();
	private RockPool rockPool;

	private void Awake() 
	{
		rockPool = FindObjectOfType<RockPool>();
		FillStoneList();
		int _rocksToSpawn = (enemyCount * rocksPerEnemy) - rockPool.RockPoolSize();
		rockPool.SpawnRock(_rocksToSpawn);
	}

	//TODO: fill the list
	private void FillStoneList() 
	{

	}

	//When a stone breaks, remove it from the list and check if its the last
	public void RemoveStone(GameObject _stone) 
	{
		//stoneList.Remove(_stone);
		_stone.SetActive(false);

		if(stoneList.Count >= 0) 
		{
			LevelClear();
		}
	}
   
	//Once all stones are cleared
	private void LevelClear() 
	{
		print("Levelclear");
	}

	//Out of balls or player broke
	public void GameOver() 
	{
		print("Gameover");
	}
}