// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var fantasyPlayer = FantasyPlayer.FromJson(jsonString);

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FantasyPlayer
    {
        [JsonProperty("elements")]
        public Element[] Element { get; set; }

        [JsonProperty("current-event")]
        public string Game_Week { get; set; }
       
    }
 

    public partial class Element
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("web_name")]
        public string WebName { get; set; }

        [JsonProperty("team_code")]
        public long TeamCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("second_name")]
        public string SecondName { get; set; }

        [JsonProperty("squad_number")]
        public string SquadNumber { get; set; }

        [JsonProperty("news")]
        public string News { get; set; }

        [JsonProperty("now_cost")]
        public long NowCost { get; set; }

        [JsonProperty("news_added")]
        public DateTimeOffset? NewsAdded { get; set; }

        [JsonProperty("chance_of_playing_this_round")]
        public long? ChanceOfPlayingThisRound { get; set; }

        [JsonProperty("chance_of_playing_next_round")]
        public long? ChanceOfPlayingNextRound { get; set; }

        [JsonProperty("value_form")]
        public string ValueForm { get; set; }

        [JsonProperty("value_season")]
        public string ValueSeason { get; set; }

        [JsonProperty("cost_change_start")]
        public long CostChangeStart { get; set; }

        [JsonProperty("cost_change_event")]
        public long CostChangeEvent { get; set; }

        [JsonProperty("cost_change_start_fall")]
        public long CostChangeStartFall { get; set; }

        [JsonProperty("cost_change_event_fall")]
        public long CostChangeEventFall { get; set; }

        [JsonProperty("in_dreamteam")]
        public bool InDreamteam { get; set; }

        [JsonProperty("dreamteam_count")]
        public long DreamteamCount { get; set; }

        [JsonProperty("selected_by_percent")]
        public string SelectedByPercent { get; set; }

        [JsonProperty("form")]
        public string Form { get; set; }

        [JsonProperty("transfers_out")]
        public long TransfersOut { get; set; }

        [JsonProperty("transfers_in")]
        public long TransfersIn { get; set; }

        [JsonProperty("transfers_out_event")]
        public long TransfersOutEvent { get; set; }

        [JsonProperty("transfers_in_event")]
        public long TransfersInEvent { get; set; }

        [JsonProperty("loans_in")]
        public long LoansIn { get; set; }

        [JsonProperty("loans_out")]
        public long LoansOut { get; set; }

        [JsonProperty("loaned_in")]
        public long LoanedIn { get; set; }

        [JsonProperty("loaned_out")]
        public long LoanedOut { get; set; }

        [JsonProperty("total_points")]
        public long TotalPoints { get; set; }

        [JsonProperty("event_points")]
        public long EventPoints { get; set; }

        [JsonProperty("points_per_game")]
        public string PointsPerGame { get; set; }

        [JsonProperty("ep_this")]
        public string EpThis { get; set; }

        [JsonProperty("ep_next")]
        public string EpNext { get; set; }

        [JsonProperty("special")]
        public bool Special { get; set; }

        [JsonProperty("minutes")]
        public long Minutes { get; set; }

        [JsonProperty("goals_scored")]
        public long GoalsScored { get; set; }

        [JsonProperty("assists")]
        public long Assists { get; set; }

        [JsonProperty("clean_sheets")]
        public long CleanSheets { get; set; }

        [JsonProperty("goals_conceded")]
        public long GoalsConceded { get; set; }

        [JsonProperty("own_goals")]
        public long OwnGoals { get; set; }

        [JsonProperty("penalties_saved")]
        public long PenaltiesSaved { get; set; }

        [JsonProperty("penalties_missed")]
        public long PenaltiesMissed { get; set; }

        [JsonProperty("yellow_cards")]
        public long YellowCards { get; set; }

        [JsonProperty("red_cards")]
        public long RedCards { get; set; }

        [JsonProperty("saves")]
        public long Saves { get; set; }

        [JsonProperty("bonus")]
        public long Bonus { get; set; }

        [JsonProperty("bps")]
        public long Bps { get; set; }

        [JsonProperty("influence")]
        public string Influence { get; set; }

        [JsonProperty("creativity")]
        public string Creativity { get; set; }

        [JsonProperty("threat")]
        public string Threat { get; set; }

        [JsonProperty("ict_index")]
        public string IctIndex { get; set; }

        [JsonProperty("ea_index")]
        public long EaIndex { get; set; }

        [JsonProperty("element_type")]
        public long ElementType { get; set; }

        [JsonProperty("team")]
        public long Team { get; set; }
    }

    public partial class FantasyPlayer
    {
        public static FantasyPlayer FromJson(string json) => JsonConvert.DeserializeObject<FantasyPlayer>(json, BusinessLogic.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this FantasyPlayer self) => JsonConvert.SerializeObject(self, BusinessLogic.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
