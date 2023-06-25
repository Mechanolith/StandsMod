using UnboundLib.Cards;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnboundLib.Utils;
using Stands.Utility;
using Stands.Effects;

namespace Stands.Cards
{
    class ChariotTechnique : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            Stands.Debug($"[{Stands.ModInitials}][Card] {GetTitle()} has been setup.");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            Stands.Debug($"[{Stands.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            ChariotTechniqueMono cardMono = player.gameObject.GetComponent<ChariotTechniqueMono>();
            if (cardMono == null)
            {
                player.gameObject.AddComponent<ChariotTechniqueMono>();
            }
            else
            {
                ++cardMono.Copies;
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            Stands.Debug($"[{Stands.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            ChariotTechniqueMono cardMono = player.gameObject.GetComponent<ChariotTechniqueMono>();

            if (cardMono != null)
            {
                --cardMono.Copies;

                bool lastCard = CardCount.Amount(player, "Chariot Technique") == 1;

                if (lastCard)
                {
                    Destroy(player.gameObject.GetComponent<ChariotTechniqueMono>());
                }
            }
        }

        protected override string GetTitle()
        {
            return "Chariot Technique";
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
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Target Bounce",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Lifetime",
                    amount = "Unlimited",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }
        public override string GetModName()
        {
            return Stands.ModInitials;
        }
    }
}

