using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bas.Interfaces
{
    public delegate void OnDeath();
    public interface IDamagable<T>
    {
        event OnDeath OnDeathHandler;
        bool TakeDamage(T _damage);
    }
}
