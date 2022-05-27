using CalradianPatrols.Notice;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using static CalradianPatrols.Behaviors.PatrolsCampaignBehavior;
using CalradianPatrols.Components;
using static CalradianPatrols.Components.PatrolPartyComponent;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Party;

namespace CalradianPatrols.SaveLoad
{
    public class CalradianPatrolsCustomSaveDefiner : CampaignBehaviorBase.SaveableCampaignBehaviorTypeDefiner
    {
        public CalradianPatrolsCustomSaveDefiner() : base(435142)
        {

        }

        protected override void DefineClassTypes()
        {
            AddClassDefinition(typeof(PatrolPartyComponent), 1);
            AddClassDefinition(typeof(PatrolEncounterData), 2);
            AddClassDefinition(typeof(PatrolPartyCreatedMapNotification), 3);
        }

        protected override void DefineContainerDefinitions()
        {
            ConstructContainerDefinition(typeof(Dictionary<Settlement, List<MobileParty>>));
            ConstructContainerDefinition(typeof(Dictionary<Settlement, bool>));
            ConstructContainerDefinition(typeof(List<PatrolEncounterData>));
            ConstructContainerDefinition(typeof(Dictionary<MobileParty, List<PatrolEncounterData>>));
        }

        protected override void DefineEnumTypes()
        {
            AddEnumDefinition(typeof(PatrolPartyState), 2435231);
        }
    }
}
