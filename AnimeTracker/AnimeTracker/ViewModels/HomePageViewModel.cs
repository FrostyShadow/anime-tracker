using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Home";
        }
    }
}