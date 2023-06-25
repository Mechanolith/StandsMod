using System;
using System.Collections.Generic;
using Sonigon;
using SoundImplementation;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnboundLib.Utils;
using UnboundLib;
using Object = UnityEngine.Object;

namespace Stands.Effects
{
    class SpiceGirlMono : MonoBehaviour
    {
        Block block;
        Action<GameObject, Vector3, Vector3> basicAction;
        Action<GameObject, Vector3, Vector3> blockReflectAction;

        void Start()
        {
            Stands.Debug("[Spice Girl] Spice Girl start.");
            block = GetComponent<Block>();
            basicAction = block.BlockProjectileAction;
            blockReflectAction = new Action<GameObject, Vector3, Vector3>((projectile, forward, hitPosition) => OnBlockReflect(projectile, forward, hitPosition));
            block.BlockProjectileAction = (Action<GameObject, Vector3, Vector3>)Delegate.Combine(block.BlockProjectileAction, blockReflectAction);
        }

        void OnBlockReflect(GameObject projectile, Vector3 forward, Vector3 hitPosition)
        {
            Stands.Debug("[Spice Girl] Reflect.");

            RayHitReflect reflect = projectile.GetComponent<RayHitReflect>();

            if (reflect != null)
            {
                reflect.reflects += 3;
            }
            else
            {
                ProjectileHit projectileHit = projectile.GetComponent<ProjectileHit>();
                Gun gun = projectileHit.ownWeapon.GetComponent<Gun>();
                reflect = projectile.gameObject.AddComponent<RayHitReflect>();
                reflect.reflects = 3;
                reflect.speedM = gun.speedMOnBounce;
                reflect.dmgM = gun.dmgMOnBounce;
                reflect.timeOfBounce = Time.time;
                reflect.SetFieldValue("projHit", projectileHit);
                reflect.SetFieldValue("move", projectileHit.GetComponent<MoveTransform>());

                projectile.GetComponentInChildren<ProjectileCollision>().SetFieldValue("reflect", reflect);
            }
        }

        public void Destroy()
        {
            Stands.Debug("[Spice Girl] Spice Girl destroy.");
            Destroy(this);
        }

        public void OnDestroy()
        {
            Stands.Debug("[Spice Girl] Spice Girl on destroy.");
            block.BlockProjectileAction = basicAction;
        }
    }
}
