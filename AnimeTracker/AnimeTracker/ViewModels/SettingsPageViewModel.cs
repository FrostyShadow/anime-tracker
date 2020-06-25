using AnimeTracker.Interfaces;
using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly IDatabaseHelper _databaseHelper;

        public SettingsPageViewModel(INavigationService navigationService, IDatabaseHelper databaseHelper) : base(navigationService)
        {
            _databaseHelper = databaseHelper;
        }
    }
}