using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.MonoBehaviours;

namespace Stands.Effects
{
    class HamonMono : CounterReversibleEffect
    {
        public GunAmmo GunAmmo;
        public int Copies = 1;

        private float maxDamageBuff = 1.5f;
        private float maxVelocityBuff = 2f;

        public override void OnApply()
        {
            //throw new NotImplementedException();
        }

        public override void Reset()
        {
            //throw new NotImplementedException();
        }

        public override CounterStatus UpdateCounter()
        {
            return CounterStatus.Apply;
            //throw new NotImplementedException();
        }

        public override void UpdateEffects()
        {
            float ammoMultiplier = (int)GunAmmo.GetFieldValue("currentAmmo") / (float)GunAmmo.maxAmmo;
            //Stands.Debug($"[HamonMono] {(int)gunAmmo.GetFieldValue("currentAmmo")}/{(float)gunAmmo.maxAmmo} = {ammoMultiplier}");
            float damageBuff = maxDamageBuff * ammoMultiplier * Copies;
            float velocityBuff = maxVelocityBuff * ammoMultiplier * Copies;

            gunStatModifier.damage_mult = damageBuff;
            gunStatModifier.projectileSpeed_mult = velocityBuff;
        }
    }
}
