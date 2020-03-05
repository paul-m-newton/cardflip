using System.Collections.Generic;
using Xamarin.Forms;

namespace TestUrho
{
    public class AbFabPage : ContentPage
    {

        private List<BoxView> _things = new List<BoxView>();
        private RelativeLayout _layout;

        public AbFabPage()
        {
            for (int i = 0; i < 9; i++)
            {
                _things.Add(item: new BoxView
                {
                    Color = Color.Pink,
                    HeightRequest = 200,
                    WidthRequest = 200
                });
            }


            _layout = new RelativeLayout();

            //
            _layout.Children.Add(view: _things[index: 0], 
                xConstraint: Constraint.RelativeToParent(measure: parent => 0),
                yConstraint: Constraint.RelativeToParent(measure: parent => 0),
                widthConstraint: Constraint.RelativeToParent(measure: parent => parent.Width / 4),
                heightConstraint: Constraint.RelativeToParent(measure: parent => parent.Height / 3));
            _layout.Children.Add(view: _things[index: 1], 
                xConstraint: Constraint.RelativeToParent(measure: parent => parent.Width / 3),
                yConstraint: Constraint.RelativeToParent(measure: parent => 0),
                widthConstraint: Constraint.RelativeToParent(measure: parent => parent.Width / 4),
                heightConstraint: Constraint.RelativeToParent(measure: parent => parent.Height / 3));

            _layout.Children.Add(view: _things[index: 2], 
                xConstraint: Constraint.RelativeToParent(measure: parent => (parent.Width / 3) * 2 ),
                yConstraint: Constraint.RelativeToParent(measure: parent => 0),
                widthConstraint: Constraint.RelativeToParent(measure: parent => parent.Width / 4),
                heightConstraint: Constraint.RelativeToParent(measure: parent => parent.Height / 3));

            _layout.Children.Add(view: _things[index: 3],
                xConstraint: Constraint.RelativeToParent(measure: parent => (parent.Width / 3) * 2),
                yConstraint: Constraint.RelativeToParent(measure: parent => 0),
                widthConstraint: Constraint.RelativeToParent(measure: parent => parent.Width / 4),
                heightConstraint: Constraint.RelativeToParent(measure: parent => parent.Height / 3));

            Content = _layout;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _things[1].Color = Color.Purple;
            _layout.RaiseChild(_things[1]);
            _things[1].ScaleTo(3, 2000);
            _things[1].RotateTo(360*3, 2000);

        }
    }
}