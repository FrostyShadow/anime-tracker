using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeTracker.Models;
using JikanDotNet;
using Newtonsoft.Json;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;

namespace AnimeTracker.ViewModels
{
    public class AnimeListPageViewModel : ViewModelBase
    {
        private readonly IJikan _jikan;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand<object> FetchAnimeListCommand { get; }
        public DelegateCommand<object> GoToDetailsCommand { get; }

        private ICollection<AnimeSubEntry> _animeList;
        private ICollection<int> _yearsList;
        private ICollection<SeasonItem> _seasonsList;
        private int _selectedYear;
        private SeasonItem _selectedSeason;

        private bool _isDownloading;

        public ICollection<AnimeSubEntry> AnimeList
        {
            get => _animeList;
            set => SetProperty(ref _animeList, value);
        }

        public ICollection<int> YearsList
        {
            get => _yearsList;
            set => SetProperty(ref _yearsList, value);
        }

        public ICollection<SeasonItem> SeasonsList
        {
            get => _seasonsList;
            set => SetProperty(ref _seasonsList, value);
        }

        public int SelectedYear
        {
            get => _selectedYear;
            set => SetProperty(ref _selectedYear, value);
        }

        public SeasonItem SelectedSeason
        {
            get => _selectedSeason;
            set => SetProperty(ref _selectedSeason, value);
        }

        public bool IsDownloading
        {
            get => _isDownloading;
            set => SetProperty(ref _isDownloading, value);
        }

        public AnimeListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IJikan jikan) : base(navigationService)
        {
            _jikan = jikan;
            _dialogService = dialogService;
            FetchAnimeListCommand = new DelegateCommand<object>(FetchAnimeList);
            GoToDetailsCommand = new DelegateCommand<object>(GoToDetails);
            Title = "Anime List";
            YearsList = new List<int>();
            for (var i = DateTime.Today.Year; i >= 1975; i--)
            {
                YearsList.Add(i);
            }
            SeasonsList = new List<SeasonItem>
            {
                new SeasonItem
                {
                    DisplayName = "Spring",
                    Seasons = Seasons.Spring
                },
                new SeasonItem
                {
                    DisplayName = "Summer",
                    Seasons = Seasons.Summer
                },
                new SeasonItem
                {
                    DisplayName = "Fall",
                    Seasons = Seasons.Fall
                },
                new SeasonItem
                {
                    DisplayName = "Winter",
                    Seasons = Seasons.Winter
                }
            };
        }

        public override void Initialize(INavigationParameters parameters)
        {
            LoadAnimeListOnStartup();
        }

        private async void GoToDetails(object parameter)
        {
            var animeSubEntry = (AnimeSubEntry) parameter;
            var navigationParams = new NavigationParameters {{"malId", animeSubEntry.MalId}};
            await NavigationService.NavigateAsync("AnimeMoreInfoPage", navigationParams);
        }

        private async void LoadAnimeListOnStartup()
        {
            var today = DateTime.Today;
            Seasons season;
            switch (today.Month)
            {
                case 1:
                case 2:
                case 3:
                    season = Seasons.Winter;
                    break;
                case 4:
                case 5:
                case 6:
                    season = Seasons.Spring;
                    break;
                case 7:
                case 8:
                case 9:
                    season = Seasons.Summer;
                    break;
                case 10:
                case 11:
                case 12:
                    season = Seasons.Fall;
                    break;
                default:
                    season = Seasons.Winter;
                    break;
            }

            await GetAnimeListBySeasonAsync(today.Year, season);
        }

        private async void FetchAnimeList(object parameter)
        {
            if (SelectedYear == 0 && SelectedSeason == null)
            {
                await _dialogService.DisplayAlertAsync("Info", "Select search parameters first!", "OK");
                return;
            }

            AnimeList = null;
            await GetAnimeListBySeasonAsync(SelectedYear, SelectedSeason.Seasons);
        }

        private async Task GetAnimeListBySeasonAsync(int year, Seasons seasons)
        {
            try
            {
                IsDownloading = true;
                var season = await _jikan.GetSeason(year, seasons);
                AnimeList = season.SeasonEntries;
                IsDownloading = false;
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Error", "Problem while sending the request", "OK");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
