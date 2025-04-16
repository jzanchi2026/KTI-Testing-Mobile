using KTI_Testing__Mobile_.Models;
using KTI_Testing__Mobile_.Resources.viewModels;

namespace MauiApp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public string na { get; set; }
        public string em { get; set; }
        public ProfilePage()
        {
            InitializeComponent();
            na = "Welcome " + App.UserInfo.Name + "!";
            em = "Email: " + App.UserInfo.Email;
            BindingContext = this;
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