using UnityEngine;
using ModdingUtils.MonoBehaviours;

namespace Stands.Effects
{
    class GratefulDeadColorMono : ReversibleColorEffect
    {
        Color effectColor = Color.gray;
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
