using UnityEngine;
using ModdingUtils.MonoBehaviours;

namespace Stands.Effects
{
    //Todo: Genericise this (along with Grateful Dead Color Mono)
    class SethanColorMono : ReversibleColorEffect
    {
        Color effectColor = new Color(0.5f, 0f, 0.5f);
        bool isActive = false;

        public void AddColor()
        {
            if (!isActive)
            {
                SetColor(effectColor);
                ApplyColor();
                isActive = true;
            }
        }

        public void RemoveColor()
        {
            if (isActive)
            {
                RemoveColorInternal();
            }
        }

        void RemoveColorInternal()
        {
            SetColorMax(GetOriginalColorMax());
            SetColorMin(GetOriginalColorMin());
            ApplyColor();
            isActive = false;
        }

        public void Reset()
        {
            RemoveColorInternal();
        }
    }
}
