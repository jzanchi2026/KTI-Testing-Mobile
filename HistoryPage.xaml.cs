using System.Collections.ObjectModel;
using KTI_Testing__Mobile_.Models;
using MauiApp2.Models;

namespace MauiApp2
{
    public partial class HistoryPage : ContentPage
    {
        private ObservableCollection<HistoryDisplayItem> _items = new();

        public ObservableCollection<HistoryDisplayItem> Items => _items;

        public HistoryPage()
        {
            InitializeComponent();
            BindingContext = this;
            _ = grabTools();
        }

        public async Task grabTools()
        {
            List<HistoryObject> tools = await ToolRepository.userToolHistory();
            foreach (HistoryObject i in tools)
                addItem(i);

            List<HistoryObject> mats = await MaterialRepository.userMaterialHistory();
            foreach (HistoryObject i in mats)
                addItem(i);
        }

        public void addItem(HistoryObject h)
        {
            bool returned = h.ReturnTime.Year != 1;

            if (h.TakenQ != 0)
            {
                Material mat = MaterialRepository.getSpecificMaterial(h.Id);

                _items.Add(new HistoryDisplayItem 
                {
                    ToolName = mat.Name,
                    CheckoutText = $"Checkout: {h.CheckoutTime:MM/dd/yyyy}",
                    ReturnText = returned
                        ? $"Returned: {h.ReturnTime:MM/dd/yyyy}"
                        : "Not Returned",
                    ReturnColor = returned
                        ? Color.FromArgb("#FFCA26")
                        : Color.FromArgb("#B00020")
                });
            }
            else
            {
                Tool tool = ToolRepository.getSpecificTool(h.Id);

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

        public class HistoryDisplayItem
        {
            public string ToolName { get; set; } = string.Empty;
            public string CheckoutText { get; set; } = string.Empty;
            public string ReturnText { get; set; } = string.Empty;
            public Color ReturnColor { get; set; }
        }

        private async void ProfileButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }
    }
}
