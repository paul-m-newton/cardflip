using Xamarin.Forms;

namespace TestUrho.UWP
{
    public class GamePage : ContentPage
    {
        public GamePage()
        {
            Content = new StackLayout
            {
                Children = 
                { 
                    new Label{Text = "GAME"}
                }
            };
        }
    }
}