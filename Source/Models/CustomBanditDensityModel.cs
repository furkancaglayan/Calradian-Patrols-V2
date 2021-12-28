using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace CalradianPatrols.Models
{
    public class CustomBanditDensityModel : DefaultBanditDensityModel
    {
        public override int NumberOfInitialHideoutsAtEachBanditFaction => base.NumberOfInitialHideoutsAtEachBanditFaction + 2;
        public override int NumberOfMaximumBanditPartiesAroundEachHideout => base.NumberOfMaximumBanditPartiesAroundEachHideout + 3;
        public override int NumberOfMaximumBanditPartiesInEachHideout => base.NumberOfMaximumBanditPartiesInEachHideout + 2;
        public override int NumberOfMaximumHideoutsAtEachBanditFaction => base.NumberOfMaximumHideoutsAtEachBanditFaction + 3;
        public override int NumberOfMaximumLooterParties => base.NumberOfMaximumLooterParties + 60;
        public override int NumberOfMinimumBanditPartiesInAHideoutToInfestIt => base.NumberOfMinimumBanditPartiesInAHideoutToInfestIt;
    }
}
