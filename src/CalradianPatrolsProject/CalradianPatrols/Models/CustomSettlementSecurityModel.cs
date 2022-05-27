using CalradianPatrols.Base;
using CalradianPatrols.Extensions;
using CalradianPatrolsV2.CalradianPatrols;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Settlements;

namespace CalradianPatrols.Models
{
    public class CustomSettlementSecurityModel : SettlementSecurityModel
    {
        private SettlementSecurityModel _baseModel;
        public CustomSettlementSecurityModel(CampaignGameStarter campaignGameStarter)
        {
            _baseModel = campaignGameStarter.GetModelByType<SettlementSecurityModel>();
        }

        public virtual float PatrolPartySecurityBonusRatio => Settings.GetInstance().PatrolPartySecurityBonusRatio;

        public override int MaximumSecurityInSettlement => _baseModel.MaximumSecurityInSettlement;
        public override int SecurityDriftMedium => _baseModel.SecurityDriftMedium;
        public override float MapEventSecurityEffectRadius => _baseModel.MapEventSecurityEffectRadius;
        public override float HideoutClearedSecurityEffectRadius => _baseModel.HideoutClearedSecurityEffectRadius;
        public override int HideoutClearedSecurityGain => _baseModel.HideoutClearedSecurityGain;
        public override int ThresholdForTaxCorruption => _baseModel.ThresholdForTaxCorruption;
        public override int ThresholdForHigherTaxCorruption => _baseModel.ThresholdForHigherTaxCorruption;
        public override int ThresholdForTaxBoost => _baseModel.ThresholdForTaxBoost;
        public override int SettlementTaxBoostPercentage => _baseModel.SettlementTaxBoostPercentage;
        public override int SettlementTaxPenaltyPercentage => _baseModel.SettlementTaxPenaltyPercentage;
        public override int ThresholdForNotableRelationBonus => _baseModel.ThresholdForNotableRelationBonus;
        public override int ThresholdForNotableRelationPenalty => _baseModel.ThresholdForNotableRelationPenalty;
        public override int DailyNotableRelationBonus => _baseModel.DailyNotableRelationBonus;
        public override int DailyNotableRelationPenalty => _baseModel.DailyNotableRelationPenalty;
        public override int DailyNotablePowerBonus => _baseModel.DailyNotablePowerBonus;
        public override int DailyNotablePowerPenalty => _baseModel.DailyNotablePowerPenalty;

        public override void CalculateGoldCutDueToLowSecurity(Town town, ref ExplainedNumber explainedNumber)
        {
            _baseModel.CalculateGoldGainDueToHighSecurity(town, ref explainedNumber);
        }

        public override void CalculateGoldGainDueToHighSecurity(Town town, ref ExplainedNumber explainedNumber)
        {
            _baseModel.CalculateGoldGainDueToHighSecurity(town, ref explainedNumber);
        }

        public override ExplainedNumber CalculateSecurityChange(Town town, bool includeDescriptions = false)
        {
            var result = _baseModel.CalculateSecurityChange(town, true);
            var patrolCount = CalradianPatrolsModuleManager.Current.PatrolBehaviorInformationProvider.GetActivePatrolPartyCount(town.Settlement);

            if (patrolCount > 0)
            {
                var bonus = patrolCount * PatrolPartySecurityBonusRatio;
                result.Add(bonus, TaleWorlds.Core.GameTexts.FindText("str_town_security_bonus"));
            }
            return result;
        }

        public override float GetLootedNearbyPartySecurityEffect(Town town, float sumOfAttackedPartyStrengths)
        {
            return _baseModel.GetLootedNearbyPartySecurityEffect(town, sumOfAttackedPartyStrengths);
        }

        public override float GetNearbyBanditPartyDefeatedSecurityEffect(Town town, float sumOfAttackedPartyStrengths)
        {
            return _baseModel.GetNearbyBanditPartyDefeatedSecurityEffect(town, sumOfAttackedPartyStrengths);
        }
    }
}
