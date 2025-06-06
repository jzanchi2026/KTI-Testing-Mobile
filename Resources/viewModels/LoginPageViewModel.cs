using KTI_Testing__Mobile_.Models;
using KTI_Testing__Mobile_.NewFolder;
using KTI_Testing__Mobile_.NewFolder1;
using MauiApp2;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace KTI_Testing__Mobile_.Resources.viewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        private UserInfo userInfo;

        private readonly MauiLoginPage _mauiLoginPage;

        readonly ILoginRepos loginRepos = new LoginServices();

        [ICommand]

        public async void Login()
        {
            //Yongle
            //https://www.bing.com/search?pglt=297&q=yongle&cvid=5b2d059acf4c45ca8bcf5571c5701f17&gs_lcrp=EgRlZGdlKgkIABBFGDsY-QcyCQgAEEUYOxj5BzIGCAEQABhAMgYIAhBFGDsyBggDEEUYOTIGCAQQLhhAMgYIBRAuGEAyBggGEEUYPDIGCAcQRRg8MgYICBBFGDzSAQgxMzE1ajBqMagCALACAA&FORM=ANNTA1&PC=HCTS
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrWhiteSpace(Password))
            {
                userInfo = await loginRepos.Login(UserName, Password);
                
                if (Preferences.ContainsKey(nameof(App.UserInfo)))
                {
                    Preferences.Remove(nameof(App.UserInfo));
                }

                string userDetails = JsonConvert.SerializeObject(userInfo);
                
                Preferences.Set("UserInfo", userDetails);

                App.UserInfo = userInfo;
                if (userInfo.Error == null)
                {   
                    string fileName = "storedUserData.json";
                    string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                    // Build the data object
                    var storedUserData = new
                    {
                        Name = userInfo.Name,
                        Email = userInfo.Email,
                        UserId = userInfo.UserId
                    };

                    // Serialize and write to the correct location
                    string json = JsonConvert.SerializeObject(storedUserData, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                    await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    // throw error
                    Console.WriteLine(userInfo.Error);
                    Preferences.Remove(nameof(App.UserInfo));
                    await App.Current.MainPage.DisplayAlert("KTI Inventory", userInfo.Error, "Ok");
                }              
            }
            else
            {
                Console.WriteLine("EMPTYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");
            }
        }
    }
}
