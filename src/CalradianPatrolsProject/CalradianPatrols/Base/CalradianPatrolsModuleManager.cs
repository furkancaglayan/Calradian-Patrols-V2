using CalradianPatrols.Behaviors;
using CalradianPatrols.Models;
using CalradianPatrols.Notice;
using CalradianPatrolsV2.CalradianPatrols;
using SandBox.View.Map;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;

namespace CalradianPatrols.Base
{
    public class CalradianPatrolsModuleManager
    {
        public static CalradianPatrolsModuleManager Current { get; private set; }
        public IPatrolBehaviorInformationProvider PatrolBehaviorInformationProvider { get; private set; }

        //private bool _registered;

        public static Settings Settings
        {
            get
            {
                if (Settings.Instance == null)
                {
                    if (_baseSettings == null)
                    {
                        _baseSettings = new Settings();
                    }

                    return _baseSettings;
                }
                else
                {
                    return Settings.Instance;
                }
            }
        }
        private static Settings _baseSettings;
        public PatrolPartyModel PatrolModel
        {
            get
            {
                if(_patrolModel == null)
                {
                    _patrolModel = (PatrolPartyModel)Campaign.Current.Models.GetGameModels().First(x => x is DefaultPatrolPartyModel);
                }

                return _patrolModel;
            }
        }

        private PatrolPartyModel _patrolModel;

        public CalradianPatrolsModuleManager()
        {
            Current = this;
        }

        public void Initialize()
        {
            //ScreenManager.OnPushScreen += OnScreenManagerPushScreen;
            PatrolBehaviorInformationProvider = Campaign.Current.GetCampaignBehavior<PatrolsCampaignBehavior>();
        }

        public void OnGameStart()
        {
           
        }

        /*private void OnScreenManagerPushScreen(ScreenBase pushedScreen)
        {
            if (!_registered && pushedScreen is MapScreen mapScreen)
            {
                mapScreen.MapNotificationView.RegisterMapNotificationType(typeof(PatrolPartyCreatedMapNotification),
                                                                          typeof(PatrolPartyCreatedItemVM)); 
                _registered = true;
            }
        }

        public void OnGameEnd()
        {
            ScreenManager.OnPushScreen -= OnScreenManagerPushScreen;
            _registered = false;
        }*/
    }
}
