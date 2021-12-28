using CalradianPatrols.Base;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace CalradianPatrols.Models
{
    public class  CustomSettlementSecurityModel : DefaultSettlementSecurityModel
    {
        public virtual float PatrolPartySecurityBonusRatio => 0.15f;
        public override ExplainedNumber CalculateSecurityChange(Town town, bool includeDescriptions = false)
        {
            var result = base.CalculateSecurityChange(town, true);
            var patrolCount = CalradianPatrolsModuleManager.Current.PatrolBehaviorInformationProvider.GetActivePatrolPartyCount(town.Settlement);
            
            if(patrolCount > 0)
            {
                var bonus = patrolCount * PatrolPartySecurityBonusRatio;
                result.Add(bonus, new TaleWorlds.Localization.TextObject("{=*}Town Patrol"));
            }
            return result;
        }
    }
}
