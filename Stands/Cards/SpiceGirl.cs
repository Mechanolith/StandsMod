using UnboundLib.Cards;
using UnityEngine;
using Stands.Effects;


namespace Stands.Cards
{
    class SpiceGirl : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            Stands.Debug($"[Card] {GetTitle()} has been setup.");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            Stands.Debug($"[Card] {GetTitle()} has been added to player {player.playerID}.");

            player.gameObject.AddComponent<SpiceGirlMono>();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            Stands.Debug($"[Card] {GetTitle()} has been removed from player {player.playerID}.");

            Destroy(player.gameObject.GetComponent<SpiceGirlMono>());
        }

        protected override string GetTitle()
        {
            return "Spice Girl";
        }
        protected override string GetDescription()
        {
            return "Blocked bullets gain extra bounces.";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bounces",
                    amount = "+3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return Stands.ModInitials;
        }
    }
}

