﻿using CommunityToolkit.Maui.Markup;
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

        private static List<Tool> toollist = new List<Tool>();

        public MainPage()
        {
            InitializeComponent();

            List<Tool> tools = ToolRepository.GetTools();

            Tool cloneTool = new Tool(1, "Hammer", "a bangy boi", 50);

            for (int i = 0; i < 20; i++)
            {
                addItem(cloneTool);
            }
        }

        public void addItem(Tool tool)
        {
            toollist.Add(tool);

            var myStyle = new Style<Button>(

            (Button.HeightRequestProperty, 120),
            (Button.MaximumWidthRequestProperty, 430),
            (Button.TextColorProperty, Colors.Black),
            (Button.BackgroundColorProperty, Colors.WhiteSmoke),
            (Button.FontSizeProperty, 28)
            );

            Button button = new Button { Text = tool.Name, Style = myStyle };
            button.Margin = new Thickness(15, 15, 15, 0);
            listBox.Children.Add(button);
        }

        private async void toolList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (toolList.SelectedItem != null)
            {
                await Shell.Current.GoToAsync($"{nameof(CartPage)}?Id={((Tools)toolList.SelectedItem).toolId}");
            }


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
            var tools = new ObservableCollection<Tool>(ToolRepository.SearchTools(((SearchBar)sender).Text));
            toolList.ItemsSource = tools;
        }
    }
    //test
}
