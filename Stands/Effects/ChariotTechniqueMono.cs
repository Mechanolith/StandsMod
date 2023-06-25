using System;
using System.Collections.Generic;
using Sonigon;
using SoundImplementation;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnboundLib.Utils;
using Object = UnityEngine.Object;

namespace Stands.Effects
{
    class ChariotTechniqueMono : MonoBehaviour
	{
		public int Copies = 1;

		CharacterData data;
		Player player;
		Block block;
		Gun gun;

		Action<BlockTrigger.BlockTriggerType> applyTechnique;
		Action<BlockTrigger.BlockTriggerType> basic;
		Action<GameObject> shootAction;

		Transform particleTransform;

		SoundEvent soundSpawn;
		SoundEvent soundShoot;

		ParticleSystem[] parts;

		ObjectsToSpawn screenEdgeToSpawn;
		ObjectsToSpawn targetBounceToSpawn;

		bool active;
		bool alreadyActivated;

		float originalLifetime;

		void Start()
		{
			data = GetComponentInParent<CharacterData>();
			player = GetComponent<Player>();
			block = GetComponent<Block>();
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			soundSpawn = addObjectToPlayer.GetComponent<Empower>().soundEmpowerSpawn;
			soundShoot = addObjectToPlayer.GetComponent<Empower>().addObjectToBullet.GetComponent<SoundUnityEventPlayer>().soundStart;
			GameObject particleObject = addObjectToPlayer.transform.GetChild(0).gameObject;
			particleTransform = Instantiate(particleObject, transform).transform;
			parts = GetComponentsInChildren<ParticleSystem>();
			gun = data.weaponHandler.gun;
			basic = block.FirstBlockActionThatDelaysOthers;
			shootAction = gun.ShootPojectileAction;
			applyTechnique = new Action<BlockTrigger.BlockTriggerType>(GetDoBlockAction(player, block, data).Invoke);
			block.FirstBlockActionThatDelaysOthers = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(block.FirstBlockActionThatDelaysOthers, applyTechnique);
			block.delayOtherActions = true;
			gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(gun.ShootPojectileAction, new Action<GameObject>(Attack));
			active = false;

			// get the screenEdge (with screenEdgeBounce component) from the TargetBounce card
			List<CardInfo> activecards = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null)).ToList();
			List<CardInfo> inactivecards = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
			List<CardInfo> allcards = activecards.Concat(inactivecards).ToList();

			CardInfo targetBounceCard = allcards.Where(card => card.gameObject.name == "TargetBounce").ToList()[0];
			Gun targetBounceGun = targetBounceCard.GetComponent<Gun>();
			screenEdgeToSpawn = (new List<ObjectsToSpawn>(targetBounceGun.objectsToSpawn)).Where(objectToSpawn => objectToSpawn.AddToProjectile.GetComponent<ScreenEdgeBounce>() != null).ToList()[0];
			targetBounceToSpawn = (new List<ObjectsToSpawn>(targetBounceGun.objectsToSpawn)).Where(objectToSpawn => objectToSpawn.AddToProjectile.GetComponent<BounceEffectRetarget>() != null).ToList()[0];
		}

		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
        {
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				if(trigger != BlockTrigger.BlockTriggerType.None && !active)
                {
					originalLifetime = gun.destroyBulletAfter;

					gun.reflects += Copies;
					gun.destroyBulletAfter = 0;

					active = true;
                }
			};
        }

		void Attack(GameObject projectile)
        {
            if (active) 
			{
				SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), base.transform);
				AddBehvaiourToProjectile(projectile, screenEdgeToSpawn);
				AddBehvaiourToProjectile(projectile, targetBounceToSpawn);
				
				gun.reflects -= Copies;
				gun.destroyBulletAfter = originalLifetime;

				active = false;
			}
        }

		void AddBehvaiourToProjectile(GameObject projectile, ObjectsToSpawn spawn)
        {
			ProjectileHit component = projectile.GetComponent<ProjectileHit>();
			if (spawn.AddToProjectile && (!spawn.AddToProjectile.gameObject.GetComponent<StopRecursion>() || !gun.isProjectileGun))
			{
				GameObject gameObject = Object.Instantiate<GameObject>(spawn.AddToProjectile, component.transform.position, component.transform.rotation, component.transform);
				gameObject.transform.localScale *= 1f * (1f - spawn.scaleFromDamage) + component.damage / 55f * spawn.scaleFromDamage;
				if (spawn.scaleStacks)
				{
					gameObject.transform.localScale *= 1f + (float)spawn.stacks * spawn.scaleStackM;
				}
				if (spawn.removeScriptsFromProjectileObject)
				{
					MonoBehaviour[] componentsInChildren = gameObject.GetComponentsInChildren<MonoBehaviour>();
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						if (componentsInChildren[j].GetType().ToString() != "SoundImplementation.SoundUnityEventPlayer")
						{
							Destroy(componentsInChildren[j]);
						}
					}
				}
			}
		}

		public void Destroy()
		{
			Destroy(this);
		}

		public void OnDestroy()
		{
			block.BlockAction = basic;
			gun.ShootPojectileAction = shootAction;
			Destroy(soundShoot);
			Destroy(soundSpawn);
			active = false;
		}

		public void Update()
		{
			if (active)
			{
				Transform transform = data.weaponHandler.gun.transform;
				particleTransform.position = transform.position;
				particleTransform.rotation = transform.rotation;
				ParticleSystem[] array = parts;
				if (!alreadyActivated)
				{
					SoundManager.Instance.PlayAtPosition(soundSpawn, SoundManager.Instance.GetTransform(), base.transform);
					array = parts;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Play();
					}
					alreadyActivated = true;
					return;
				}
			}
			else if (alreadyActivated)
			{
				ParticleSystem[] array2 = parts;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].Stop();
				}
				alreadyActivated = false;
			}
		}
	}
}
