using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace Stands.Effects
{
    class YellowTempereanceEffectMono : ReversibleEffect
    {
        public void OnHit(float _damage)
        {
            ClearModifiers();
            characterDataModifier.maxHealth_add -= _damage;
            ApplyModifiers();
        }
    }
}
