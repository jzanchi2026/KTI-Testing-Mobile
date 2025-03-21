using KTI_Testing__Mobile_.Models;

namespace MauiApp2;
public partial class App : Application
{
        public static UserInfo UserInfo;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
}

