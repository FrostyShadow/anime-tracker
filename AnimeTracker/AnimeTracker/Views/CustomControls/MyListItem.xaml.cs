using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeTracker.Views.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyListItem : ViewCell
    {
        public MyListItem()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemImageProperty = BindableProperty.Create(
            "ItemImage", typeof(ImageSource), typeof(JikanListItem));

        public static readonly BindableProperty ItemTitleProperty = BindableProperty.Create(
            "ItemTitle", typeof(string), typeof(JikanListItem));

        public static readonly BindableProperty ItemWatchedEpisodesProperty = BindableProperty.Create(
            "ItemWatchedEpisodes", typeof(string), typeof(JikanListItem));

        public static readonly BindableProperty ItemTotalEpisodesProperty = BindableProperty.Create(
            "ItemTotalEpisodes", typeof(string), typeof(JikanListItem));

        public static readonly BindableProperty ItemYearProperty = BindableProperty.Create(
            "ItemYear", typeof(string), typeof(JikanListItem));

        public static readonly BindableProperty ItemTypeProperty = BindableProperty.Create(
            "ItemType", typeof(string), typeof(JikanListItem));

        public static readonly BindableProperty ItemScoreProperty = BindableProperty.Create(
            "ItemScore", typeof(float), typeof(JikanListItem));

        public static readonly BindableProperty ItemWatchingStatusProperty = BindableProperty.Create(
            "ItemWatchingStatus", typeof(string), typeof(JikanListItem));

        public ImageSource ItemImage
        {
            get => (ImageSource)GetValue(ItemImageProperty);
            set => SetValue(ItemImageProperty, value);
        }

        public string ItemTitle
        {
            get => (string)GetValue(ItemTitleProperty);
            set => SetValue(ItemTitleProperty, value);
        }

        public string ItemWatchedEpisodes
        {
            get => (string)GetValue(ItemWatchedEpisodesProperty);
            set => SetValue(ItemWatchedEpisodesProperty, value);
        }

        public string ItemTotalEpisodes
        {
            get => (string)GetValue(ItemTotalEpisodesProperty);
            set => SetValue(ItemTotalEpisodesProperty, value);
        }

        public string ItemYear
        {
            get => (string)GetValue(ItemYearProperty);
            set => SetValue(ItemYearProperty, value);
        }

        public string ItemType
        {
            get => (string)GetValue(ItemTypeProperty);
            set => SetValue(ItemTypeProperty, value);
        }

        public float ItemScore
        {
            get => (float)GetValue(ItemScoreProperty);
            set => SetValue(ItemScoreProperty, value);
        }

        public string ItemWatchingStatus
        {
            get => (string)GetValue(ItemWatchingStatusProperty);
            set => SetValue(ItemWatchingStatusProperty, value);
        }
    }
}