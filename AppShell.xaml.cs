using KTI_Testing__Mobile_;
using KTI_Testing__Mobile_.Resources.viewModels;
using MauiApp2;

namespace MauiApp2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            this.BindingContext = new AppShellViewMode();
            string getuserSavedKey = Preferences.Get("UserInfo", "empty");

            if (getuserSavedKey != "empty")
            {
                MyAppShell.CurrentItem = MyMainPage;
            }
            else
            {
                MyAppShell.CurrentItem = MyLoginPage;
            }
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
            Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(Temp), typeof(Temp));
            Routing.RegisterRoute(nameof(Inventory), typeof(Inventory));
        }
    }
}
