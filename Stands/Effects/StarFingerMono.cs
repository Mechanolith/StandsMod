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
using UnboundLib;

namespace Stands.Effects
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

		StarFingerEffectMono effect;


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

			effect = ExtensionMethods.GetOrAddComponent<StarFingerEffectMono>(gameObject, false);
			effect.Init();
		}

		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
        {
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				if(trigger != BlockTrigger.BlockTriggerType.None && !active)
                {
					effect.ApplyModifiers();

					active = true;
                }
			};
        }

		void Attack(GameObject projectile)
        {
            if (active) 
			{
				SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), base.transform);

				effect.ClearModifiers();

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
