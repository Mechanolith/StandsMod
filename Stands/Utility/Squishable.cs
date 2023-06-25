using System;
using System.Linq;
using HarmonyLib;
using PCE.Extensions;
using UnityEngine;

namespace Stands.Utility
{
    public class Squishable : MonoBehaviour
    {
		private void Awake()
		{
			this.playerToModify = base.gameObject.GetComponent<Player>();
			this.charStatsToModify = base.gameObject.GetComponent<CharacterStatModifiers>();
		}

		private void Update()
		{
			if (PlayerStatus.PlayerAliveAndSimulated(this.playerToModify) && Time.time >= this.timeOfLastSquish + this.minTimeBetweenSquishes)
			{
				foreach (Player player2 in Enumerable.ToList<Player>(Enumerable.Where<Player>(PlayerManager.instance.players, (Player player) => PlayerStatus.PlayerAliveAndSimulated(player) && player.teamID != this.playerToModify.teamID)))
				{
					float num = (float)Traverse.Create(this.playerToModify.data.playerVel).Field("mass").GetValue();
					if ((float)Traverse.Create(player2.data.playerVel).Field("mass").GetValue() >= this.minMassFactor * num)
					{
						Vector2 to = player2.transform.position - this.playerToModify.transform.position;
						if (to.magnitude <= this.range && Vector2.Angle(Vector2.up, to) <= Math.Abs(this.angleThreshold / 2f))
						{
							float num2 = this.playerToModify.data.maxHealth * 2f;
							this.playerToModify.data.healthHandler.TakeDamage(new Vector2(0f, -1f * num2), this.playerToModify.transform.position, Color.red, null, player2, true, false);
							this.ResetTimer();
							break;
						}
					}
				}
			}
		}

		public void OnDestroy()
		{
		}

		public void ResetTimer()
		{
			this.timeOfLastSquish = Time.time;
		}

		public void Destroy()
		{
			Destroy(this);
		}

		public void SetDamagePerc(float perc)
		{
			this.damagePerc = perc;
		}

		public void IncreaseDamagePerc(float inc)
		{
			this.damagePerc += inc;
			this.damagePerc = Math.Min(this.damagePerc, 0.75f);
		}

		private Player playerToModify;
		private CharacterStatModifiers charStatsToModify;
		private float damagePerc;
		private float timeOfLastSquish = -1f;
		private readonly float range = 1.5f;
		private readonly float angleThreshold = 30f;
		private readonly float minTimeBetweenSquishes = 0.5f;
		private readonly float minMassFactor = 1.1f;
	}
}
