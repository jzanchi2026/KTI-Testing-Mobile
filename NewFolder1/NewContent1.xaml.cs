using MauiApp2;

namespace KTI_Testing__Mobile_.NewFolder1;

public partial class NewContent1 : ContentView
{
	public NewContent1()
	{
		InitializeComponent();
		if (App.UserInfo != null) 
		{
			lbUserName.Text = "Logged in as: " + App.UserInfo.Name;
			lbUserEmail.Text = App.UserInfo.Name;
		}
	}
}