using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private Block standardBlock;
	[SerializeField] private float spawnRadius;
	[SerializeField] private int blockCountRing;

	private List<Ring> rings;

	public float Radius => spawnRadius;
	
	private void Start() {
		rings = new List<Ring>();

		SpawnRing();
	}

	/// <summary>
	/// Spawns a new ring of blocks
	/// </summary>
	private void SpawnRing() {
		// Create a new ring and add it to the list for future reference
		rings.Add(new Ring(rings.Count, this, blockCountRing, standardBlock));
	}
	
}