using CommunityToolkit.Mvvm.ComponentModel;
using KTI_Testing__Mobile_.Resources.viewModels;

namespace MauiApp2;

public partial class MauiLoginPage : ContentPage
{
    [ObservableProperty]
    #pragma warning disable MVVMTK0019 // Invalid containing type for [ObservableProperty] field
    public bool isRememberMeChecked;
    #pragma warning restore MVVMTK0019 // Invalid containing type for [ObservableProperty] field

    public MauiLoginPage(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
		this.BindingContext = loginPageViewModel;
        /*if ()
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, "storedUserData.json");
            App.UserInfo = new KTI_Testing__Mobile_.Models.UserInfo();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                // Example usage
                JObject data = JObject.Parse(json);


            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }*/
	}
    private void OnRememberMeLabelTapped(object sender, EventArgs e)
    {
        RememberMeCheckbox.IsChecked = !RememberMeCheckbox.IsChecked;
    }


}