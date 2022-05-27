using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;

namespace CalradianPatrols
{
    public interface IPatrolBehaviorInformationProvider
    {
        int GetActivePatrolPartyCount(Settlement settlement);
        int GetPatrolPartyOnQueueCount(Settlement settlement);
    }
}
