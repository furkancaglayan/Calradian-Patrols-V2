using CalradianPatrols.Behaviors;
using CalradianPatrols.Models;
using CalradianPatrolsV2.CalradianPatrols;
using MCM.Abstractions.Settings.Providers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;

namespace CalradianPatrols.Base
{
    public class CalradianPatrolsSubModule : MBSubModuleBase
    {
        private CalradianPatrolsModuleManager ModManager;

        protected override void OnSubModuleLoad()
        {
            ModManager = new CalradianPatrolsModuleManager();      
        }

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            ModManager.Initialize();
        }

        public override void OnGameEnd(Game game)
        {
            base.OnGameEnd(game);
            //ModManager.OnGameEnd();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            if (game.GameType is Campaign campaign)
            {
                CampaignGameStarter campaignStarter = (CampaignGameStarter)gameStarter;
                AddModels(campaignStarter);
                AddBehaviors(campaignStarter);

                campaignStarter.LoadGameTexts(ModuleHelper.GetModuleFullPath("CalradianPatrolsV2") + "ModuleData/module_strings.xml");
                ModManager.OnGameStart();
            }
        }

        public override void OnCampaignStart(Game game, object starterObject)
        {
        }

        public override void OnGameLoaded(Game game, object initializerObject)
        {
            var campaign = game.GameType as Campaign;

            if (campaign != null)
            {
                var campaignInitializer = (CampaignGameStarter)initializerObject;
            }
        }


        private void AddBehaviors(CampaignGameStarter campaignStarter)
        {
            campaignStarter.AddBehavior(new PatrolsCampaignBehavior());
        }

        private void AddModels(CampaignGameStarter campaignStarter)
        {
            if (Settings.GetInstance().IncreasedBanditDensity)
            {
                campaignStarter.AddModel(new CustomBanditDensityModel(campaignStarter));
            }
            campaignStarter.AddModel(new CustomSettlementSecurityModel(campaignStarter));
            campaignStarter.AddModel(new DefaultPatrolPartyModel());
            campaignStarter.AddModel(new CustomPartySpeedCalculatingModel(campaignStarter));
            campaignStarter.AddModel(new CustomWageModel(campaignStarter));
        }
    }
}
