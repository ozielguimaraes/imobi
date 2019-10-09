using Xamarin.Forms;

namespace Imobi.Views.Components
{
    public class ButtonRounded : Button
    {
        public ButtonRounded()
        {
            Padding = new Thickness(30, 20, 30, 20);
            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Left, 20);
            HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true);
        }
    }
}