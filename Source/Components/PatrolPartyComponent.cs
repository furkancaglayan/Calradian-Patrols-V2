using CalradianPatrols.Base;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace CalradianPatrols.Components
{
    public partial class PatrolPartyComponent : PartyComponent
    {
        public enum PatrolPartyState
        {
            Invalid,
            GoingToSettlementForFood,
            GoingToSettlementForUnits,
            Patrolling,
            GoingAfterPartyByOrder,
            EngagingBandits,
            Disbanding,
            FollowingLord,
            Resting,
        }
        
        public PatrolPartyState State { get; set; } = PatrolPartyState.Invalid;
        
        [SaveableProperty(10)]
        public Settlement Settlement { get; private set; }

        [SaveableProperty(20)]
        public PartyTemplateObject Template { get; private set; }
        
        [SaveableProperty(30)]
        public int Gold { get; set; }

        [SaveableProperty(40)]
        public Clan RulerClan { get; set; }

        public bool IsFollowingPlayer => State == PatrolPartyState.FollowingLord;

        public override TextObject Name
        {
            get
            {
                /*if (NativeConfig.CheatMode && Settlement != null && MobileParty != null)
                {
                    var dist = Campaign.Current.Models.MapDistanceModel.GetDistance(MobileParty, Settlement);
                    var cheatModeName = new TextObject("{=*}{SETTLEMENT} Town Patrol ({DISTANCE}) - ({STATUS})");
                    cheatModeName.SetTextVariable("SETTLEMENT", Settlement.Name);
                    cheatModeName.SetTextVariable("STATUS", State.ToString());
                    cheatModeName.SetTextVariable("DISTANCE", (int)dist);
                    return cheatModeName;
                }*/
                var name = new TextObject("{=*}{SETTLEMENT} Town Patrol");
                name.SetTextVariable("SETTLEMENT", Settlement.Name);
                return name;
            }
        }

        public override Hero PartyOwner => RulerClan.Leader;
        public override Settlement HomeSettlement { get; }


        private static void SetUpParty(MobileParty party, Clan manhunterClan)
        {
            party.ActualClan = manhunterClan;
            party.Aggressiveness = CalradianPatrolsModuleManager.Current.PatrolModel.GetPatrolPartyAggressiveness(party);
            party.Ai.SetDoNotMakeNewDecisions(true);

            //mobilepartytracker
            party.SetPartyUsedByQuest(true);
        }

        public static MobileParty CreatePatrolParty(string stringId, Settlement targetSettlement, Clan manhunterClan, PartyTemplateObject partyTemplateObject)
        {
            return MobileParty.CreateParty(stringId, new PatrolPartyComponent(targetSettlement, partyTemplateObject), (MobileParty x) => SetUpParty(x, manhunterClan));
        }

        public PatrolPartyComponent(Settlement targetSettlement, PartyTemplateObject partyTemplateObject)
        {
            Settlement = targetSettlement;
            Template = partyTemplateObject;
            RulerClan = targetSettlement.OwnerClan;
            Gold = 0;
        }

        public void TakeAction(PatrolPartyState partyState, Settlement settlement = null, MobileParty party = null)
        {
            State = partyState;
            switch (partyState)
            {
                case PatrolPartyState.Invalid:
                    break;
                case PatrolPartyState.GoingToSettlementForFood:
                case PatrolPartyState.GoingToSettlementForUnits:
                    GoToSettlement(settlement);
                    break;
                case PatrolPartyState.Patrolling:
                    PatrolAroundSettlement(settlement);
                    break;
                case PatrolPartyState.GoingAfterPartyByOrder:
                case PatrolPartyState.EngagingBandits:
                    EngageBanditParty(party);
                    break;
                case PatrolPartyState.FollowingLord:
                    FollowParty(party);
                    break;
                case PatrolPartyState.Resting:
                    RestInSettlement(settlement);
                    break;
                case PatrolPartyState.Disbanding:
                    Disband();
                    break;
                default:
                    break;
            }
        }

        private void PatrolAroundSettlement(Settlement settlement)
        {
            MobileParty.SetMovePatrolAroundSettlement(settlement);
        }

        private void FollowParty(MobileParty party)
        {
            MobileParty.SetMoveEscortParty(party);
        }

        private void GoToSettlement(Settlement settlement)
        {
            MobileParty.SetMoveGoToSettlement(settlement);
        }

        private void Disband()
        {
            DisbandPartyAction.ApplyDisband(MobileParty);
        }

        private void EngageBanditParty(MobileParty party)
        {
            MobileParty.SetMoveEngageParty(party);
        }

        private void RestInSettlement(Settlement settlement)
        {

        }
    }
}
