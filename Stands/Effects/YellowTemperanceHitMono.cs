using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;
using UnboundLib;

namespace Stands.Effects
{
    class YellowTemperanceHitMono : RayHitEffect
    {
        public override HasToReturn DoHitEffect(HitInfo hit)
        {
            if (!hit.transform)
            {
                return HasToReturn.canContinue;
            }

            Player hitPlayer = hit.transform.GetComponentInParent<Player>();

            if(hitPlayer == null)
            {
                return HasToReturn.canContinue;
            }

            YellowTempereanceEffectMono effectMono = ExtensionMethods.GetOrAddComponent<YellowTempereanceEffectMono>(hit.transform.gameObject, false);
            ProjectileHit projectileComponent = GetComponentInParent<ProjectileHit>();
            effectMono.OnHit(projectileComponent.damage, hitPlayer.data.maxHealth);
            hitPlayer.data.healthHandler.Heal(projectileComponent.damage);

            return HasToReturn.canContinue;
        }

        public void Destroy()
        {
            Destroy(this);
        }
    }
}
