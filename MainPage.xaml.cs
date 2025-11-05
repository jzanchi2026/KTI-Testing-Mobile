using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_;
using KTI_Testing__Mobile_.Models;
using MauiApp2;
using MauiApp2.Models;
using System.Collections.ObjectModel;

namespace MauiApp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private static List<Tool> toollist = new();

        public MainPage()
        {
            InitializeComponent();
            _ = grabTools();
        }

        private async Task grabTools()
        {
            await ToolRepository.InitializeToolsAsync();
            List<Tool> tools = ToolRepository.GetTools();

            foreach (Tool i in tools)
                addItem(i);
        }

        public void addItem(Tool tool)
        {
            toollist.Add(tool);

            var myStyle = new Style<Button>(
                (Button.HeightRequestProperty, 120),
                (Button.MaximumWidthRequestProperty, 430),
                (Button.TextColorProperty, Colors.Black),
                (Button.BackgroundColorProperty, Colors.Beige),
                (Button.FontSizeProperty, 28)
            );

            var button = new Button
            {
                Text = tool.Name,
                Style = myStyle,
                Margin = new Thickness(15, 15, 15, 0),
                BindingContext = tool // 👈 attach the whole tool object
            };

            button.Clicked += async (s, e) =>
            {
                var btn = (Button)s;
                var selectedTool = (Tool)btn.BindingContext; // 👈 retrieve attached tool
                await Shell.Current.GoToAsync($"{nameof(ToolInfo)}?Id={selectedTool.Id}");
            };

            listBox.Children.Add(button);
        }

        private async void toolList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Tool selectedTool)
            {
                await Shell.Current.GoToAsync($"{nameof(ToolInfo)}?Id={selectedTool.Id}");
            }
            toolList.SelectedItem = null;
        }

        private void toolList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            toolList.SelectedItem = null;
        }

        private void loadTools()
        {
            var tools = new ObservableCollection<Tool>(ToolRepository.GetTools());
            toolList.ItemsSource = tools;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var tools = new ObservableCollection<Tool>(ToolRepository.SearchTools(searchBar.Text));
            toolList.ItemsSource = tools;
        }
    }
}
