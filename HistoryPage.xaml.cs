using MauiApp2.Models;
using KTI_Testing__Mobile_.Models;
using System.Collections.ObjectModel;

namespace MauiApp2
{
    public partial class HistoryPage : ContentPage
    {
        private ObservableCollection<HistoryDisplayItem> _items = new();

        public HistoryPage()
        {
            InitializeComponent();
            historyList.ItemsSource = _items;
            _ = LoadHistory();
        }

        private async Task LoadHistory()
        {
            List<HistoryObject> tools = await ToolRepository.userToolHistory();

            foreach (var h in tools)
            {
                Tool tool = ToolRepository.getSpecificTool(h.ToolId);

                bool returned = h.ReturnTime.Year != 1;

                _items.Add(new HistoryDisplayItem
                {
                    ToolName = tool.Name,
                    CheckoutText = $"Checkout: {h.CheckoutTime:MM/dd/yyyy}",

                    ReturnText = returned
                        ? $"Returned: {h.ReturnTime:MM/dd/yyyy}"
                        : "Not Returned",

                    ReturnColor = returned
                        ? Color.FromArgb("#FFCA26")
                        : Color.FromArgb("#B00020")
                });
            }
        }

        private async void ProfileButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }
    }


    public class HistoryDisplayItem
    {
        public string ToolName { get; set; } = string.Empty;
        public string CheckoutText { get; set; } = string.Empty;
        public string ReturnText { get; set; } = string.Empty;
        public Color ReturnColor { get; set; }
    }
}
