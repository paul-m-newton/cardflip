using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestUrho
{
    public class GameConfiguration
    {
        public List<Image> CardsFront = new List<Image>();
        public List<Image> CardsBack = new List<Image>();
        public Image CardBack { get; set; }
        public Image CardFront { get; set; }
        public string CardBackImageString { get; set; }
        public string CardFrontImageString { get; set; }

        public int numberOfCards { get; set; }
        public int CardsInRow { get; set; }
        public int CardsInColumn { get; set; }

        public GameConfiguration(string _CardImageString,int _CardsInRow, int _CardsInColumn)
        {
            CardBackImageString = _CardImageString;
            CardsInRow = _CardsInRow;
            CardsInColumn = _CardsInColumn;

            numberOfCards = CardsInRow * CardsInColumn;

        }
        public Image NewImage(string ImageSourceString)
        {
            CardFront = new Image();
            CardFront.Source = ImageSourceString;
            CardFront.WidthRequest = 100;
            CardFront.HeightRequest = 150;

            return CardFront;
        }
        public async Task FlipCard(int IndexOfCard)
        {
            CardsBack[IndexOfCard].TranslateTo(100, 0, 400);
            await CardsBack[IndexOfCard].RotateYTo(-90, 200);
            CardsBack[IndexOfCard].RotationY = -270;
            CardsBack[IndexOfCard].Source = CardsFront[IndexOfCard].Source;
            CardsBack[IndexOfCard].RotateYTo(-360, 150);
            await CardsBack[IndexOfCard].TranslateTo(0, 0, 150);
            CardsBack[IndexOfCard].RotationY = 0;
        }
        internal int GetIndexOfCard(Image image)
        {
            var imageIndex = CardsBack.IndexOf(image);

            return imageIndex;
        }
    }
}
