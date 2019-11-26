using Xamarin.Forms;

namespace Imobi.Controls
{
    public class Picker : Xamarin.Forms.Picker
    {
        #region Public Properties

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        #endregion Public Properties



        #region Public Fields + Structs

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
                      propertyName: nameof(PlaceholderColor),
              returnType: typeof(Color),
              declaringType: typeof(Color),
              defaultValue: GlobalStyles.EntryPlaceholderColorPrimary);

        #endregion Public Fields + Structs
    }
}