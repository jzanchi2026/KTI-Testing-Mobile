namespace MauiApp2;

using System;
using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;
using MauiApp2.Models;

[QueryProperty(nameof(ToolId), "Id")]
public partial class ToolInfo : ContentPage
{
    private string toolId;
    public string ToolId
    {
        get => toolId;
        set
        {
            toolId = value;
            LoadToolInfo(toolId);
        }
    }

    public ToolInfo()
    {
        InitializeComponent();
        mainText.Text = "Loading...";
    }

    int quantity = 0;

    private void OnIncrementClicked(object sender, EventArgs e)
    {
        quantity++;
        quantityLabel.Text = quantity.ToString();
    }

    private void OnDecrementClicked(object sender, EventArgs e)
    {
        if (quantity > 0)
        {
            quantity--;
            quantityLabel.Text = quantity.ToString();
        }
    }

    private async void OnScanCheckoutClicked(object sender, EventArgs e)
    {
        // implement later
    }

    private async void OnReturnClicked(object sender, EventArgs e)
    {
        // implement later
    }

    // Temporary function that puts random img in
    private string ChooseRandomImg()
    {
        string[] imageNames = new string[]
        {
            "hammor.png",
            "gears.png",
            "ruler.png",
            "wrench.png",
            "wire_cutter.png"
        };

        Random random = new Random();
        int index = random.Next(imageNames.Length);
        return imageNames[index];
    }

    private void LoadToolInfo(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                mainText.Text = "Invalid tool ID";
                return;
            }

            var tool = ToolRepository.GetTools().FirstOrDefault(t => t.Id.ToString() == id);

            if (tool != null)
            {
                mainText.Text = tool.Name;
                availableCount.Text = tool.Amount.ToString() + " available";
                /*if (tool.Icon.Trim() != "")
                    toolImage.Source = tool.Icon;
                else*/
                    toolImage.Source = ChooseRandomImg();
            }
            else
            {
                mainText.Text = "Tool not found";
            }
        }
        catch (Exception ex)
        {
            mainText.Text = $"Error: {ex.Message}";
        }
    }
}
