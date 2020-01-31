
using Urho.Forms;
using Xamarin.Forms;

namespace TestUrho
{
    public class GamePage : ContentPage
    {

        private readonly UrhoSurface _urhoSurface = new UrhoSurface
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.PaleGoldenrod
        };

        private Game _urhoGame;

        public GamePage()
        {

            BindingContext = new GameViewModel();

            Content = _urhoSurface;

        }

        protected override async void OnAppearing()
        {
            StartUrhoApp();
        }

        async void StartUrhoApp()
        {
            _urhoGame = await _urhoSurface.Show<Game>(new Urho.ApplicationOptions()
            {
                Orientation = Urho.ApplicationOptions.OrientationType.LandscapeAndPortrait
            });
        }
    }
}