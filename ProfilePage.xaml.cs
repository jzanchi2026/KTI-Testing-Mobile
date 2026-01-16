using System.Collections.ObjectModel;
using System.Windows.Input;
using KTI_Testing__Mobile_.Models;
using MauiApp2.Models;
using Microsoft.Maui.Controls;

namespace MauiApp2
{
    public partial class ProfilePage : ContentPage
    {
        public string na { get; set; }
        public string em { get; set; }
        public ICommand SignOutCommand { get; }

        private ObservableCollection<ProfileDisplayItem> _currentItems = new();
        private ObservableCollection<ProfileDisplayItem> _historyItems = new();

        public ObservableCollection<ProfileDisplayItem> CurrentItems => _currentItems;
        public ObservableCollection<ProfileDisplayItem> HistoryItems => _historyItems;

        public ProfilePage()
        {
            InitializeComponent();

            na = "Welcome " + (App.UserInfo?.Name ?? "Guest") + "!";
            em = "Email: " + (App.UserInfo?.Email ?? "N/A");

            SignOutCommand = new Command(async () => await SignOutAsync());

            BindingContext = this;

            _ = LoadProfileItems();
        }

        private async Task LoadProfileItems()
        {
            List<HistoryObject> tools = await ToolRepository.userToolHistory();
            List<HistoryObject> mats = await MaterialRepository.userMaterialHistory();

            foreach (HistoryObject h in tools.Concat(mats))
            {
                bool returned = h.ReturnTime.Year != 1;

                if (!returned)
                {
                    _currentItems.Add(new ProfileDisplayItem
                    {
                        Icon = "hammor.png"
                    });
                }
                else if (_historyItems.Count < 5)
                {
                    _historyItems.Add(new ProfileDisplayItem
                    {
                        Icon = "hammor.png"
                    });
                }
            }
        }

        private async Task SignOutAsync()
        {
            if (Preferences.ContainsKey(nameof(App.UserInfo)))
                Preferences.Remove(nameof(App.UserInfo));

            Uri loginUri = new Uri(App.uri, "logout");
            await App.myHttpClient.GetAsync(loginUri.ToString());

            await Shell.Current.GoToAsync("//MauiLoginPage");
        }

        private async void SeeHistoryTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(HistoryPage));
        }

        private async void SeeInventoryTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Inventory));
        }

        public class ProfileDisplayItem
        {
            public string Icon { get; set; } = string.Empty;
        }
    }
}
