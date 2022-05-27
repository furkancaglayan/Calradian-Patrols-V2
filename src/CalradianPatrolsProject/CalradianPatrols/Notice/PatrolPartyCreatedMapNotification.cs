using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace CalradianPatrols.Notice
{
    public class PatrolPartyCreatedMapNotification : InformationData
    {
        public override TextObject TitleText => GameTexts.FindText("str_new_patrol_party");
        public override string SoundEventPath => ""; //"event:/ui/notification/child_born"; 

        [SaveableProperty(1)]
        public MobileParty MobileParty { get; private set; }

        public PatrolPartyCreatedMapNotification(MobileParty party, TextObject text) : base(text)
        {
            MobileParty = party;
        }

        public override bool IsValid()
        {
            return MobileParty != null && MobileParty.IsActive;
        }
    }
}
