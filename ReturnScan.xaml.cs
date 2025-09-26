namespace MauiApp2;

using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;

[QueryProperty("addedTool", "addedTool")]
public partial class ReturnScan : ContentPage
{
    public ReturnScan()
    {
        InitializeComponent();
    }

    public ReturnScan(Tool t)
    {

    }
}