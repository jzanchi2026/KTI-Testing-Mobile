using MauiApp2;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTI_Testing__Mobile_.Resources.viewModels
{
    public partial class AppShellViewMode : BaseViewModel
    {
        [ICommand]

        async void SignOut()
        {
            if (Preferences.ContainsKey("UserInfo")) 
            {
                Preferences.Remove(nameof(App.UserInfo));
                Uri loginUri = new Uri(App.uri, "logout");
                var shell = Shell.Current as AppShell;
                
                var response = await App.myHttpClient.GetAsync(loginUri.ToString());
            }
            await Shell.Current.GoToAsync("//MauiLoginPage");
        }
    }
}
