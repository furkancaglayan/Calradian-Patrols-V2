using TaleWorlds.CampaignSystem;

namespace CalradianPatrols
{
    public interface IPatrolBehaviorInformationProvider
    {
        int GetActivePatrolPartyCount(Settlement settlement);
        int GetPatrolPartyOnQueueCount(Settlement settlement);
    }
}
