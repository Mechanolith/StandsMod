using UnboundLib.Cards;
using UnityEngine;
using Stands.Utility;
using Stands.Effects;

namespace Stands.Cards
{
    class Hamon : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            Stands.Debug($"[Card] {GetTitle()} has been setup.");
            gun.reloadTime = 1.5f;
            gun.projectileColor = Color.yellow;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            Stands.Debug($"[Card] {GetTitle()} has been added to player {player.playerID}.");
            HamonMono cardMono = player.gameObject.GetComponent<HamonMono>();

            if (cardMono == null) 
            {
                HamonMono hamon = player.gameObject.AddComponent<HamonMono>();
                hamon.GunAmmo = gunAmmo;
            }
            else
            {
                ++cardMono.Copies;
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            Stands.Debug($"[Card] {GetTitle()} has been removed from player {player.playerID}.");

            HamonMono cardMono = player.gameObject.GetComponent<HamonMono>();

            if (cardMono != null) 
            {
                --cardMono.Copies;

                bool lastCard = CardCount.Amount(player, "Hamon") == 1;
                if (lastCard)
                {
                    Destroy(player.gameObject.GetComponent<HamonMono>());
                }
            }
        }

        protected override string GetTitle()
        {
            return "Hamon";
        }
        protected override string GetDescription()
        {
            return "Imbue your bullets with the power of the sun. More powerful the more full your ammo is.";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+100%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectile Speed",
                    amount = "+100%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }
        public override string GetModName()
        {
            return Stands.ModInitials;
        }
    }
}

