using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnimeTracker.Models
{
    public class Anime
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        [JsonProperty("title_english")]
        public string TitleEnglish { get; set; }
        [JsonProperty("title_japanese")]
        public string TitleJapanese { get; set; }
        [JsonProperty("title_synonyms")]
        public ICollection<string> TitleSynonyms { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Episodes { get; set; }
        public string Status { get; set; }
        public bool Airing { get; set; }
        public TimePeriod Aired { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public float? Score { get; set; }
        [JsonProperty("scored_by")]
        public int? ScoredBy { get; set; }
        public int? Rank { get; set; }
        public int? Popularity { get; set; }
        public int? Members { get; set; }
        public int? Favorites { get; set; }
        public string Synopsis { get; set; }
        public string Background { get; set; }
        public string Premiered { get; set; }
        public string Broadcast { get; set; }
        public RelatedAnime Related { get; set; }
        public ICollection<MALSubItem> Producers { get; set; }
        public ICollection<MALSubItem> Licensors { get; set; }
        public ICollection<MALSubItem> Studios { get; set; }
        public ICollection<MALSubItem> Genres { get; set; }
        [JsonProperty("opening_theme")]
        public ICollection<string> OpeningTheme { get; set; }
        [JsonProperty("ending_theme")]
        public ICollection<string> EndingTheme { get; set; } 
    }
}