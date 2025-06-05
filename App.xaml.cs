using KTI_Testing__Mobile_.Models;
using System.Net;

namespace MauiApp2;
public partial class App : Application
{
    public static UserInfo UserInfo;
    public static CookieContainer cookieContainer = new CookieContainer();
    public static HttpClientHandler handler = new HttpClientHandler
    {
        CookieContainer = cookieContainer,
        UseCookies = true
    };
    public static Uri uri = new Uri("https://develmets.skiscratcher.com/");
    public static HttpClient myHttpClient = new HttpClient(App.handler);

    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}

