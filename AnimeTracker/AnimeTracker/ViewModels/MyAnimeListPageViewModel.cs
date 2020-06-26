using System.Collections.Generic;
using System.Linq;
using AnimeTracker.Interfaces;
using AnimeTracker.Models;
using JikanDotNet;
using Prism.Commands;
using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class MyAnimeListPageViewModel : ViewModelBase
    {
        private readonly IDatabaseHelper _databaseHelper;
        private ICollection<AnimeListEntry> _list;

        public DelegateCommand<object> GoToDetailsCommand { get; }
        public DelegateCommand<object> RefreshCommand { get; }

        private ICollection<AnimeListEntry> _myList;
        private ICollection<WatchingStatus> _watchingStatuses;
        private WatchingStatus _watchingStatus;

        private bool _isRefreshing;

        public ICollection<AnimeListEntry> MyList
        {
            get => _myList;
            set => SetProperty(ref _myList, value);
        }

        public ICollection<WatchingStatus> WatchingStatuses
        {
            get => _watchingStatuses;
            set => SetProperty(ref _watchingStatuses, value);
        }

        public WatchingStatus WatchingStatus
        {
            get => _watchingStatus;
            set
            {
                SetProperty(ref _watchingStatus, value);
                SetFilter();
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public MyAnimeListPageViewModel(INavigationService navigationService, IDatabaseHelper databaseHelper) : base(navigationService)
        {
            Title = "My list";
            _databaseHelper = databaseHelper;

            GoToDetailsCommand = new DelegateCommand<object>(GoToDetails);
            RefreshCommand = new DelegateCommand<object>(Refresh);

            WatchingStatuses = new List<WatchingStatus>
            {
                new WatchingStatus
                {
                    DisplayName = "All",
                    UserAnimeListExtension = UserAnimeListExtension.All
                },
                new WatchingStatus
                {
                    DisplayName = "Watching",
                    UserAnimeListExtension = UserAnimeListExtension.Watching
                },
                new WatchingStatus
                {
                    DisplayName = "On Hold",
                    UserAnimeListExtension = UserAnimeListExtension.OnHold
                },
                new WatchingStatus
                {
                    DisplayName = "Completed",
                    UserAnimeListExtension = UserAnimeListExtension.Completed
                },
                new WatchingStatus
                {
                    DisplayName = "Dropped",
                    UserAnimeListExtension = UserAnimeListExtension.Dropped
                },
                new WatchingStatus
                {
                    DisplayName = "Plan To Watch",
                    UserAnimeListExtension = UserAnimeListExtension.PlanToWatch
                }
            };
        }

        private void SetFilter()
        {
            if (WatchingStatus.UserAnimeListExtension == UserAnimeListExtension.All)
            {
                MyList = _list;
                return;
            }
            MyList = _list.Where(l => l.WatchingStatus == _watchingStatus.UserAnimeListExtension).ToList();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            GetAnime();
        }

        private async void GetAnime()
        {
            _list = (await _databaseHelper.GetUserListAsync()).OrderByDescending(l => l.WatchingStatus)
                .ThenByDescending(l => l.WatchStartDate)
                .ThenBy(l => l.Title).ToList();
            MyList = _list.ToList();
        }

        private void Refresh(object parameter)
        {
            IsRefreshing = true;
            GetAnime();
            IsRefreshing = false;
        }

        private async void GoToDetails(object parameter)
        {
            var animeSubEntry = (AnimeListEntry)parameter;
            var navigationParams = new NavigationParameters { { "malId", animeSubEntry.MalId } };
            await NavigationService.NavigateAsync("AnimeMoreInfoPage", navigationParams);
        }
    }
}