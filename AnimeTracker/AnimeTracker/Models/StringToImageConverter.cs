using System;
using System.Globalization;
using Xamarin.Forms;

namespace AnimeTracker.Models
{
    public class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string s)) throw new InvalidCastException();
            var imageSource = new UriImageSource {Uri = new Uri(s)};
            return imageSource;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UriImageSource u)
            {
                return u.Uri.ToString();
            }

            throw new InvalidCastException();
        }
    }
}