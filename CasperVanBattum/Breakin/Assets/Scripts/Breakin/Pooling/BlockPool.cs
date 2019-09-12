using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Breakin.Pooling
{
    public class BlockPool
    {
        private Dictionary<Block, List<Block>> blockLists;
        private Transform root;

        public BlockPool(int capacity, Transform root, params Block[] prefabs)
        {
            this.root = root;

            blockLists = new Dictionary<Block, List<Block>>(prefabs.Length);

            // Initialize a pool list for each prefab
            foreach (Block _t in prefabs)
            {
                blockLists.Add(_t, new List<Block>(capacity));
            }

            Populate();
        }

        /// <summary>
        /// Gets a block of a specified prefab
        /// </summary>
        /// <param name="prefabKey"></param>
        /// <returns></returns>
        public Block GetBlock(Block prefabKey)
        {
            // Try to find an inactive object in the pool. If no object was found, create an extra one.
            Block _b = FindInactive(prefabKey);
            if (!_b)
            {
                _b = Spawn(prefabKey);
                Debug.Log("Pool expansion for prefab " + prefabKey.name + " to size " + blockLists[prefabKey].Count);
            }

            _b.IsActive = true;

            return _b;
        }

        /// <summary>
        /// Fills the pools for each prefab with inactive instances of those prefabs up to the base capacity set at
        /// construction time
        /// </summary>
        private void Populate()
        {
            foreach (var _entry in blockLists)
            {
                for (int i = 0; i < _entry.Value.Capacity; i++)
                {
                    Spawn(_entry.Key);
                }
            }
        }

        /// <summary>
        /// Finds a block that is currently not active and returns it. Returns null when there was no active block found
        /// or when the provided prefab does not exist in the dictionary.
        /// </summary>
        /// <returns>An inactive block or null when none was found.</returns>
        private Block FindInactive(Block prefabKey)
        {
            return blockLists[prefabKey]?.FirstOrDefault(b => !b.IsActive);
        }

        /// <summary>
        /// Adds an (inactive) block to the object pool.
        /// </summary>
        /// <param name="prefab">The specific prefab that needs to be spawned</param>
        /// <returns>The block that was just spawned</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private Block Spawn(Block prefab)
        {
            Block _b = Object.Instantiate(prefab, root);
            _b.IsActive = false;

            if (!blockLists.ContainsKey(prefab))
            {
                throw new InvalidOperationException("Can't spawn object when there is no pool for its prefab");
            }

            blockLists[prefab].Add(_b);
            return _b;
        }
    }
}