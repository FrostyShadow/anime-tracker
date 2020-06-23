using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using JikanDotNet;
using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class AnimeMoreInfoPageViewModel : ViewModelBase
    {
        private int _malId;
        private readonly IJikan _jikan;

        private string _titleEnglish;
        private string _imageURL;
        private int? _year;
        private string _type;
        private string _episodes;
        private string _rank;

        private string _synopsis;
        private string _titleRomaji;
        private string _titleJapanese;

        public string TitleEnglish
        {
            get => _titleEnglish;
            set => SetProperty(ref _titleEnglish, value);
        }

        public string ImageURL
        {
            get => _imageURL;
            set => SetProperty(ref _imageURL, value);
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

        public string Episodes
        {
            get => _episodes;
            set => SetProperty(ref _episodes, value);
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

        public AnimeMoreInfoPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _jikan = new Jikan(true);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _malId = parameters.GetValue<int>("malId");
            GetAnimeMoreInfoAsync();
        }

        //TODO: Add expandable synopsis
        private async void GetAnimeMoreInfoAsync()
        {
            var anime = await _jikan.GetAnime(_malId);

            TitleRomaji = anime.Title;
            TitleEnglish = anime.TitleEnglish;
            TitleJapanese = anime.TitleJapanese;
            Year = anime.Aired.From.GetValueOrDefault().Year;
            ImageURL = anime.ImageURL;
            Type = anime.Type;
            Episodes = $"{anime.Episodes} Episodes";
            Rank = $"Rank #{anime.Rank}";
            Synopsis = anime.Synopsis;
            
            
        }
    }
}
