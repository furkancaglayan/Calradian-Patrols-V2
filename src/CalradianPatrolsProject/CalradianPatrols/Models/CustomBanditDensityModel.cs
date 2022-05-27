using CalradianPatrols.Extensions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;

namespace CalradianPatrols.Models
{
    public class CustomBanditDensityModel : BanditDensityModel
    {
        private BanditDensityModel _baseModel;
        public CustomBanditDensityModel(CampaignGameStarter campaignGameStarter)
        {
            _baseModel = campaignGameStarter.GetModelByType<BanditDensityModel>();
        }
        public override int NumberOfInitialHideoutsAtEachBanditFaction => _baseModel.NumberOfInitialHideoutsAtEachBanditFaction + 2;
        public override int NumberOfMaximumBanditPartiesAroundEachHideout => _baseModel.NumberOfMaximumBanditPartiesAroundEachHideout + 3;
        public override int NumberOfMaximumBanditPartiesInEachHideout => _baseModel.NumberOfMaximumBanditPartiesInEachHideout + 2;
        public override int NumberOfMaximumHideoutsAtEachBanditFaction => _baseModel.NumberOfMaximumHideoutsAtEachBanditFaction + 3;
        public override int NumberOfMaximumLooterParties => _baseModel.NumberOfMaximumLooterParties + 60;
        public override int NumberOfMinimumBanditPartiesInAHideoutToInfestIt => _baseModel.NumberOfMinimumBanditPartiesInAHideoutToInfestIt;
        public override int NumberOfMinimumBanditTroopsInHideoutMission => _baseModel.NumberOfMinimumBanditTroopsInHideoutMission;
        public override int NumberOfMaximumTroopCountForFirstFightInHideout => _baseModel.NumberOfMaximumTroopCountForFirstFightInHideout;
        public override int NumberOfMaximumTroopCountForBossFightInHideout => _baseModel.NumberOfMaximumTroopCountForBossFightInHideout;
        public override float SpawnPercentageForFirstFightInHideoutMission => _baseModel.SpawnPercentageForFirstFightInHideoutMission;

        public override int GetPlayerMaximumTroopCountForHideoutMission(MobileParty party)
        {
            return _baseModel.GetPlayerMaximumTroopCountForHideoutMission(party);
        }
    }
}
