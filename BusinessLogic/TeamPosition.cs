// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var teamposition = Teamposition.FromJson(jsonString);

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TeamPosition
    {
        [JsonProperty("element_types")]
        public ElementType[] ElementTypes { get; set; }
    }

    public partial class ElementType
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("singular_name")]
        public string SingularName { get; set; }

        [JsonProperty("singular_name_short")]
        public string SingularNameShort { get; set; }

        [JsonProperty("plural_name")]
        public string PluralName { get; set; }

        [JsonProperty("plural_name_short")]
        public string PluralNameShort { get; set; }
    }

    public partial class TeamPosition
    {
        public static TeamPosition FromJson(string json) => JsonConvert.DeserializeObject<TeamPosition>(json, BusinessLogic.Converter.Settings);
    }
    
}
