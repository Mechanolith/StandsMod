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
            characterStatModifiersModifier.movementSpeed_mult *= 0.975f;
            characterStatModifiersModifier.movementSpeed_mult = Mathf.Clamp(characterStatModifiersModifier.movementSpeed_mult, 0.2f, 1f);
            ApplyModifiers();
        }

        public void Reset()
        {
            ClearModifiers();
            characterStatModifiersModifier.movementSpeed_mult = 1f;
        }
    }
}
