using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;
using UnboundLib;

namespace Stands.Effects
{
    class LittleFeetHitMono : RayHitEffect
    {
        public override HasToReturn DoHitEffect(HitInfo hit)
        {
            if (!hit.transform || hit.transform.GetComponentInParent<Player>() == null) 
            {
                return HasToReturn.canContinue;
            }

            LittleFeetShrinkMono shrinkMono = ExtensionMethods.GetOrAddComponent<LittleFeetShrinkMono>(hit.transform.gameObject, false);
            ProjectileHit projectileComponent = GetComponentInParent<ProjectileHit>();
            shrinkMono.Shrink(projectileComponent.damage);

            return HasToReturn.canContinue;
        }

        public void Destroy()
        {
            Destroy(this);
        }
    }
}
