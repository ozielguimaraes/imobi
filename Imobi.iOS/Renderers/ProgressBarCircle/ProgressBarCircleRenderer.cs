using System;
using CoreGraphics;
using Foundation;
using Imobi.Controls.ProgressBarCircle;
using Imobi.iOS.Renderers.ProgressBarCircle;
using ProgressRingControl.Forms.Plugin;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ProgressBarCircle), typeof(ProgressBarCircleRenderer))]

namespace Imobi.iOS.Renderers.ProgressBarCircle
{
    [Preserve(AllMembers = true)]
    public class ProgressBarCircleRenderer : ViewRenderer
    {
        #region Private Fields + Structs

        private float? _radius;
        private bool _sizeChanged = false;

        #endregion Private Fields + Structs



        #region Public Methods

        /// <summary>
        /// Necessary to register this class with the Xamarin.Forms with dependency service
        /// </summary>
        public async static void Init()
        {
            var temp = DateTime.Now;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (CGContext g = UIGraphics.GetCurrentContext())
            {
                var progressRing = (ProgressRing)Element;

                var lineWidth = (float)progressRing.RingThickness;
                var radius = (int)(GetRadius(lineWidth));
                var progress = (float)progressRing.Progress;
                var backColor = progressRing.RingBaseColor.ToUIColor();
                var frontColor = progressRing.RingProgressColor.ToUIColor();

                DrawProgressRing(g, Bounds.GetMidX(), Bounds.GetMidY(), progress, lineWidth, radius, backColor, frontColor);
            }
        }

        #endregion Public Methods



        #region Protected Methods

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var progressRing = (Controls.ProgressBarCircle.ProgressBarCircle)Element;

            if (e.PropertyName == ProgressBar.ProgressProperty.PropertyName ||
                e.PropertyName == Controls.ProgressBarCircle.ProgressBarCircle.RingThicknessProperty.PropertyName ||
                e.PropertyName == Controls.ProgressBarCircle.ProgressBarCircle.RingBaseColorProperty.PropertyName ||
                e.PropertyName == Controls.ProgressBarCircle.ProgressBarCircle.RingProgressColorProperty.PropertyName)
            {
                SetNeedsDisplay();
            }

            if (e.PropertyName == VisualElement.WidthProperty.PropertyName ||
               e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                _sizeChanged = true;
                SetNeedsDisplay();
            }
        }

        #endregion Protected Methods

        #region Private Methods

        // TODO Optimize circle drawing by removing allocation of CGPath
        // (maybe by drawing via BitmapContext, per pixel:
        // https://stackoverflow.com/questions/34987442/drawing-pixels-on-the-screen-using-coregraphics-in-swift)
        private void DrawProgressRing(CGContext g, nfloat x0, nfloat y0,
                                     nfloat progress, nfloat lineThickness, nfloat radius,
                                     UIColor backColor, UIColor frontColor)
        {
            g.SetLineWidth(lineThickness);

            // Draw background circle
            CGPath path = new CGPath();

            backColor.SetStroke();

            path.AddArc(x0, y0, radius, 0, 2.0f * (float)Math.PI, true);
            g.AddPath(path);
            g.DrawPath(CGPathDrawingMode.Stroke);

            // Draw progress circle
            var pathStatus = new CGPath();
            frontColor.SetStroke();

            var startingAngle = 1.5f * (float)Math.PI;
            pathStatus.AddArc(x0, y0, radius, startingAngle, startingAngle + progress * 2 * (float)Math.PI, false);

            g.AddPath(pathStatus);
            g.DrawPath(CGPathDrawingMode.Stroke);
        }

        private nfloat GetRadius(nfloat lineWidth)
        {
            if (_radius == null || _sizeChanged)
            {
                _sizeChanged = false;

                nfloat width = Bounds.Width;
                nfloat height = Bounds.Height;
                var size = (float)Math.Min(width, height);

                _radius = (size / 2f) - ((float)lineWidth / 2f);
            }

            return _radius.Value;
        }

        #endregion Private Methods
    }
}