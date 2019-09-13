using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Breakin.Pooling
{
    public class MultiPrefabPool<T> where T : MonoBehaviour, IPoolable
    {
        private Dictionary<T, List<T>> objectLists;
        private Transform root;

        public MultiPrefabPool(int capacity, Transform root, params T[] prefabs)
        {
            this.root = root;

            objectLists = new Dictionary<T, List<T>>(prefabs.Length);

            // Initialize a pool list (dictionary entry) for each prefab
            foreach (T _t in prefabs)
            {
                objectLists.Add(_t, new List<T>(capacity));
            }

            Populate();
        }

        /// <summary>
        /// Gets an object of a specified prefab
        /// </summary>
        /// <param name="prefabKey"></param>
        /// <returns></returns>
        public T GetBlock(T prefabKey)
        {
            // Try to find an inactive object in the pool. If no object was found, create an extra one.
            T _obj = FindInactive(prefabKey);
            if (!_obj)
            {
                _obj = Spawn(prefabKey);
                Debug.Log("Pool expansion for prefab " + prefabKey.name + " to size " + objectLists[prefabKey].Count);
            }

            _obj.IsActive = true;

            return _obj;
        }

        /// <summary>
        /// Fills the pools for each prefab with inactive instances of those prefabs up to the base capacity set at
        /// construction time
        /// </summary>
        private void Populate()
        {
            foreach (var _entry in objectLists)
            {
                for (int i = 0; i < _entry.Value.Capacity; i++)
                {
                    Spawn(_entry.Key);
                }
            }
        }

        /// <summary>
        /// Finds an object that is currently not active and returns it. Returns null when there was no active object
        /// found or when the provided prefab does not exist in the dictionary.
        /// </summary>
        /// <returns>An inactive object or null when none was found.</returns>
        private T FindInactive(T prefabKey)
        {
            return objectLists[prefabKey]?.FirstOrDefault(obj => !obj.IsActive);
        }

        /// <summary>
        /// Adds an (inactive) block to the object pool.
        /// </summary>
        /// <param name="prefab">The specific prefab that needs to be spawned</param>
        /// <returns>The block that was just spawned</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private T Spawn(T prefab)
        {
            T _obj = Object.Instantiate(prefab, root);
            _obj.IsActive = false;

            if (!objectLists.ContainsKey(prefab))
            {
                throw new InvalidOperationException("Can't spawn object when there is no pool for its prefab");
            }

            objectLists[prefab].Add(_obj);
            return _obj;
        }
    }
}