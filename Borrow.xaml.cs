namespace MauiApp2;
using KTI_Testing__Mobile_.Models;
using KTI_Testing__Mobile_.Resources.viewModels;
using System.Windows.Input;

public partial class Borrow : ContentPage
{
	public Borrow()
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
            //
            ScannedTool = new Tool(500, "kitty", "pooh", 1);
            barcodeResult.Text = ScannedTool.Name;
            Confirm.Text = "Are you sure you want to check out a\n" + ScannedTool.Name + "?";
            Confirm.IsVisible = true;
            //Navigation.PushAsync(new CartPage(myTool));
        });
    }

    private async void addToCartPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CartPage));
        Confirm.IsVisible = false;
        barcodeResult.Text = "";
        await cameraView.StartCameraAsync();
    }
}