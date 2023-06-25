using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace Stands.Effects
{
    class GratefulDeadEffectMono : ReversibleEffect
    {
        public void Tick()
        {
            ClearModifiers();
            //characterStatModifiersModifier.attackSpeedMultiplier_mult *= 0.95f;
            characterStatModifiersModifier.movementSpeed_mult *= 0.95f;
            ApplyModifiers();
        }

        public void Reset()
        {
            ClearModifiers();
            characterStatModifiersModifier.movementSpeed_mult = 1f;
        }
    }
}
