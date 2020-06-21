using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class AnimeListPageViewModel : ViewModelBase
    {
        public AnimeListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Anime List";
        }
    }
}
