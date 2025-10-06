namespace MauiApp2;

using System;
using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;
using MauiApp2.Models;

public partial class ToolInfo : ContentPage
{
    public ToolInfo()
    {
        InitializeComponent();
        mainText.Text = "Invalid";

    }
    private string _passedValue;
    public ToolInfo(Tool scannedTool, string action)
    {
        InitializeComponent();
        _passedValue = scannedTool.Name;
        mainText.Text = _passedValue;
        if (action == "checkout")
        {
            ToolRepository.checkoutTool(scannedTool);
        }
        else if (action == "return")
        {
            ToolRepository.returnTool(scannedTool);
        }
    }

}