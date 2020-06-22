using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet;
using Newtonsoft.Json;
using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class AnimeListPageViewModel : ViewModelBase
    {
        private readonly IJikan _jikan;

        public DelegateCommand<object> FetchAnimeListCommand { get; }
        public DelegateCommand<object> GoToDetailsCommand { get; }

        private ICollection<AnimeSubEntry> _animeList;

        public ICollection<AnimeSubEntry> AnimeList
        {
            get => _animeList;
            set => SetProperty(ref _animeList, value);
        }

        public AnimeListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _jikan = new Jikan(true);
            FetchAnimeListCommand = new DelegateCommand<object>(FetchAnimeList);
            GoToDetailsCommand = new DelegateCommand<object>(GoToDetails);
            Title = "Anime List";
        }

        private async void GoToDetails(object parameter)
        {
            var animeSubEntry = (AnimeSubEntry) parameter;
            var navigationParams = new NavigationParameters();
            navigationParams.Add("malId", animeSubEntry.MalId);
            await NavigationService.NavigateAsync("AnimeMoreInfoPage", navigationParams);
        }

        private async void FetchAnimeList(object parameter)
        {
            await GetAnimeListBySeasonAsync(DateTime.Today.Year, Seasons.Spring);
        }

        private async Task GetAnimeListBySeasonAsync(int year, Seasons seasons)
        {
            var season = await _jikan.GetSeason(year, seasons);
            AnimeList = season.SeasonEntries;
        }
    }
}
