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
        string name = tool != null ? tool.Name : mat.Name;
        object context = tool != null ? tool : mat;

        var nameLabel = new Label
        {
            Text = name,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        };

        var infoLabel = new Label
        {
            Text = tool != null ? "Tool" : "Material",
            FontSize = 14,
            TextColor = Color.FromArgb("#BBBBBB")
        };

        var stack = new VerticalStackLayout
        {
            Spacing = 6,
            Children =
        {
            nameLabel,
            infoLabel
        }
        };

        var frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            CornerRadius = 12,
            Padding = 12,
            Margin = new Thickness(0, 6),
            HasShadow = true,
            Content = stack,
            BindingContext = context
        };

        // Optional: tap behavior (acts like a button)
        var tap = new TapGestureRecognizer();
        tap.Tapped += (s, e) =>
        {
            var boundItem = ((Frame)s).BindingContext;
            // handle tap here if needed
        };
        frame.GestureRecognizers.Add(tap);

        listBox.Children.Add(frame);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        listBox.Children.Clear();


        GetInvTools();
    }
}