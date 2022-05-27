using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace CalradianPatrolsProject.CalradianPatrols
{
    public class CustomCharacterCreationContent : CharacterCreationContentBase
    {
        public override TextObject ReviewPageDescription { get; }

        protected override void OnInitialized(CharacterCreation characterCreation)
        {
            FirstMenu(characterCreation);
        }

        private void FirstMenu(CharacterCreation characterCreation)
        {
            var menu = new CharacterCreationMenu(new TextObject("{=b4lDDcli}Coming out"), new TextObject("{=XgFU1pCx}Very nice first menu beybi"), FirstMenuInit);

            var empireCategory = menu.AddMenuCategory();
            empireCategory.AddCategoryOption(new TextObject("{=InN5ZZt3}A landlord's retainers"), new List<SkillObject>() { DefaultSkills.Bow }, null, FocusToAdd, SkillLevelToAdd, AttributeLevelToAdd, null, null, null,
               new TextObject("{=ivKl4mV2}Your father was a trusted lieutenant of the local landowning aristocrat. He rode with the lord's cavalry, fighting as an armored lancer."));

            empireCategory.AddCategoryOption(new TextObject("{=InN5ZZt3}A landlord's secondarsadşasd"), new List<SkillObject>() { DefaultSkills.Bow }, null, FocusToAdd, SkillLevelToAdd, AttributeLevelToAdd, null, null, null,
               new TextObject("{=ivKl4mV2}Your faasdasdadsahe local landowning aristocrat. He rode with the lord's cavalry, fighting as an armored lancer."));

            var nonEmpireCategory = menu.AddMenuCategory();

            nonEmpireCategory.AddCategoryOption(new TextObject("{=InN5ZZt3}Non empire"), new List<SkillObject>() { DefaultSkills.Bow }, null, FocusToAdd, SkillLevelToAdd, AttributeLevelToAdd, null, null, null,
             new TextObject("{=ivKl4mV2}Your father was a trusted lieutenant of the local landowning aristocrat. He rode with the lord's cavalry, fighting as an armored lancer."));

            nonEmpireCategory.AddCategoryOption(new TextObject("{=InN5ZZt3}Non empire"), new List<SkillObject>() { DefaultSkills.Bow }, null, FocusToAdd, SkillLevelToAdd, AttributeLevelToAdd, null, null, null,
               new TextObject("{=ivKl4mV2}Your faasdasdadsahe local landowning aristocrat. He rode with the lord's cavalry, fighting as an armored lancer."));

            characterCreation.AddNewMenu(menu);
        }

        private void FirstMenuInit(CharacterCreation characterCreation)
        {

        }

        private bool empire_condition()
        {
            return GetSelectedCulture().StringId == CampaignData.CultureEmpire;
        }

        private bool nonempire_condition()
        {
            return !empire_condition();
        }

    }
}
