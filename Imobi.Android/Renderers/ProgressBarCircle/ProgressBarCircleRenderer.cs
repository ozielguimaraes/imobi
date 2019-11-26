using System;
using Android.Graphics;
using Imobi;
using Imobi.Controls.ProgressBarCircle;
using Imobi.Droid;
using Imobi.Droid.Renderers.ProgressBarCircle;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(ProgressBarCircle), typeof(ProgressBarCircleRenderer))]

namespace Imobi.Droid.Renderers.ProgressBarCircle
{
    public class ProgressBarCircleRenderer : ViewRenderer
    {
        #region Private Fields + Structs

        private Paint _paint;
        private RectF _ringDrawArea;
        private bool _sizeChanged = false;

        #endregion Private Fields + Structs

        #region Public Constructors + Destructors

        public ProgressBarCircleRenderer()
        {
            SetWillNotDraw(false);
        }

        #endregion Public Constructors + Destructors



        #region Protected Methods

        protected override void OnDraw(Canvas canvas)
        {
            var progressRing = (Controls.ProgressBarCircle.ProgressBarCircle)Element;

            if (_paint == null)
            {
                var displayDensity = Context.Resources.DisplayMetrics.Density;
                var strokeWidth = (float)Math.Ceiling(progressRing.RingThickness * displayDensity);

                _paint = new Paint
                {
                    StrokeWidth = strokeWidth
                };
                _paint.SetStyle(Paint.Style.Stroke);
                _paint.Flags = PaintFlags.AntiAlias;
            }

            if (_ringDrawArea == null || _sizeChanged)
            {
                _sizeChanged = false;

                var ringAreaSize = Math.Min(canvas.ClipBounds.Width(), canvas.ClipBounds.Height());

                var ringDiameter = ringAreaSize - _paint.StrokeWidth;

                var left = canvas.ClipBounds.CenterX() - ringDiameter / 2;
                var top = canvas.ClipBounds.CenterY() - ringDiameter / 2;

                _ringDrawArea = new RectF(left, top, left + ringDiameter, top + ringDiameter);
            }

            var backColor = progressRing.RingBaseColor;
            var frontColor = progressRing.RingProgressColor;
            var progress = (float)progressRing.Progress;
            DrawProgressRing(canvas, progress, backColor, frontColor);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ProgressBar.ProgressProperty.PropertyName ||
                e.PropertyName == Controls.ProgressBarCircle.ProgressBarCircle.RingThicknessProperty.PropertyName ||
                e.PropertyName == Controls.ProgressBarCircle.ProgressBarCircle.RingBaseColorProperty.PropertyName ||
                e.PropertyName == Controls.ProgressBarCircle.ProgressBarCircle.RingProgressColorProperty.PropertyName)
            {
                Invalidate();
            }

            if (e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                _sizeChanged = true;
                Invalidate();
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void DrawProgressRing(Canvas canvas, float progress,
                                              Color ringBaseColor,
                                      Color ringProgressColor)
        {
            _paint.Color = ringBaseColor.ToAndroid();
            canvas.DrawArc(_ringDrawArea, 270, 360, false, _paint);

            _paint.Color = ringProgressColor.ToAndroid();
            canvas.DrawArc(_ringDrawArea, 270, 360 * progress, false, _paint);
        }

        #endregion Private Methods
    }
}