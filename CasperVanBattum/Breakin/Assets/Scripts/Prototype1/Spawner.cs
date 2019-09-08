using System.Collections.Generic;
using UnityEngine;

namespace Prototype1
{
	public class Spawner : MonoBehaviour
	{
		public float Radius => spawnRadius;

		[SerializeField] private Block standardBlock;
		[SerializeField] private float spawnRadius;
		[SerializeField] private int blockCountRing;

		private List<Ring> rings;

		private void Start()
		{
			rings = new List<Ring>();

			SpawnRing();
		}

		/// <summary>
		/// Spawns a new ring of blocks
		/// </summary>
		private void SpawnRing()
		{
			// Create a new ring and add it to the list for future reference
			rings.Add(new Ring(rings.Count, this, blockCountRing, standardBlock));
		}
	}
}