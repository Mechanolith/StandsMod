using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace Stands.Effects
{
    class YellowTempereanceEffectMono : ReversibleEffect
    {
        float minHealth = 5f;

        public void OnHit(float _damage, float _maxHealth)
        {
            ClearModifiers();
            float maxHealthDamage = _damage;

            if (_maxHealth <= (maxHealthDamage + minHealth)) 
            {
                maxHealthDamage += _maxHealth - (maxHealthDamage + minHealth);
            }

            characterDataModifier.maxHealth_add -= maxHealthDamage;
            ApplyModifiers();
        }
    }
}
