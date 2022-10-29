using TaleWorlds.CampaignSystem;
using CalradianPatrols.Base;
using CalradianPatrols.Components;
using System;
using CalradianPatrolsV2.CalradianPatrols;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace CalradianPatrols.Models
{
    public class DefaultPatrolPartyModel : PatrolPartyModel
    {
        public override float MinimumIdealFoodForDays => Settings.GetInstance().PatrolPartyFoodThreshold;
        public override int MinimumTierForPatrolParties => Settings.GetInstance().PatrolPartiesTierRequirement;
        public override int DaysWorthOfFoodToGiveToParty => 7;
        public override int Tier1PatrolPartyIdealSize => Settings.GetInstance().AveragePartySize;
        public override int MaximumCustomGarrisonPartySize => Settings.GetInstance().MaxPartySizeFromGarrison;
        public override int MinimumCustomGarrisonPartySize => 12;
        public override float GetAttackScoreForParty(MobileParty party, PatrolPartyComponent patrolPartyComponent, MobileParty banditParty)
        {
            var aggressiveness = GetPatrolPartyAggressiveness(party);
            var strengthFactor = Math.Max(banditParty.GetTotalStrengthWithFollowers() - party.Party.TotalStrength, 0f);
            
            var banditMultiplier = banditParty.ActualClan.StringId == CampaignData.Looters ? 0.9f : 1.2f;
            var distance = Campaign.Current.Models.MapDistanceModel.GetDistance(party, banditParty);

            return banditMultiplier * (strengthFactor + aggressiveness - 0.5f) + ( 1 / (distance * distance));
        }

        public override bool CanNPCClanRecruitPartyForTown(Clan clan, Town town)
        {
            return clan != null && 
                   town != null &&
                   town.IsTown &&
                   !town.Settlement.IsUnderSiege &&
                   !town.InRebelliousState &&
                   clan.Gold > GetGoldCostForPatrolParty(town.Settlement) * 3;
        }

        public override int GetGoldCostForPatrolParty(Settlement settlement)
        {
            if (settlement.Town != null)
            {
                var baseCost = Settings.GetInstance().BaseGoldCost;
                var securityEffect = (settlement.Town.Security - 50) * 20;
                var loyaltyEffect = (settlement.Town.Loyalty - 80) * 40;

                var existingPartiesEffect = 
                    (CalradianPatrolsModuleManager.Current.PatrolBehaviorInformationProvider.GetActivePatrolPartyCount(settlement) +
                    CalradianPatrolsModuleManager.Current.PatrolBehaviorInformationProvider.GetPatrolPartyOnQueueCount(settlement)) * 1000;

                return (int)(baseCost + securityEffect + loyaltyEffect + existingPartiesEffect);
            }
            else
            {
                //wrong
                return 12000;
            }
        }

        public override bool GetIsRosterStatusGoodForHunting(MobileParty party)
        {
            return party.MemberRoster.TotalHealthyCount > party.PrisonRoster.TotalManCount && party.MemberRoster.TotalManCount > 3;
        }

        public override int GetMaxAmountOfPartySizePerSettlement(Clan clan, Settlement settlement)
        {
            if (settlement.IsTown)
            {
                var setting = Settings.GetInstance().PatrolPartyCountPerTown;
                if (setting == 0)
                {
                    return Math.Max(clan.Tier + 1, 1);
                }

                return setting;
            }

            return 0;
        }

        public override float GetNPCSettlementHirePatrolPartyChance(Settlement settlement)
        {
            float activePartyCount = CalradianPatrolsModuleManager.Current.PatrolBehaviorInformationProvider.GetActivePatrolPartyCount(settlement);
            if (activePartyCount >= Settings.GetInstance().PatrolPartyCountPerTown)
            {
                return 0.0f;
            }

            return Settings.GetInstance().AISpawnPartyChance;
        }

        public override float GetPatrolPartyAggressiveness(MobileParty party)
        {
            return 1f;
        }

        public override int GetGoldCostForTroop(CharacterObject characterObject, Clan patrolClan)
        {
            var baseCost = 15;
            return characterObject.Tier * baseCost;
        }
    }
}
