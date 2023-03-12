using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.MonoBehaviours;

namespace Stands.Cards
{
    class HamonMono : CounterReversibleEffect
    {
        public GunAmmo gunAmmo;
        private Gun baseGun;
        private float maxDamageBuff = 2f;
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
            float ammoMultiplier = (int)gunAmmo.GetFieldValue("currentAmmo") / (float)gunAmmo.maxAmmo;
            //Stands.Debug($"[HamonMono] {(int)gunAmmo.GetFieldValue("currentAmmo")}/{(float)gunAmmo.maxAmmo} = {ammoMultiplier}");
            float damageBuff = maxDamageBuff * ammoMultiplier;
            float velocityBuff = maxVelocityBuff * ammoMultiplier;

            gunStatModifier.damage_mult = damageBuff;
            gunStatModifier.projectileSpeed_mult = velocityBuff;
        }
    }
}
