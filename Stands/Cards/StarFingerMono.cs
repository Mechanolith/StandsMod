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

namespace Stands.Cards
{
    class StarFingerMono : MonoBehaviour
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

		bool active;
		bool alreadyActivated;

		float originalSpeed;
		float originalSpread;
		float originalSize;
		int originalReflects;
		float originalGravity;
		Color originalColor;


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
		}

		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
        {
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				if(trigger != BlockTrigger.BlockTriggerType.None && !active)
                {
					originalSpeed = gun.projectileSpeed;
					originalSpread = gun.spread;
					originalSize = gun.projectileSize;
					originalReflects = gun.reflects;
					originalGravity = gun.gravity;
					originalColor = gun.projectileColor;

					gun.projectileSpeed *= 5f * Copies;
					gun.spread = 0;
					gun.projectileSize *= 0.25f * Copies;
					gun.reflects = 0;
					gun.gravity = 0;
					gun.projectileColor = new Color(1f, 0f, 1f, gun.projectileColor.a);

					active = true;
                }
			};
        }

		void Attack(GameObject projectile)
        {
            if (active) 
			{
				SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), base.transform);

				gun.projectileSpeed = originalSpeed;
				gun.spread = originalSpread;
				gun.projectileSize = originalSize;
				gun.reflects = originalReflects;
				gun.gravity = originalGravity;
				gun.projectileColor = originalColor;

				active = false;
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
