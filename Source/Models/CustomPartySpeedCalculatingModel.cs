using CalradianPatrols.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace CalradianPatrols.Models
{
    public class CustomPartySpeedCalculatingModel : DefaultPartySpeedCalculatingModel
    {
        public override ExplainedNumber CalculateFinalSpeed(MobileParty mobileParty, ExplainedNumber finalSpeed)
        {
            if (mobileParty.PartyComponent is PatrolPartyComponent)
            {
                finalSpeed.Add(0.3f);
            }

            return finalSpeed;
        }
    }
}
