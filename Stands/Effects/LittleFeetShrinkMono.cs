using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace Stands.Effects
{
    class LittleFeetShrinkMono : ReversibleEffect
    {
        public void Shrink(float _baseDamage)
        {
            //Convert 10% of damage into a percent reduction in size.
            float sizeMultiplier = Mathf.Clamp(1f - (_baseDamage * 0.001f), 0.05f, 1f);
            
            ClearModifiers();
            characterStatModifiersModifier.sizeMultiplier_mult *= sizeMultiplier;
            characterDataModifier.maxHealth_mult *= sizeMultiplier;
            ApplyModifiers();
        }
    }
}
