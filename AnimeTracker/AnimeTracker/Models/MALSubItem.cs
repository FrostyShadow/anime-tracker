using Newtonsoft.Json;

namespace AnimeTracker.Models
{
    public class MALSubItem
    {
        [JsonProperty("mal_id")]
        public long MalId { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}