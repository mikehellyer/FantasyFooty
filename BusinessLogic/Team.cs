// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var team = Team.FromJson(jsonString);

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Team
    {
        [JsonProperty("teams")]
        public TeamElement[] Teams { get; set; }
    }

    public partial class TeamElement
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("current_event_fixture")]
        public TEventFixture[] CurrentEventFixture { get; set; }

        [JsonProperty("next_event_fixture")]
        public TEventFixture[] NextEventFixture { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("unavailable")]
        public bool Unavailable { get; set; }

        [JsonProperty("strength")]
        public long Strength { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("played")]
        public long Played { get; set; }

        [JsonProperty("win")]
        public long Win { get; set; }

        [JsonProperty("loss")]
        public long Loss { get; set; }

        [JsonProperty("draw")]
        public long Draw { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }

        [JsonProperty("form")]
        public object Form { get; set; }

        [JsonProperty("link_url")]
        public string LinkUrl { get; set; }

        [JsonProperty("strength_overall_home")]
        public long StrengthOverallHome { get; set; }

        [JsonProperty("strength_overall_away")]
        public long StrengthOverallAway { get; set; }

        [JsonProperty("strength_attack_home")]
        public long StrengthAttackHome { get; set; }

        [JsonProperty("strength_attack_away")]
        public long StrengthAttackAway { get; set; }

        [JsonProperty("strength_defence_home")]
        public long StrengthDefenceHome { get; set; }

        [JsonProperty("strength_defence_away")]
        public long StrengthDefenceAway { get; set; }

        [JsonProperty("team_division")]
        public long TeamDivision { get; set; }
    }

    public partial class TEventFixture
    {
        [JsonProperty("is_home")]
        public bool IsHome { get; set; }

        [JsonProperty("day")]
        public long Day { get; set; }

        [JsonProperty("event_day")]
        public long EventDay { get; set; }

        [JsonProperty("month")]
        public long Month { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("opponent")]
        public long Opponent { get; set; }
    }


    
    public partial class Team
    {
        public static Team FromJson(string json) => JsonConvert.DeserializeObject<Team>(json, BusinessLogic.Converter.Settings);
    }

   
}
