using System.Windows.Input;
using KTI_Testing__Mobile_.Models;
using KTI_Testing__Mobile_.Resources.viewModels;
using Microsoft.Maui.Controls;

namespace MauiApp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public string na { get; set; }
        public string em { get; set; }
        public ICommand SignOutCommand { get; }
        public ProfilePage()
        {
            InitializeComponent();
            na = "Welcome " + App.UserInfo.Name + "!";
            em = "Email: " + App.UserInfo.Email;
            SignOutCommand = new Command(async () => await SignOutAsync());
            BindingContext = this;
        }
        private async System.Threading.Tasks.Task SignOutAsync()
        {
            try
            {
                // Remove saved user info
                Preferences.Remove(nameof(App.UserInfo));
                Uri loginUri = new Uri(App.uri, "logout");
                var shell = Shell.Current as AppShell;
                
                var response = await App.myHttpClient.GetAsync(loginUri.ToString());
                await Shell.Current.GoToAsync("//MauiLoginPage");
            }
            catch (Exception ex)
            {
                // Show helpful failure message during debugging
                await DisplayAlert("Sign out failed", ex.Message, "OK");
            }
        }
        private void GoToProfilePage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(ProfilePage));
        }

        private void GoToHistoryPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(HistoryPage));
        }

        private void GoToCartPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(CartPage));
        }

        private void GoToSettingPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(SettingsPage));
        }
    }
}