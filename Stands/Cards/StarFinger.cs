using Stands.Utility;
using UnboundLib.Cards;
using UnityEngine;
using Stands.Effects;
using UnboundLib;


namespace Stands.Cards
{
    class StarFinger : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            Stands.Debug($"[{Stands.ModInitials}][Card] {GetTitle()} has been setup.");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Stands.Debug($"[{Stands.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");

            ExtensionMethods.GetOrAddComponent<StarFingerMono>(player.gameObject, false);
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Stands.Debug($"[{Stands.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");

            StarFingerMono cardMono = player.gameObject.GetComponent<StarFingerMono>();

            if (cardMono != null)
            {
                --cardMono.Copies;

                bool lastCard = CardCount.Amount(player, "Star Finger") == 1;

                if (lastCard)
                {
                    Destroy(player.gameObject.GetComponent<StarFingerMono>());
                    Destroy(player.gameObject.GetComponent<StarFingerEffectMono>());
                }
            }
        }

        protected override string GetTitle()
        {
            return "Star Finger";
        }
        protected override string GetDescription()
        {
            return "Your next shot after blocking has:";
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
                    stat = "Projectile Speed",
                    amount = "+400%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Spread",
                    amount = "No",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Gravity",
                    amount = "No",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Projectile Size",
                    amount = "-75%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Bounces",
                    amount = "No",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return Stands.ModInitials;
        }
    }
}

