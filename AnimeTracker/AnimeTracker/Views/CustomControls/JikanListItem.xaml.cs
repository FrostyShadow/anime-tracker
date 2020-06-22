using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeTracker.Views.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JikanListItem : ViewCell
    {
        public static readonly BindableProperty ItemImageProperty = BindableProperty.Create(
            "ItemImage", typeof(ImageSource), typeof(JikanListItem));

        public static readonly BindableProperty ItemTitleProperty = BindableProperty.Create(
            "ItemTitle", typeof(string), typeof(JikanListItem));

        public static readonly BindableProperty ItemYearProperty = BindableProperty.Create(
            "ItemYear", typeof(string), typeof(JikanListItem));

        public static readonly BindableProperty ItemTypeProperty = BindableProperty.Create(
            "ItemType", typeof(string), typeof(JikanListItem));

        public static readonly BindableProperty ItemScoreProperty = BindableProperty.Create(
            "ItemScore", typeof(float), typeof(JikanListItem));

        public JikanListItem()
        {
            InitializeComponent();
        }

        public ImageSource ItemImage
        {
            get => (ImageSource) GetValue(ItemImageProperty);
            set => SetValue(ItemImageProperty, value);
        }

        public string ItemTitle
        {
            get => (string) GetValue(ItemTitleProperty);
            set => SetValue(ItemTitleProperty, value);
        }

        public string ItemYear
        {
            get => (string) GetValue(ItemYearProperty);
            set => SetValue(ItemYearProperty, value);
        }

        public string ItemType
        {
            get => (string) GetValue(ItemTypeProperty);
            set => SetValue(ItemTypeProperty, value);
        }

        public float ItemScore
        {
            get => (float) GetValue(ItemScoreProperty);
            set => SetValue(ItemScoreProperty, value);
        }
    }
}