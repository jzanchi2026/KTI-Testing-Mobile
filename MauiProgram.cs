using Camera.MAUI;
using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Resources.viewModels;
using MauiApp2;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace MauiApp2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("HighSchoolUsa.ttf", "HighSchoolUsa");
                });
            
            /*if (App.UserInfo == null)
            {

                string path = Path.Combine(FileSystem.AppDataDirectory, "storedUserData.json");
                App.UserInfo = new KTI_Testing__Mobile_.Models.UserInfo();
                if (File.Exists(path))
                    {
                    string json = File.ReadAllText(path);
                    // Example usage
                    JObject data = JObject.Parse(json);

                    App.UserInfo.UserId = data["UserId"].ToString();
                    App.UserInfo.Email = data["Email"].ToString(); // change this later, wait for db to update
                    App.UserInfo.Name = data["Name"].ToString();
                    
                }
                else
                {
                    Console.WriteLine("File not found.");
                }

            }*/
            builder.Logging.AddDebug();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MauiLoginPage>();
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<CartPage>();
            builder.Services.AddSingleton<cartModel>();
            builder.UseMauiCameraView();
            builder.UseMauiApp<App>().UseMauiCommunityToolkitMarkup();
            return builder.Build();
        }
    }
}