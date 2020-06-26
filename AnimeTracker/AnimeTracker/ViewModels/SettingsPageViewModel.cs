using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AnimeTracker.Interfaces;
using JikanDotNet;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace AnimeTracker.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IJikan _jikan;
        private readonly IPageDialogService _dialogService;

        private IDictionary<string, string> _userConfig;

        private string _userName;
        private DateTime? _lastUpdateDate;
        private bool _isAutoUpdateEnabled;

        private bool _isListUpdating;

        public DelegateCommand<object> UpdateMyListCommand { get; }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public DateTime? LastUpdateDate
        {
            get => _lastUpdateDate;
            set => SetProperty(ref _lastUpdateDate, value);
        }

        public bool IsAutoUpdateEnabled
        {
            get => _isAutoUpdateEnabled;
            set
            {
                SetProperty(ref _isAutoUpdateEnabled, value);
                SetAutoUpdate();
            }
        }

        public bool IsListUpdating
        {
            get => _isListUpdating;
            set => SetProperty(ref _isListUpdating, value);
        }

        public SettingsPageViewModel(INavigationService navigationService, IDatabaseHelper databaseHelper, IJikan jikan,
            IPageDialogService dialogService) : base(navigationService)
        {
            _databaseHelper = databaseHelper;
            _jikan = jikan;
            _dialogService = dialogService;
            Title = "Settings";
            UpdateMyListCommand = new DelegateCommand<object>(UpdateMyList);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            await GetSettings();
            if (IsAutoUpdateEnabled && !string.IsNullOrEmpty(UserName))
            {
                GetMyList();
            }
        }

        private async void GetMyList()
        {
            try
            {
                var animeList = await _jikan.GetUserAnimeList(UserName);
                if (animeList.Anime.Count == 0) return;
                var updateDate = DateTime.Now;
                LastUpdateDate = updateDate;
                await _databaseHelper.SaveUserListAsync(animeList.Anime.ToList());
                await _databaseHelper.SaveUserConfig(
                    new KeyValuePair<string, string>("LastListUpdateDate",
                        updateDate.ToString(CultureInfo.InvariantCulture)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void SetAutoUpdate()
        {
            await _databaseHelper.SaveUserConfig(new KeyValuePair<string, string>("IsAutomaticListUpdateEnabled", IsAutoUpdateEnabled.ToString()));
        }

        private async Task GetSettings()
        {
            _userConfig = await _databaseHelper.GetUserConfigListAsync();
            UserName = _userConfig.Where(c => c.Key == "UserName").Select(c => c.Value).FirstOrDefault();
            if(_userConfig.Where(c => c.Key == "LastListUpdateDate").Select(c => c.Value).FirstOrDefault() != null)
                LastUpdateDate = DateTime.Parse(_userConfig.Where(c => c.Key == "LastListUpdateDate").Select(c => c.Value).FirstOrDefault());
            IsAutoUpdateEnabled = bool.Parse(_userConfig.Where(c => c.Key == "IsAutomaticListUpdateEnabled").Select(c => c.Value)
                .FirstOrDefault() ?? string.Empty);
        }

        private async void UpdateMyList(object parameter)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                await _dialogService.DisplayAlertAsync("Info", "Enter username first", "OK");
                return;
            }

            try
            {
                IsListUpdating = true;
                if (UserName != _userConfig.Where(c => c.Key == "UserName").Select(c => c.Value).FirstOrDefault())
                {
                    await _databaseHelper.SaveUserConfig(new KeyValuePair<string, string>("UserName", UserName));
                    var animeList = await _jikan.GetUserAnimeList(UserName);
                    if (animeList.Anime.Count == 0)
                    {
                        await _dialogService.DisplayAlertAsync("Info", "Anime list is empty", "OK");
                        return;
                    }

                    var updateDate = DateTime.Now;
                    LastUpdateDate = updateDate;
                    await _databaseHelper.SaveUserListAsync(animeList.Anime.ToList(), true);
                    await _databaseHelper.SaveUserConfig(
                        new KeyValuePair<string, string>("LastListUpdateDate",
                            updateDate.ToString(CultureInfo.InvariantCulture)));
                }
                else
                {
                    var animeList = await _jikan.GetUserAnimeList(UserName);
                    if (animeList.Anime.Count == 0)
                    {
                        await _dialogService.DisplayAlertAsync("Info", "Anime list is empty", "OK");
                        return;
                    }

                    var updateDate = DateTime.Now;
                    LastUpdateDate = updateDate;
                    await _databaseHelper.SaveUserListAsync(animeList.Anime.ToList());
                    await _databaseHelper.SaveUserConfig(
                        new KeyValuePair<string, string>("LastListUpdateDate",
                            updateDate.ToString(CultureInfo.InvariantCulture)));
                }

                IsListUpdating = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}