using Stands.Effects;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;


namespace Stands.Cards
{
    class Sethan : CustomCard
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

            //Give everyone except the owner the script.
            foreach (var activePlayer in PlayerManager.instance.players)
            {
                if (activePlayer != player)
                {
                    SethanMono mono = ExtensionMethods.GetOrAddComponent<SethanMono>(activePlayer.gameObject, false);
                    mono.SetSource(player);
                }
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            Stands.Debug($"[Card] {GetTitle()} has been removed from player {player.playerID}.");
        }

        protected override string GetTitle()
        {
            return "Sethan";
        }
        protected override string GetDescription()
        {
            return "Enemies around you lose Max Health over time. Lasts for the round.";
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
                    stat = "Max Health Drain",
                    amount = "5 Health per second you goddamn nerd",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
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

