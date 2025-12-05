namespace MauiApp2;

using System;
using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;
using MauiApp2.Models;

[QueryProperty(nameof(MaterialId), "Id")]
public partial class MaterialInfo : ContentPage
{
    private string materialId;
    public string MaterialId
    {
        get => materialId;
        set
        {
            materialId = value;
            LoadToolInfo(materialId);
        }
    }

    public MaterialInfo()
    {
        InitializeComponent();
        mainText.Text = "Loading...";
    }

    private string _passedValue;
    private Material mat;
    private string action;
    public MaterialInfo(Material scannedMat, string a)
    {
        InitializeComponent();
        _passedValue = scannedMat.Name;
        mat = scannedMat;
        mainText.Text = _passedValue;
        action = a;
        availableCount.Text = scannedMat.CurrentAmount.ToString();
    }
    float quantity = 0;

    private void OnIncrementClicked(object sender, EventArgs e)
    {
        quantity += 0.1f;
        quantityLabel.Text = quantity.ToString("0.0");
    }

    private void OnDecrementClicked(object sender, EventArgs e)
    {
        if (quantity > 0)
        {
            quantity -= 0.1f;
            quantityLabel.Text = quantity.ToString("0.0");
        }
    }
    private void OnBigIncrementClicked(object sender, EventArgs e)
    {
        quantity++;
        quantityLabel.Text = quantity.ToString("0.0");
    }

    private void OnBigDecrementClicked(object sender, EventArgs e)
    {
        if (quantity > 1)
        {
            quantity--;
            quantityLabel.Text = quantity.ToString("0.0");
        }
        else
        {
            quantity = 0;
            quantityLabel.Text = quantity.ToString("0.0");
        }
    }

    private async void OnScanCheckoutClicked(object sender, EventArgs e)
    {
        if (mat != null)
        {
            bool a = await MaterialRepository.checkoutMaterial(mat, float.Parse(quantityLabel.Text));
            if (a)
            {
                await DisplayAlert("Success", "Material checked out successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to check out the tool.", "OK");
            }
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void OnReturnClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new ReturnScan());
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
            "wires_cutter.png"
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
                //availableCount.Text = tool.Amount.ToString() + " available";
                /*if (tool.Icon.Trim() != "")
                    toolImage.Source = tool.Icon;
                else*/
                matImage.Source = ChooseRandomImg();
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
