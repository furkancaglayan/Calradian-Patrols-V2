using CalradianPatrols.Base;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Load;

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

        [SaveableProperty(50)]
        public TextObject _customName { get; private set; }

        public bool IsFollowingPlayer => State == PatrolPartyState.FollowingLord;

        public override TextObject Name
        {
            get
            {
                if (_customName != null)
                {
                    return _customName;
                }

                TextObject name;
                /*if (NativeConfig.CheatMode && Settlement != null && MobileParty != null)
                {
                    name = new TextObject("{=!}{SETTLEMENT} Town Patrol ({STATE})");
                    name.SetTextVariable("STATE", State.ToString());
                }
                else*/
                {
                    name = GameTexts.FindText("str_settlement_town_patrol");
                }

                name.SetTextVariable("SETTLEMENT", Settlement.Name);
                return name;
            }
        }

        public void SetCustomName(TextObject partyName)
        {
            _customName = partyName;
        }

        public override Hero PartyOwner => RulerClan.Leader;
        public override Settlement HomeSettlement { get; }


        [LoadInitializationCallback]
        private void OnLoad(MetaData metaData, ObjectLoadData objectLoadData)
        {
            //Tier = Math.Max(1, Tier);
        }

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
            return MobileParty.CreateParty(stringId, new PatrolPartyComponent(targetSettlement, partyTemplateObject/*, tier*/), (MobileParty x) => SetUpParty(x, manhunterClan));
        }

        public PatrolPartyComponent(Settlement targetSettlement, PartyTemplateObject partyTemplateObject/*, int tier*/)
        {
            Settlement = targetSettlement;
            Template = partyTemplateObject;
            RulerClan = targetSettlement.OwnerClan;
            Gold = 0;
            //Tier = tier;
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
            MobileParty.Ai.SetMovePatrolAroundSettlement(settlement);
        }

        private void FollowParty(MobileParty party)
        {
            MobileParty.Ai.SetMoveEscortParty(party);
        }

        private void GoToSettlement(Settlement settlement)
        {
            MobileParty.Ai.SetMoveGoToSettlement(settlement);
        }

        private void Disband()
        {
            DisbandPartyAction.StartDisband(MobileParty);
        }

        private void EngageBanditParty(MobileParty party)
        {
            MobileParty.Ai.SetMoveEngageParty(party);
        }

        private void RestInSettlement(Settlement settlement)
        {

        }
    }
}
