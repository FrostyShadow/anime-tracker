using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class MangaListPageViewModel : ViewModelBase
    {
        public MangaListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
