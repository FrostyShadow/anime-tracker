using System;
using JikanDotNet;
using Newtonsoft.Json;
using SQLite;

namespace AnimeTracker.Models
{
    public class AnimeListEntity
    {
        public AnimeListEntity()
        {
        }

        public AnimeListEntity(AnimeListEntry animeListEntry)
        {
            MalId = animeListEntry.MalId;
            AiringStatus = JsonConvert.SerializeObject(animeListEntry.AiringStatus);
            Days = animeListEntry.Days;
            EndDate = animeListEntry.EndDate;
            HasEpisodeVideo = animeListEntry.HasEpisodeVideo;
            HasPromoVideo = animeListEntry.HasPromoVideo;
            HasVideo = animeListEntry.HasVideo;
            ImageUrl = animeListEntry.ImageURL;
            IsRewatching = animeListEntry.IsRewatching;
            Priority = animeListEntry.Priority;
            Rating = animeListEntry.Rating;
            Score = animeListEntry.Score;
            StartDate = animeListEntry.StartDate;
            Title = animeListEntry.Title;
            TotalEpisodes = animeListEntry.TotalEpisodes;
            Type = animeListEntry.Type;
            Url = animeListEntry.URL;
            VideoUrl = animeListEntry.VideoUrl;
            WatchedEpisodes = animeListEntry.WatchedEpisodes;
            WatchEndDate = animeListEntry.WatchEndDate;
            WatchingStatus = JsonConvert.SerializeObject(animeListEntry.WatchingStatus);
            WatchStartDate = animeListEntry.WatchStartDate;
        }

        [PrimaryKey] public long MalId { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public int? TotalEpisodes { get; set; }
        public string VideoUrl { get; set; }
        public int Score { get; set; }
        public string Rating { get; set; }
        public string AiringStatus { get; set; }
        public int? Days { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? HasEpisodeVideo { get; set; }
        public bool? HasPromoVideo { get; set; }
        public bool? HasVideo { get; set; }
        public bool? IsRewatching { get; set; }
        public string Priority { get; set; }
        public DateTime? StartDate { get; set; }
        public string Type { get; set; }
        public int? WatchedEpisodes { get; set; }
        public DateTime? WatchEndDate { get; set; }
        public string WatchingStatus { get; set; }
        public DateTime? WatchStartDate { get; set; }
    }

    public static class AnimeListEntityExtensions
    {
        public static AnimeListEntry ToAnimeListEntry(this AnimeListEntity animeListEntity)
        {
            return new AnimeListEntry
            {
                AiringStatus = JsonConvert.DeserializeObject<AiringStatus>(animeListEntity.AiringStatus),
                Days = animeListEntity.Days,
                EndDate = animeListEntity.EndDate,
                HasEpisodeVideo = animeListEntity.HasEpisodeVideo,
                HasPromoVideo = animeListEntity.HasPromoVideo,
                HasVideo = animeListEntity.HasVideo,
                ImageURL = animeListEntity.ImageUrl,
                IsRewatching = animeListEntity.IsRewatching,
                MalId = animeListEntity.MalId,
                Priority = animeListEntity.Priority,
                Rating = animeListEntity.Rating,
                Score = animeListEntity.Score,
                StartDate = animeListEntity.StartDate,
                Title = animeListEntity.Title,
                TotalEpisodes = animeListEntity.TotalEpisodes,
                Type = animeListEntity.Type,
                URL = animeListEntity.Url,
                VideoUrl = animeListEntity.VideoUrl,
                WatchedEpisodes = animeListEntity.WatchedEpisodes,
                WatchEndDate = animeListEntity.WatchEndDate,
                WatchingStatus = JsonConvert.DeserializeObject<UserAnimeListExtension>(animeListEntity.WatchingStatus),
                WatchStartDate = animeListEntity.WatchStartDate
            };
        }
    }
}