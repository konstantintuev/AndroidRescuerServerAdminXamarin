using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ServeAdmin
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            Item give = item;
            if (!item.Text.Contains("From")) {
                give = item.SetText(item.Text.Contains("url") ? item.Text.Substring(item.Text.LastIndexOf(":", StringComparison.CurrentCulture) + 1) : item.Text.Substring(0, item.Text.IndexOf("ERR:", StringComparison.CurrentCulture)));
            }
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(give)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        public async void OnDelete(object sender, EventArgs e)
        {
			var mi = ((MenuItem)sender);
            Item item = mi.CommandParameter as Item;
            var res = await DisplayAlert("Delete this item?", item.Text, "OK", "Cancel");
            if (res) {
                await BaseViewModel.DataStore.DeleteItemAsync(item.Id);
                viewModel.Items.Remove(item);
            }
		}

        void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            CloudDataStore.error = e.Value;
            viewModel.LoadItemsCommand.Execute(null);
        }

        public void Support() {
            supportErr.IsToggled = false;
            CloudDataStore.error = false;
            viewModel.LoadItemsCommand.Execute(null);
        }

        public void Error()
        {
            supportErr.IsToggled = true;
            CloudDataStore.error = true;
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
