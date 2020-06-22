using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using JikanDotNet;
using Prism.Navigation;

namespace AnimeTracker.ViewModels
{
    public class AnimeMoreInfoPageViewModel : ViewModelBase
    {
        private int _malId;
        private readonly IJikan _jikan;

        public AnimeMoreInfoPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _jikan = new Jikan(true);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _malId = parameters.GetValue<int>("malId");
            Console.WriteLine(_malId);
        }
    }
}
