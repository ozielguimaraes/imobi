using CoreAnimation;
using CoreGraphics;
using Imobi.iOS.Renderers;
using Imobi.Views.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BoxGradientView), typeof(BoxGradientViewRenderer))]

namespace Imobi.iOS.Renderers
{
    public class BoxGradientViewRenderer : VisualElementRenderer<BoxView>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            BoxGradientView layout = (BoxGradientView)Element;
            if (layout.Colors?.Length <= 0) return;

            CGColor[] colors = new CGColor[layout.Colors.Length];

            for (int i = 0, l = colors.Length; i < l; i++)
            {
                colors[i] = layout.Colors[i].ToCGColor();
            }

            var gradientLayer = new CAGradientLayer();

            switch (layout.Mode)
            {
                default:
                case GradientColorMode.ToRight:
                    gradientLayer.StartPoint = new CGPoint(0, 0.5);
                    gradientLayer.EndPoint = new CGPoint(1, 0.5);
                    break;

                case GradientColorMode.ToLeft:
                    gradientLayer.StartPoint = new CGPoint(1, 0.5);
                    gradientLayer.EndPoint = new CGPoint(0, 0.5);
                    break;

                case GradientColorMode.ToTop:
                    gradientLayer.StartPoint = new CGPoint(0.5, 0);
                    gradientLayer.EndPoint = new CGPoint(0.5, 1);
                    break;

                case GradientColorMode.ToBottom:
                    gradientLayer.StartPoint = new CGPoint(0.5, 1);
                    gradientLayer.EndPoint = new CGPoint(0.5, 0);
                    break;

                case GradientColorMode.ToTopLeft:
                    gradientLayer.StartPoint = new CGPoint(1, 0);
                    gradientLayer.EndPoint = new CGPoint(0, 1);
                    break;

                case GradientColorMode.ToTopRight:
                    gradientLayer.StartPoint = new CGPoint(0, 1);
                    gradientLayer.EndPoint = new CGPoint(1, 0);
                    break;

                case GradientColorMode.ToBottomLeft:
                    gradientLayer.StartPoint = new CGPoint(1, 1);
                    gradientLayer.EndPoint = new CGPoint(0, 0);
                    break;

                case GradientColorMode.ToBottomRight:
                    gradientLayer.StartPoint = new CGPoint(0, 0);
                    gradientLayer.EndPoint = new CGPoint(1, 1);
                    break;
            }

            gradientLayer.Frame = rect;
            gradientLayer.Colors = colors;

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}