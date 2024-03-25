namespace Elite.LMS.Maui;

public class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        Title = "Welcome";

        Padding = 12;

        Content = new VerticalStackLayout
        {
            Spacing = 12,

            Children =
            {
                new Label { Text = "Welcome to the .NET MAUI Community Toolkit"},
                new Label { Text = "Explore features using the flyout menu in the top left"}
            }
        };
    }
}
