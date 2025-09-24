namespace MauiApp2;

using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;

[QueryProperty("addedTool", "addedTool")]
public partial class Temp : ContentPage
{
    public Temp()
    {
        InitializeComponent();
    }

    public Temp(Tool t)
    {
        

        var myStyle = new Style<Entry>(

        (Entry.HeightRequestProperty, 120),
        (Entry.MaximumWidthRequestProperty, 430),
        (Entry.ShadowProperty, 10),
        (Entry.TextColorProperty, Colors.Black),
        (Entry.BackgroundColorProperty, Colors.WhiteSmoke),
        (Entry.FontSizeProperty, 28)
        );

        Button button = new Button { Text = t.Name, Style = myStyle };
        button.Margin = new Thickness(15, 15, 15, 0);

        Cart.Children.Add(button);
    }
}