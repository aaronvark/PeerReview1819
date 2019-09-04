using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bas.Interfaces
{
    public interface IDamagable<T>
    {
        void OnDeath();
        bool TakeDamage(T _damage);
    }
}
