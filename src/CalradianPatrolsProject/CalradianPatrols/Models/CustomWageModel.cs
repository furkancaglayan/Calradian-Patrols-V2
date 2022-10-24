using CalradianPatrols.Components;
using CalradianPatrols.Extensions;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;

namespace CalradianPatrols.Models
{
    public class CustomWageModel : PartyWageModel
    {
        private PartyWageModel _baseModel;
        public CustomWageModel(CampaignGameStarter campaignGameStarter)
        {
            _baseModel = campaignGameStarter.GetModelByType<PartyWageModel>();
        }

        public override int MaxWage => _baseModel.MaxWage;

        public override int GetCharacterWage(CharacterObject character)
        {
            return _baseModel.GetCharacterWage(character);
        }

        public override ExplainedNumber GetTotalWage(MobileParty mobileParty, bool includeDescriptions = false)
        {
            if (mobileParty.PartyComponent is PatrolPartyComponent)
            {
                return new ExplainedNumber(0);
            }

            return _baseModel.GetTotalWage(mobileParty, includeDescriptions);
        }

        public override int GetTroopRecruitmentCost(CharacterObject troop, Hero buyerHero, bool withoutItemCost = false)
        {
            return _baseModel.GetTroopRecruitmentCost(troop, buyerHero, withoutItemCost);
        }
    }
}
