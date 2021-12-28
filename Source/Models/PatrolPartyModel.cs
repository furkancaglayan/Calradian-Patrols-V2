using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using CalradianPatrols.Components;

namespace CalradianPatrols.Models
{
    public abstract class PatrolPartyModel : GameModel
    {
        public abstract float GetIdealDaysSpentOutside { get; }
        public abstract float GetIdealDaysSpentInSettlement { get; }
        public abstract float MinimumIdealFoodForDays { get; }
        public abstract int MinimumTierForPatrolParties { get;}
        public abstract int DaysWorthOfFoodToGiveToParty { get; }
        public abstract int Tier1PatrolPartyIdealSize { get; }
        public abstract int GetGoldCostForTroop(CharacterObject characterObject, Clan patrolClan);
        public abstract float GetPatrolPartyAggressiveness(MobileParty party);
        public abstract float GetNPCSettlementHirePatrolPartyChance(Settlement settlement);
        public abstract bool GetIsRosterStatusGoodForHunting(MobileParty party);
        public abstract int GetMaxAmountOfPartySizePerSettlement(Clan clan, Settlement settlement);
        public abstract float GetAttackScoreforBanditParty(MobileParty party, PatrolPartyComponent patrolPartyComponent, MobileParty banditParty);
        public abstract int GetGoldCostForPatrolParty(Settlement currentSettlement);
    }
}
