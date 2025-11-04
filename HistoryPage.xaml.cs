using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;
using MauiApp2.Models;

namespace MauiApp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            _ = grabTools();

        }
        public async Task grabTools()
        {
            List<HistoryObject> tools = await ToolRepository.userToolHistory();

            foreach (HistoryObject i in tools)
            {
                addItem(i);
            }
        }
        public void addItem(HistoryObject h)
        {
            Tool tool = ToolRepository.getSpecificTool(h.ToolId);
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
                Text = $"{tool.Name}\nCheckout time: {chkTime}\nReturn time: {retTime}",
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

        private void latestSubmitions_Clicked(object sender, EventArgs e)
        {

        }
        /*
        private void GoToProfilePage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(ProfilePage));
        }

        private void GoToCartPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(CartPage));
        }

        private void GoToSettingPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(SettingsPage));
        }
        */
    }
}