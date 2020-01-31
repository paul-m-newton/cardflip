using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestUrho
{
    public class GridPage : ContentPage
    {
        private BoxView _thing;

        private List<BoxView> _things = new List<BoxView>();
        private Grid _grid;

        public GridPage()
        {
            for (int i = 0; i < 9; i++)
            {
                var thing = new BoxView
                {
                    Color = Color.Pink,
                    HeightRequest = 200,
                    WidthRequest = 200
                };
                var tapper = new TapGestureRecognizer();
                tapper.Tapped += TapperOnTapped;
                thing.GestureRecognizers.Add(tapper);
                _things.Add(thing);
            }

            _thing = new BoxView
            {
                Color = Color.Pink,
                HeightRequest = 200,
                WidthRequest = 200
            };

            _grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                }
            };

            _grid.Children.Add(_things[0], 0, 0);
            _grid.Children.Add(_things[1], 1, 0);
            _grid.Children.Add(_things[2], 2, 0);
            _grid.Children.Add(_things[3], 0, 1);
            _grid.Children.Add(_things[4], 1, 1);
            _grid.Children.Add(_things[5], 2, 1);
            _grid.Children.Add(_things[6], 0, 2);
            _grid.Children.Add(_things[7], 1, 2);
            _grid.Children.Add(_things[8], 2, 2);

            Content = _grid;

        }

        private async void TapperOnTapped(object sender, EventArgs e)
        {
            var thing = (BoxView)sender;

            var red = (double)new Random().Next(0,255)/255;
            var green = (double)new Random().Next(0,255)/255;
            var blue = (double)new Random().Next(0,255)/255;
            var randomColour = new Color(red, green, blue);

            thing.Color = randomColour;
            _grid.RaiseChild(thing);
             await thing.ScaleTo(2, 1000);
             await thing.RotateTo(720, 1000);
          
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            //_grid.RaiseChild(_things[4]);
            //_things[4].Color = Color.Purple;
            ////_things[4].RotateTo(360 * 3, 2000);
            ////_things[4].ScaleTo(0.5, 2000);
            //_things[4].ScaleTo(2, 1000);
            //_things[4].RotateTo(720, 1000);

            //_thing.RotateTo(360, 5000);
            //_thing.TranslateTo(-200, 0, 5000, Easing.SpringOut);
        }
    }
}