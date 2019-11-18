using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public abstract class ManagerBase : MonoBehaviour
    {
        public abstract void Run(MonoBehaviour reference);
    }
}
