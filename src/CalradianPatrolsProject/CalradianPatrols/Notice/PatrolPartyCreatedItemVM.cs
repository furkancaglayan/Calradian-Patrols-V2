using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes;

namespace CalradianPatrols.Notice
{
    public class PatrolPartyCreatedItemVM : MapNotificationItemBaseVM
    {
        public PatrolPartyCreatedItemVM(PatrolPartyCreatedMapNotification data) : base(data)
        {
            NotificationIdentifier = "battle";
            _onInspect = () =>
            {
                if (data.MobileParty != null)
                {
                    data.MobileParty.Party.SetAsCameraFollowParty();
                }

                ExecuteRemove();
            };
        }
    }
}
