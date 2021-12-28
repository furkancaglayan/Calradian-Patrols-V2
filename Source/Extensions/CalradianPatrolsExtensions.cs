using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace CalradianPatrols.Extensions
{
    public static class CalradianPatrolsExtensions
    {
        public static void SetLeaderToHigherTroop(this TroopRoster memberRoster)
        {
            var list = memberRoster.GetTroopRoster();
            var leaderTroop = list[0];
            var leaderTier = leaderTroop.Character.Tier;
            var selectedTroop = leaderTroop;

            for (int i = 0; i < memberRoster.Count; i++)
            {
                var troop = memberRoster.GetElementCopyAtIndex(i);
                if (troop.Character.Tier > leaderTier)
                {
                    selectedTroop = troop;
                }
                else if (troop.Character.Tier == leaderTier && troop.Character.IsMounted && !selectedTroop.Character.IsMounted)
                {
                    selectedTroop = troop;
                }

                if (selectedTroop.Character.IsMounted && selectedTroop.Character.Tier == 5)
                {
                    break;
                }
            }

            if (selectedTroop.Character != leaderTroop.Character)
            {
                memberRoster.AddToCounts(selectedTroop.Character, -selectedTroop.Number);
                memberRoster.AddToCounts(selectedTroop.Character, selectedTroop.Number, true);
                memberRoster.RemoveZeroCounts();
            }
        }

        public static float GetRemainingFoodInDays(this MobileParty party)
        {
            var food = party.Food;
            var dailyFoodConsumption = MBMath.Absf(party.FoodChange);

            return food / dailyFoodConsumption;
        }

        public static void Set<T1, T2>(this Dictionary<T1, List<T2>> dict, T1 key, T2 value)
        {
            if (dict.TryGetValue(key, out List<T2> oldValue))
            {
                dict[key].Add(value);
            }
            else
            {
                dict.Add(key, new List<T2>() { value });
            }
        }

        public static void Set<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T2 value)
        {
            if (dict.TryGetValue(key, out T2 oldValue))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }
    }
}
