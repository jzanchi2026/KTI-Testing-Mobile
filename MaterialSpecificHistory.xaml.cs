using CommunityToolkit.Maui.Markup;
using KTI_Testing__Mobile_.Models;
using MauiApp2.Models;

namespace MauiApp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSpecificHistory : ContentPage
    {
        private Material mat;
        public MaterialSpecificHistory()
        {
            InitializeComponent();
            grabMats();

        }
        public MaterialSpecificHistory(Material the)
        {
            InitializeComponent();
            mat = the;
            grabMats();
            

        }
        public async void grabMats()
        {
    
            List<HistoryObject> matHistory = await MaterialRepository.specificMaterialHistory(mat.Id);
            foreach (HistoryObject i in matHistory)
            {
                addItem(i);
            }

        }
        public void addItem(HistoryObject h)
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
                Text = $"Material: {mat.Name} Amount: {h.TakenQ}\nCheckout time: {chkTime}\nReturn time: {retTime}",
                LineBreakMode = LineBreakMode.WordWrap,
                Style = myStyle,
                Margin = new Thickness(15, 15, 15, 0)
            };

            // ✅ Add a new row for this button before the footer row (3)
            int insertRow = matList.RowDefinitions.Count - 1; // place before bottom buttons
            matList.RowDefinitions.Insert(insertRow, new RowDefinition { Height = GridLength.Auto });

            Grid.SetRow(button, insertRow);
            Grid.SetColumnSpan(button, matList.ColumnDefinitions.Count);

            matList.Children.Add(button);
        }
    }
}