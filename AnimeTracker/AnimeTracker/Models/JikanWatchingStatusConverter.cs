using System;
using System.Globalization;
using JikanDotNet;
using Xamarin.Forms;

namespace AnimeTracker.Models
{
    public class JikanWatchingStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is UserAnimeListExtension usrExt)) throw new InvalidCastException();
            var progressString = usrExt switch
            {
                UserAnimeListExtension.Completed => "Completed",
                UserAnimeListExtension.Dropped => "Dropped",
                UserAnimeListExtension.OnHold => "On Hold",
                UserAnimeListExtension.PlanToWatch => "Plan To Watch",
                UserAnimeListExtension.Watching => "Watching",
                _ => string.Empty
            };

            return progressString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}