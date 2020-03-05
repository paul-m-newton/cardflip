
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestUrho
{

    //Todo: Get working on devices -- android device

    public class GridPage : ContentPage
    {
        private GameConfiguration Game { get; set; }

        private Grid _grid;
        private Button resetButton;
        private Button showAllCardsButton;

        private Label label;
        private int Clicked { get; set; }

        private TapGestureRecognizer tapper;
        private TapGestureRecognizer tapped;

        #region Fake logic

        private string Reward { get; set; }

        public bool isCorrect { get; set; }

        public bool NotCorrect { get; set; }

        public int WinningCardIndex { get; set; }
        #endregion
        #region Setup
        public GridPage()
        {

            Game = new GameConfiguration("aspersback.jpg", 10, 10);
        
            for (int i = 0; i < Game.numberOfCards; i++)
            {
                BuildCardBacks(Game.CardBackImageString);                
            }

            BuildCardFronts();
            _grid = new Grid
            {
                RowDefinitions =
                {
                    AddRows()
                },           
        ColumnDefinitions =
                {
                    AddColumns()
                },
                ColumnSpacing = 5,
                RowSpacing = 5,
            };
           
            VisualSetup();

            tapped = new TapGestureRecognizer();
            tapped.Tapped += FlipAllCards;
            showAllCardsButton.GestureRecognizers.Add(tapped);

            Content = _grid;

    }
        private RowDefinition AddRows()
        {
            for (int i = 0; i < Game.CardsInRow; i++)
            {
                return new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
            }
            return new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
        }
        private ColumnDefinition AddColumns()
        {
            for (int i = 0; i < Game.CardsInColumn; i++)
            {
                return new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            }
            return new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
        }
        private void VisualSetup()
        {

            AddNextcard(0, 0, 1);
            resetButton = new Button
            {
                Text = "View Your Reward",
                IsVisible = false,
                Scale = 0.75,
                BackgroundColor = Color.DarkBlue,
                TextColor = Color.White,
            };

            showAllCardsButton = new Button
            {
                Text = "Reveal All Cards",
                IsVisible = false,
                Scale = 0.75,
                BackgroundColor = Color.DarkBlue,
                TextColor = Color.White,
            };

            label = new Label 
            { 
                Text = "Card Game",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White,
                FontSize = 60,
        };

            _grid.Children.Add(label, 0, 0);
            _grid.Children.Add(resetButton, 0, Game.CardsInColumn+1);
            _grid.Children.Add(showAllCardsButton, 0, Game.CardsInColumn+1);
            Grid.SetColumnSpan(label, Game.CardsInRow);
            Grid.SetColumnSpan(resetButton, Game.CardsInRow);
            Grid.SetColumnSpan(showAllCardsButton, Game.CardsInRow);

            int PackOfCards = Game.numberOfCards - Game.CardsInRow;
            for (int i = 0; i < Game.CardsBack.Count; i++)
            {
                if (i != PackOfCards)
                {
                    Game.CardsBack[i].TranslateTo(-2000, 2000, 0);
                }
                Game.CardsBack[PackOfCards].TranslateTo(0, 0, 0);
            }
            
            BackgroundColor = Color.Black;


        }

        private void AddNextcard(int total, int row, int column)
        {
          
            _grid.Children.Add(Game.CardsBack[total], row, column);
            total++;           
            if (total < Game.numberOfCards)
            {
                _ = row == Game.CardsInRow-1 ? row = 0 & column++ : row++;
                AddNextcard(total, row, column);
            }          
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        #endregion

        private void BuildCardBacks(string CardBackSource)
        {
            Game.CardBack = new Image();
            Game.CardBack.Source = CardBackSource;
            Game.CardBack.WidthRequest = 100;
            Game.CardBack.HeightRequest = 150;

            Game.CardsBack.Add(Game.CardBack);

            tapper = new TapGestureRecognizer();
            tapper.Tapped += TapperOnTapped;
            Game.CardBack.GestureRecognizers.Add(tapper);
        }
        private void BuildCardFronts()
        {
            List<Image> ImageOptions = new List<Image>();

            for (int i = 0; i < Game.CardsBack.Count; i++)
            {
                ImageOptions.Add(Game.NewImage("norewardaspers.jpg"));
            }

            Random rnd = new Random();
            int value = rnd.Next(0, Game.numberOfCards);

            ImageOptions[value] = Game.NewImage("rewardaspers.jpg");

            Game.CardsFront = ImageOptions;

        }

        private async Task OnAlertYesNoClicked(object sender, EventArgs e)
        {
            await Task.Delay(1000);

            //Reward = "500 points";
            showAllCardsButton.IsVisible = true;
            await DisplayAlert("Congratulations", "You just won a Reward", "close");
        }

        private async void TapperOnTapped(object sender, EventArgs e)
        {

            var thing = (Image)sender;
            TranslateIn();

            if (Clicked == 1)
            {                          

                FlipCardAsync(thing);

                await OnAlertYesNoClicked(sender, e);

            }
            Clicked++;
        }

        private async void TranslateIn()
        {
            if(Clicked < 1)
            {
                for (int i = 0; i < Game.numberOfCards; i++)
                {
                    
                        await Game.CardsBack[i].TranslateTo(0, 0, 250);
                    
                }
                Clicked = 1;
            }
        }

        private async Task FlipCardAsync(Image image)
        {
            
            image.TranslateTo(100, 0, 400);
            await image.RotateYTo(-90, 200);
            image.RotationY = -270;
            image.Source = Game.CardsFront[Game.GetIndexOfCard(image)].Source;
            image.RotateYTo(-360, 200);
            await image.TranslateTo(0, 0, 220);
            image.RotationY = 0;
        }

        private async void FlipAllCards(object sender, EventArgs e)
        {
            for (int i = 0; i < Game.CardsBack.Count; i++)
            {
                await Game.FlipCard(i);
            }

            showAllCardsButton.IsVisible = false;
            resetButton.IsVisible = true;

        }

    }
   
}