namespace MauiApp2;

using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;

public partial class AdminPage : ContentPage
{
    public AdminPage()
    {
        InitializeComponent();
    }

    private async void broccoli(object sender, EventArgs e)
    {
        await DisplayAlert("Broccoli", "", "OK");
    }
    private async void AddTool(object sender, EventArgs e)
    {
        string toolName = ToolNameEntry.Text;

        if (string.IsNullOrWhiteSpace(toolName))
        {
            DisplayAlert("Error", "Please enter a tool name.", "OK");
            return;
        }
        Uri checkUri = new Uri($"{App.uri}createTool?name={toolName}");
        // Treat like a GET although it is a POST
        var response = await App.myHttpClient.PostAsync(checkUri, null);
        var stringContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine(stringContent);
        // Use the tool name
        DisplayAlert("Added", $"Tool '{toolName}' added!", "OK");
    }

}
//Bro this is meat