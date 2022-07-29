using CalradianPatrols.Base;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v1;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace CalradianPatrolsV2.CalradianPatrols
{
    public class Settings : AttributeGlobalSettings<Settings>
    {
        public override string Id => "CalradianPatrolsV2";
        public override string DisplayName => new TextObject("{=str_calradianpatrols_mod_name}Calradian Patrols V2").ToString();
        public override string FolderName => "CalradianPatrolsV2";
        public override string FormatType => "xml";


        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_0_0}Patrol Party Security Bonus Ratio", 0f, 1f, "0%", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_0_1}Contribution to town security for each party (Default=0.15).")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_advanced}Calradian Patrols - Advanced")]
        public float PatrolPartySecurityBonusRatio { get; set; } = 0.15f;
        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_1_0}Patrol Party Speed Bonus", 0f, 10f, "0%", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_1_1}Increases patrol party speed.")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_advanced}Calradian Patrols - Advanced")]
        public float PatrolPartySpeedBonus { get; set; } = 1f;
        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_2_0}Patrol Party Food Threshold to Go Back", 1f, 10f, "0", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_2_1}Patrol Party will go back their settlements to buy food, if they have less than specified days worth of food.")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_advanced}Calradian Patrols - Advanced")]
        public float PatrolPartyFoodThreshold { get; set; } = 1f;
        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_3_0}Patrol Party Spawn Time In Hours", 1f, 100f, "0", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_3_1}Patrol Party spawn time in hours")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_advanced}Calradian Patrols - Advanced")]
        public int TargetHoursForSpawn { get; set; } = 24;

        [SettingProperty("{=str_calradianpatrols_settings_4_0}Stronger Bandits", RequireRestart = false, HintText = "{=str_calradianpatrols_settings_4_1}An option to increase bandit density and number of looters")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_basic}Calradian Patrols - Basic", GroupOrder = 0)]
        public bool IncreasedBanditDensity { get; set; } = true;

        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_5_0}Patrol Party Count per Town", 0f, 100f, "0", Order = 0, RequireRestart = true, HintText = "{=str_calradianpatrols_settings_5_1}Patrol Party Count - Higher values may drain performance.")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_basic}Calradian Patrols - Basic", GroupOrder = 0)]
        public int PatrolPartyCountPerTown { get; set; } = 8;
        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_12_0}Patrol Party Count per Castle", 0f, 100f, "0", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_12_1}Patrol Party Count for castles - Higher values may drain performance.")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_basic}Calradian Patrols - Basic", GroupOrder = 0)]
        public int PatrolPartiesTierRequirement { get; set; } = 2;
        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_7_0}Base Spawn Chance for AI", 0f, 1f, "0%", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_7_1}Base chance to spawn a patrol party for AI. Higher values my drain performance")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_basic}Calradian Patrols - Basic", GroupOrder = 0)]
        public float BaseAISpawnPartyChange { get; set; } = 0.12f;
        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_8_0}Patrol Party Base Gold Cost", 0f, 100000f, "0", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_8_1}Cost still increases with each party.")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_basic}Calradian Patrols - Basic", GroupOrder = 0)]
        public int BaseGoldCost { get; set; } = 8000;
        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_9_0}Patrol Party Average Size", 1f, 500f, "0", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_9_1}Average party size created by the game (excluding parties that are created from garrison)")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_basic}Calradian Patrols - Basic", GroupOrder = 0)]
        public int AveragePartySize { get; set; } = 30;
        [SettingPropertyFloatingInteger("{=str_calradianpatrols_settings_10_0}Patrol Party from Garrison Max Size", 12f, 500f, "0", Order = 0, RequireRestart = false, HintText = "{=str_calradianpatrols_settings_10_1}Max Size For Garrison Created Parties")]
        [SettingPropertyGroup("{=str_calradianpatrols_settings_basic}Calradian Patrols - Basic", GroupOrder = 0)]
        public int MaxPartySizeFromGarrison { get; set; } = 50;
        public static Settings GetInstance() => CalradianPatrolsModuleManager.Settings;
    }
}
