using System.Windows.Input;
using Xamarin.Forms;

namespace AnimeTracker.Models
{
    public class ListViewExtended : ListView
    {
        public static BindableProperty ItemClickCommandProperty =
            BindableProperty.Create(nameof(ItemClickCommand), typeof(ICommand), typeof(ListViewExtended));

        public ICommand ItemClickCommand
        {
            get => (ICommand) GetValue(ItemClickCommandProperty);
            set => SetValue(ItemClickCommandProperty, value);
        }

        public ListViewExtended()
        {
            ItemTapped += OnItemTapped;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            ItemClickCommand?.Execute(e.Item);
            SelectedItem = null;
        }
    }
}