using CalradianPatrols.Components;
using CalradianPatrols.Extensions;
using CalradianPatrolsV2.CalradianPatrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;

namespace CalradianPatrols.Models
{
    public class CustomPartySpeedCalculatingModel : PartySpeedModel
    {
        private PartySpeedModel _baseModel;
        public CustomPartySpeedCalculatingModel(CampaignGameStarter campaignGameStarter)
        {
            _baseModel = campaignGameStarter.GetModelByType<PartySpeedModel>();
        }

        public override float BaseSpeed => _baseModel.BaseSpeed;
        public override float MinimumSpeed => _baseModel.MinimumSpeed;


        public override ExplainedNumber CalculateFinalSpeed(MobileParty mobileParty, ExplainedNumber finalSpeed)
        {
            var baseSpeed = _baseModel.CalculateFinalSpeed(mobileParty, finalSpeed);
            if (mobileParty.PartyComponent is PatrolPartyComponent)
            {
                baseSpeed.AddFactor(Settings.GetInstance().PatrolPartySpeedBonus);
            }

            return baseSpeed;
        }

        public override ExplainedNumber CalculateBaseSpeed(MobileParty party, bool includeDescriptions = false, int additionalTroopOnFootCount = 0, int additionalTroopOnHorseCount = 0)
        {
            return _baseModel.CalculateBaseSpeed(party, includeDescriptions, additionalTroopOnFootCount, additionalTroopOnHorseCount);
        }
    }
}
