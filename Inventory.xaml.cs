namespace MauiApp2;

using System.ComponentModel;
using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_;
using KTI_Testing__Mobile_.Models;
/* Unmerged change from project 'KTI Testing (Mobile) (net8.0-android)'
Before:
using System.Collections;
using CommunityToolkit.Maui.Markup;
After:
using System.Collections;
*/
using KTI_Testing__Mobile_.Resources.viewModels;
using MauiApp2.Models;
using Newtonsoft.Json.Linq;

/* Unmerged change from project 'KTI Testing (Mobile) (net8.0-ios)'
Before:
using System.Collections;
using CommunityToolkit.Maui.Markup;
After:
using System.Collections;
*/

/* Unmerged change from project 'KTI Testing (Mobile) (net8.0-maccatalyst)'
Before:
using System.Collections;
using CommunityToolkit.Maui.Markup;
After:
using System.Collections;
*/


public partial class Inventory : ContentPage
{
    List<Tool> tools;
    public Inventory()
    {
        InitializeComponent();
        //GetInvTools();
    }
    private async void GetInvTools()
    {
        List<Tool> toolList = await ToolRepository.ringo("getUserTools");
        foreach (Tool i in toolList)
        {
            addItem(i, null);
        }
        List<Material> matList = await MaterialRepository.dingo("getUserMaterials");
        foreach (Material i in matList)
        {
            addItem(null, i);
        }

    }
    public void addItem(Tool tool, Material mat)
    {
        if(mat == null)
        {
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
            listBox.Children.Add(button);
        }
        if (tool == null)
        {
            var myStyle = new Style<Button>(
            (Button.HeightRequestProperty, 120),
            (Button.MaximumWidthRequestProperty, 430),
            (Button.TextColorProperty, Colors.Black),
            (Button.BackgroundColorProperty, Colors.Beige),
            (Button.FontSizeProperty, 28)
);


            var button = new Button
            {
                Text = mat.Name,
                Style = myStyle,
                Margin = new Thickness(15, 15, 15, 0),
                BindingContext = mat //  attach the whole tool object
            };
            listBox.Children.Add(button);
        }

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        foreach (var button in listBox.Children.OfType<Button>().ToList())
        {
            listBox.Children.Remove(button);
        }

        GetInvTools();
    }
}