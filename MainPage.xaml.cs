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
        private static List<Material> matlist = new();

        public MainPage()
        {
            InitializeComponent();
            _ = grabTools();
            _ = grabMaterials();
        }

        private async Task grabTools()
        {
            await ToolRepository.InitializeToolsAsync();
            List<Tool> tools = ToolRepository.GetTools();

            foreach (Tool i in tools)
                addTool(i);
        }
        private async Task grabMaterials()
        {
            await MaterialRepository.InitializeMaterialsAsync();
            List<Material> materials = MaterialRepository.GetMaterials();
            foreach (Material i in materials)
                addMaterial(i);
        }

        public void addTool(Tool tool)
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
                BindingContext = tool //  attach the whole tool object
            };

            button.Clicked += async (s, e) =>
            {
                var btn = (Button)s;
                var selectedTool = (Tool)btn.BindingContext; //  retrieve attached tool
                await Navigation.PushAsync(new ToolInfo(selectedTool, "button"));
                
            };

            listBox.Children.Add(button);
        }
        public void addMaterial(Material mat)
        {
            matlist.Add(mat);

            var myStyle = new Style<Button>(
                (Button.HeightRequestProperty, 120),
                (Button.MaximumWidthRequestProperty, 430),
                (Button.TextColorProperty, Colors.Black),
                (Button.BackgroundColorProperty, Colors.Beige),
                (Button.FontSizeProperty, 28)
            );

            var button = new Button
            {
                Text = mat.name,
                Style = myStyle,
                Margin = new Thickness(15, 15, 15, 0),
                BindingContext = mat //  attach the whole tool object
            };

            button.Clicked += async (s, e) =>
            {
                var btn = (Button)s;
                var selectedTool = (Tool)btn.BindingContext; //  retrieve attached tool
                await Navigation.PushAsync(new ToolInfo(selectedTool, "button"));

            };

            listBox.Children.Add(button);
        }

        private async void toolList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Tool selectedTool)
            {
                await Navigation.PushAsync(new ToolInfo(selectedTool, "button"));
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
