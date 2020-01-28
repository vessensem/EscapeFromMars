using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Damage
{
    public interface IDamagable
    {
        void ApplyDamage(float damage);
        void Die();
    }
}