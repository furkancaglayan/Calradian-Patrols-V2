using TaleWorlds.CampaignSystem;
using CalradianPatrols.Base;
using CalradianPatrols.Components;
using System;

namespace CalradianPatrols.Models
{
    public class DefaultPatrolPartyModel : PatrolPartyModel
    {
        public override float GetIdealDaysSpentOutside => 5;
        public override float GetIdealDaysSpentInSettlement => 1;
        public override float MinimumIdealFoodForDays => 1f;
        public override int MinimumTierForPatrolParties => 2;
        public override int DaysWorthOfFoodToGiveToParty => 6;
        public override int Tier1PatrolPartyIdealSize => 15;

        public override float GetAttackScoreforBanditParty(MobileParty party, PatrolPartyComponent patrolPartyComponent, MobileParty banditParty)
        {
            var aggressiveness = GetPatrolPartyAggressiveness(party);
            var strengthFactor = Math.Max(banditParty.GetTotalStrengthWithFollowers() - party.Party.TotalStrength, 0f);
            
            var banditMultiplier = banditParty.ActualClan.StringId == CampaignData.Looters ? 0.9f : 1.2f;
            var distance = Campaign.Current.Models.MapDistanceModel.GetDistance(party, banditParty);

            return banditMultiplier * (strengthFactor + aggressiveness - 0.5f) + ( 1 / (distance * distance));
        }

        public override int GetGoldCostForPatrolParty(Settlement settlement)
        {
            if (settlement.Town != null)
            {
                var baseCost = 5000;
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
                return 10000;
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
                return clan.Tier + 3;
            }

            return 0;
        }

        public override float GetNPCSettlementHirePatrolPartyChance(Settlement settlement)
        {
            float activePartyCount = CalradianPatrolsModuleManager.Current.PatrolBehaviorInformationProvider.GetActivePatrolPartyCount(settlement);
            return (0.14f - (activePartyCount * 0.012f));
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
