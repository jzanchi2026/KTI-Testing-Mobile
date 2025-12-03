/*namespace MauiApp2;

using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;
public partial class ReturnScan : ContentPage
{
    public ReturnScan()
    {
        InitializeComponent();
    }

    public ReturnScan(Tool t)
    {

    }
}*/
namespace MauiApp2;

using KTI_Testing__Mobile_.Models;
using KTI_Testing__Mobile_.Resources.viewModels;
using MauiApp2.Models;
using System.Windows.Input;

public partial class ReturnScan : ContentPage
{
    public string Prefix { get; set; }
    public ReturnScan()
    {
        InitializeComponent();

        RefreshView refreshView = new RefreshView();
        ICommand refreshCommand = new Command(() =>
        {
            // IsRefreshing is true
            // Refresh data here
            refreshView.IsRefreshing = false;
        });
        refreshView.Command = refreshCommand;
        this.BindingContext = new cartModel();
    }

    private Tool ScannedTool;
    private Material ScannedMat;
    private void cameraview_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                //Task.Delay(500);
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }

    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            //barcodeResult.Text = $"{args.Result[0].BarcodeFormat}: {args.Result[0].Text}";

            await cameraView.StopCameraAsync();
            string barcodeValue = args.Result[0].Text;
            Tool tool = null;
            Material mat = null;
            string truncated = "";
            Prefix = "";
            
            foreach (char character in barcodeValue)
            {
                truncated += int.TryParse(character.ToString(), out int j) ? character : "";
                Prefix += !int.TryParse(character.ToString(), out int k) ? character : "";
            }

            if (int.TryParse(truncated, out int result))
            {
                if (result < 1000)
                {
                    tool = ToolRepository.getSpecificTool(result);
                }
                if (result >= 1000)
                {
                    mat = MaterialRepository.getSpecificMaterial(result - 1000);
                }
            }
            else
            {
                tool = null;
                mat = null;
            }

            Console.WriteLine($"DEBUG Prefix = '{Prefix}'");
            ScannedTool = tool;
            ScannedMat = mat;
            if (Prefix == "RKTM_")
            {
                if (ScannedTool != null)
                {
                    barcodeResult.Text = $"Are you sure you want to return:\n{ScannedTool.Name}";
                    Confirm.IsVisible = true;
                    Confirm.Text = "Confirm";
                }
                else if (ScannedMat != null)
                {
                    barcodeResult.Text = $"Are you sure you want to return {ScannedMat.quantity}: \n{ScannedMat.name}s";
                    Confirm.IsVisible = true;
                    Confirm.Text = "Confirm";
                }
                else
                {
                    barcodeResult.Text = "Tool/Material does not exist";
                }
            }
            else if (Prefix == "KTM_")
            {
                barcodeResult.Text = "This is a checkout code";
            }
            else
            {
                barcodeResult.Text = "invalid";

            }
        });
    }
    private async void addToCartPage(object sender, EventArgs e)
    {
        ToolRepository.returnTool(ScannedTool);
        Confirm.IsVisible = false;
        barcodeResult.Text = "";

        await Shell.Current.GoToAsync("..");
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (cameraView.Camera == null && cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
        }

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                await cameraView.StartCameraAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start camera: {ex.Message}");
            }
        });
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                await cameraView.StopCameraAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to stop camera: {ex.Message}");
            }
        });
    }

}