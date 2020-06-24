using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeTracker.Models;
using JikanDotNet;
using Prism.Navigation;
using Prism.Services;

namespace AnimeTracker.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private readonly IJikan _jikan;
        private readonly IPageDialogService _dialogService;

        private ICollection<AnimeSearchEntry> _resultsList;

        public DelegateCommand<object> SearchCommand { get; }
        public DelegateCommand<object> GoToDetailsCommand { get; }

        public ICollection<AnimeSearchEntry> ResultsList
        {
            get => _resultsList;
            set => SetProperty(ref _resultsList, value);
        }

        public SearchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            _jikan = new Jikan(true);
            _dialogService = dialogService;
            SearchCommand = new DelegateCommand<object>(Search);
            GoToDetailsCommand = new DelegateCommand<object>(GoToDetails);
        }

        private async void GoToDetails(object parameter)
        {
            var animeSubEntry = (AnimeSearchEntry)parameter;
            var navigationParams = new NavigationParameters {{"malId", animeSubEntry.MalId}};
            await NavigationService.NavigateAsync("AnimeMoreInfoPage", navigationParams);
        }

        private async void Search(object parameter)
        {
            ResultsList?.Clear();
            if (parameter == null)
            {
                await _dialogService.DisplayAlertAsync("Info", "Enter search query first!", "OK");
                return;
            }

            var query = (string) parameter;
            var results = await _jikan.SearchAnime(query);
            if (results.Results.Count == 0)
            {
                await _dialogService.DisplayAlertAsync("Info", "No results", "OK");
                return;
            }

            ResultsList = results.Results;
        }
    }
}
