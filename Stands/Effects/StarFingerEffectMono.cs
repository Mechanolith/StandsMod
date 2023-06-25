using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace Stands.Effects
{
    class StarFingerEffectMono : ReversibleEffect
    {
        public void Init()
        {
            applyImmediately = false;
            gunStatModifier.projectileSpeed_mult *= 5f;
            gunStatModifier.spread_mult = 0f;
            gunStatModifier.projectileSize_mult *= 0.25f;
            gunStatModifier.reflects_mult = 0;
            gunStatModifier.gravity_mult = 0;
            gunStatModifier.projectileColor = new Color(1f, 0f, 1f, gun.projectileColor.a);
        }
    }
}
