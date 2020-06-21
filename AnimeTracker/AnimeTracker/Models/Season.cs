using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnimeTracker.Models
{
    public class Season
    {
        [JsonProperty("season_name")]
        public string SeasonName { get; set; }
        [JsonProperty("season_year")]
        public int SeasonYear { get; set; }
        [JsonProperty("anime")]
        public IList<Anime> AnimeList { get; set; }
    }
}