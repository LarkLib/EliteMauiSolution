using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views;

public class NewContent1 : ContentPage
{
	public NewContent1()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}