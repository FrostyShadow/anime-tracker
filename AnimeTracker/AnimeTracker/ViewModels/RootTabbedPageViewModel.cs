using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimeTracker.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace AnimeTracker.ViewModels
{
    public class RootTabbedPageViewModel : ViewModelBase
    {
        public RootTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }
    }
}
