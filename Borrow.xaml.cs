namespace MauiApp2;
using KTI_Testing__Mobile_.Models;
using KTI_Testing__Mobile_.Resources.viewModels;
using MauiApp2.Models;
using System.Windows.Input;

public partial class Borrow : ContentPage
{
    public string Prefix { get; set; }
    public Borrow()
    {
        InitializeComponent();
        Prefix = "";
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
            string truncated = "";
            Prefix = "";
            string action = "";
            foreach (char character in barcodeValue)
            {
                truncated += int.TryParse(character.ToString(), out int j) ? character : "";
                Prefix += !int.TryParse(character.ToString(), out int k) ? character : "";
            }

            if (int.TryParse(truncated, out int result))
            {
                tool = ToolRepository.getSpecificTool(result);
            }
            else
            {
                tool = new Tool(-1, "invalid", "DNE");
            }

            Console.WriteLine($"DEBUG Prefix = '{Prefix}'");
            ScannedTool = tool;
            if (Prefix == "KTM_")
            {
                barcodeValue = "";
                await Navigation.PushAsync(new ToolInfo(ScannedTool, "scan"));
                //barcodeResult.Text = $"Are you sure you want to check out:\n{ScannedTool.Name}";
            }
            else
            {
                barcodeResult.Text = "invalid";

            }
            //Navigation.PushAsync(new CartPage(myTool));
        });
    }

    private async void addToCartPage(object sender, EventArgs e)
    {
        if (Prefix == "KTM_")
        {
            await Navigation.PushAsync(new ToolInfo(ScannedTool, "checkout"));
        }
        else if (Prefix == "RKTM_")
        {
            await Navigation.PushAsync(new ToolInfo(ScannedTool, "return"));
        }
        Confirm.IsVisible = false;
        barcodeResult.Text = "";
        await cameraView.StartCameraAsync();
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