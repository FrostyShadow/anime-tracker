using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JikanDotNet;
using Prism.Logging;
using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class AnimeMoreInfoPageViewModel : ViewModelBase
    {
        private int _malId;
        private readonly IJikan _jikan;

        private string _titleEnglish;
        private string _imageUrl;
        private int? _year;
        private string _type;
        private string _episodesString;
        private string _rank;

        private string _synopsis;
        private ICollection<CharacterEntry> _characters;

        private string _titleRomaji;
        private string _titleJapanese;
        private string _synonymString;

        private string _episodes;
        private string _duration;
        private string _source;
        private string _status;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _season;

        private string _studiosString;
        private string _producersString;

        public string TitleEnglish
        {
            get => _titleEnglish;
            set => SetProperty(ref _titleEnglish, value);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public int? Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public string EpisodesString
        {
            get => _episodesString;
            set => SetProperty(ref _episodesString, value);
        }

        public string Rank
        {
            get => _rank;
            set => SetProperty(ref _rank, value);
        }

        public string Synopsis
        {
            get => _synopsis;
            set => SetProperty(ref _synopsis, value);
        }

        public ICollection<CharacterEntry> Characters
        {
            get => _characters;
            set => SetProperty(ref _characters, value);
        }

        public string TitleRomaji
        {
            get => _titleRomaji;
            set => SetProperty(ref _titleRomaji, value);
        }

        public string TitleJapanese
        {
            get => _titleJapanese;
            set => SetProperty(ref _titleJapanese, value);
        }

        public string SynonymString
        {
            get => _synonymString;
            set => SetProperty(ref _synonymString, value);
        }

        public string Episodes
        {
            get => _episodes;
            set => SetProperty(ref _episodes, value);
        }

        public string Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        public string Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public string Season
        {
            get => _season;
            set => SetProperty(ref _season, value);
        }

        public string StudiosString
        {
            get => _studiosString;
            set => SetProperty(ref _studiosString, value);
        }

        public string ProducersString
        {
            get => _producersString;
            set => SetProperty(ref _producersString, value);
        }

        public AnimeMoreInfoPageViewModel(INavigationService navigationService, IJikan jikan) : base(navigationService)
        {
            _jikan = jikan;
            Title = "Anime overview";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _malId = parameters.GetValue<int>("malId");
            GetAnimeMoreInfoAsync();
        }

        //TODO: Add expandable synopsis
        private async void GetAnimeMoreInfoAsync()
        {
            try
            {
                var anime = await _jikan.GetAnime(_malId);
                var characters = await _jikan.GetAnimeCharactersStaff(_malId);
                var stringBuilder = new StringBuilder();

                TitleRomaji = anime.Title;
                TitleEnglish = anime.TitleEnglish;
                TitleJapanese = anime.TitleJapanese;
                foreach (var synonym in anime.TitleSynonyms)
                {
                    stringBuilder.AppendLine(synonym);
                }
                SynonymString = stringBuilder.ToString();
                Year = anime.Aired.From.GetValueOrDefault().Year;
                ImageUrl = anime.ImageURL;
                Type = anime.Type;
                EpisodesString = $"{anime.Episodes} Episodes";
                Rank = $"Rank #{anime.Rank}";
                Synopsis = anime.Synopsis;
                Characters = characters.Characters;

                Episodes = anime.Episodes;
                Duration = anime.Duration;
                Source = anime.Source;
                Status = anime.Status;
                StartDate = anime.Aired.From;
                EndDate = anime.Aired.To;
                Season = anime.Premiered;

                stringBuilder.Clear();
                foreach (var studio in anime.Studios)
                {
                    stringBuilder.AppendLine(studio.Name);
                }

                StudiosString = stringBuilder.ToString();
                stringBuilder.Clear();
                foreach (var producer in anime.Producers)
                {
                    stringBuilder.AppendLine(producer.Name);
                }

                ProducersString = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
