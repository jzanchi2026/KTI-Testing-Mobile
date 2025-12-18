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
            List<HistoryObject> mats = await MaterialRepository.userMaterialHistory();

            foreach (HistoryObject i in mats)
            {
                addItem(i);
            }
        }

        private async void ProfileButton_Clicked(object sender, EventArgs e)
        {
            if (h.TakenQ != 0)
            {
                Material mat = MaterialRepository.getSpecificMaterial(h.Id);
                var myStyle = new Style<Button>(
                    (Button.HeightRequestProperty, 120),
                    (Button.MaximumWidthRequestProperty, 430),
                    (Button.TextColorProperty, Colors.Black),
                    (Button.BackgroundColorProperty, Colors.WhiteSmoke),
                    (Button.FontSizeProperty, 10)
                );
                string retTime = h.ReturnTime.Year == 1 ? "Not Returned" : h.ReturnTime.ToString();
                string chkTime = h.CheckoutTime.ToString();
                Button button = new Button
                {
                    Text = $"Material: {mat.Name}\nCheckout time: {chkTime}\nReturn time: {retTime}",
                    LineBreakMode = LineBreakMode.WordWrap,
                    Style = myStyle,
                    Margin = new Thickness(15, 15, 15, 0)
                };
                // ✅ Add a new row for this button before the footer row (3)
                int insertRow = toolList.RowDefinitions.Count - 1; // place before bottom buttons
                toolList.RowDefinitions.Insert(insertRow, new RowDefinition { Height = GridLength.Auto });
                Grid.SetRow(button, insertRow);
                Grid.SetColumnSpan(button, toolList.ColumnDefinitions.Count);
                toolList.Children.Add(button);
                return;
            }
            else
            {
                Tool tool = ToolRepository.getSpecificTool(h.Id);
                var myStyle = new Style<Button>(
                    (Button.HeightRequestProperty, 120),
                    (Button.MaximumWidthRequestProperty, 430),
                    (Button.TextColorProperty, Colors.Black),
                    (Button.BackgroundColorProperty, Colors.WhiteSmoke),
                    (Button.FontSizeProperty, 10)
                );

                string retTime = h.ReturnTime.Year == 1 ? "Not Returned" : h.ReturnTime.ToString();
                string chkTime = h.CheckoutTime.ToString();

                Button button = new Button
                {
                    Text = $"Tool: {tool.Name}\nCheckout time: {chkTime}\nReturn time: {retTime}",
                    LineBreakMode = LineBreakMode.WordWrap,
                    Style = myStyle,
                    Margin = new Thickness(15, 15, 15, 0)
                };

                // ✅ Add a new row for this button before the footer row (3)
                int insertRow = toolList.RowDefinitions.Count - 1; // place before bottom buttons
                toolList.RowDefinitions.Insert(insertRow, new RowDefinition { Height = GridLength.Auto });

                Grid.SetRow(button, insertRow);
                Grid.SetColumnSpan(button, toolList.ColumnDefinitions.Count);

                toolList.Children.Add(button);
            }
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
