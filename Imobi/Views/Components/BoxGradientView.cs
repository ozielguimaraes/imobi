using Xamarin.Forms;

namespace Imobi.Views.Components
{
    public class BoxGradientView : BoxView
    {
        public string ColorsList { get; set; }

        public Color[] Colors
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ColorsList)) return null;

                string[] hex = ColorsList.Split(',');
                Color[] colors = new Color[hex.Length];

                for (int i = 0; i < hex.Length; i++)
                    colors[i] = Color.FromHex(hex[i].Trim());

                return colors;
            }
        }

        public GradientColorMode Mode { get; set; }
    }

    public enum GradientColorMode
    {
        ToRight,
        ToLeft,
        ToTop,
        ToBottom,
        ToTopLeft,
        ToTopRight,
        ToBottomLeft,
        ToBottomRight
    }
}