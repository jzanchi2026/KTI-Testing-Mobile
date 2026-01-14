namespace MauiApp2;

using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;
using MauiApp2.Models;

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
    /*
     * SEE STATEMENT IN .xaml FILE
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
    */
    private async void ViewHistory(object sender, EventArgs e)
    {

        string id = HistoryId.Text;
        Tool tool = await ToolRepository.parseTool(int.Parse(id));
        await Navigation.PushAsync(new ToolSpecificHistory(tool));
    }
    private async void ViewMHistory(object sender, EventArgs e)
    {

        string id = HistoryId.Text;
        Material mat = await MaterialRepository.parseMaterial(int.Parse(id));
        await Navigation.PushAsync(new MaterialSpecificHistory(mat));
    }
}
//Bro this is meat