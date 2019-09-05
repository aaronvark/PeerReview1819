using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bas.Interfaces
{
    public interface IPooler
    {
        GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation);
    }
}
