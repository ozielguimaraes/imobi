using Xamarin.Forms;

namespace Imobi.Views.Components
{
    public class ButtonRounded : Button
    {
        #region Public Constructors + Destructors

        public ButtonRounded()
        {
            Padding = new Thickness(30, 20, 30, 20);
            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Left, 20);
            HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true);
        }

        #endregion Public Constructors + Destructors
    }
}