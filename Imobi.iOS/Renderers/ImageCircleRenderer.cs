using System;
using System.ComponentModel;
using Imobi.iOS.Renderers;
using Imobi.Views.Components;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(ImageCircle), typeof(ImageCircleRenderer))]

namespace Imobi.iOS.Renderers
{
    internal class ImageCircleRenderer : ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null) return;

            CreateCircle();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
              e.PropertyName == ImageCircle.BorderColorProperty.PropertyName ||
              e.PropertyName == ImageCircle.BorderThicknessProperty.PropertyName ||
              e.PropertyName == ImageCircle.FillColorProperty.PropertyName)
            {
                CreateCircle();
            }
        }

        private void CreateCircle()
        {
            try
            {
                var min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (nfloat)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.BackgroundColor = ((ImageCircle)Element).FillColor.ToUIColor();
                Control.ClipsToBounds = true;

                var borderThickness = ((ImageCircle)Element).BorderThickness;

                //Remove previously added layers
                var tempLayer = Control.Layer.Sublayers?
                                       .Where(p => p.Name == borderName)
                                       .FirstOrDefault();
                tempLayer?.RemoveFromSuperLayer();

                var externalBorder = new CALayer
                {
                    Name = borderName,
                    CornerRadius = Control.Layer.CornerRadius,
                    Frame = new CGRect(-.5, -.5, min + 1, min + 1),
                    BorderColor = ((ImageCircle)Element).BorderColor.ToCGColor(),
                    BorderWidth = ((ImageCircle)Element).BorderThickness
                };

                Control.Layer.AddSublayer(externalBorder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }

        private const string borderName = "borderLayerName";
    }
}