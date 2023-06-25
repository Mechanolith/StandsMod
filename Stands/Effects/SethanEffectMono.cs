using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace Stands.Effects
{
    class SethanEffectMono : ReversibleEffect
    {
        float healthReductionPerTick = 2.5f;

        public void Tick(float _maxHealth)
        {
            ClearModifiers();

            Stands.Debug($"[Sethan] Ticking {_maxHealth} vs {healthReductionPerTick}");

            if (_maxHealth > healthReductionPerTick * 2f) 
            {
                characterDataModifier.maxHealth_add -= healthReductionPerTick;
            }

            ApplyModifiers();
        }


        public void Reset()
        {
            ClearModifiers();
            characterDataModifier.maxHealth_add = 0f;
        }
    }
}
