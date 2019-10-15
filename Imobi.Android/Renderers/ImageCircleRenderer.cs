using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.ComponentModel;
using Imobi.Views.Components;
using Imobi.Droid.Renderers;

[assembly: ExportRenderer(typeof(ImageCircle), typeof(ImageCircleRenderer))]

namespace Imobi.Droid.Renderers
{
    public class ImageCircleRenderer : ImageRenderer
    {
        public ImageCircleRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement is null)
            {
                if ((int)Android.OS.Build.VERSION.SdkInt < 18)
                    SetLayerType(LayerType.Software, null);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ImageCircle.BorderColorProperty.PropertyName ||
              e.PropertyName == ImageCircle.BorderThicknessProperty.PropertyName ||
              e.PropertyName == ImageCircle.FillColorProperty.PropertyName)
            {
                Invalidate();
            }
        }

        protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            try
            {
                var radius = (float)Math.Min(Width, Height) / 2f;
                var borderThickness = ((ImageCircle)Element).BorderThickness;
                var strokeWidth = 0f;

                if (borderThickness > 0)
                {
                    var logicalDensity = Android.App.Application.Context.Resources.DisplayMetrics.Density;
                    strokeWidth = (float)Math.Ceiling(borderThickness * logicalDensity + .5f);
                }
                radius -= strokeWidth / 2f;

                var path = new Path();
                path.AddCircle(Width / 2.0f, Height / 2.0f, radius, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);

                var paint = new Paint
                {
                    AntiAlias = true
                };
                paint.SetStyle(Paint.Style.Fill);
                paint.Color = ((ImageCircle)Element).FillColor.ToAndroid();
                canvas.DrawPath(path, paint);
                paint.Dispose();

                var result = base.DrawChild(canvas, child, drawingTime);

                path.Dispose();
                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2f, Height / 2f, radius, Path.Direction.Ccw);

                if (strokeWidth > 0.0f)
                {
                    paint = new Paint
                    {
                        AntiAlias = true,
                        StrokeWidth = strokeWidth
                    };
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = ((ImageCircle)Element).BorderColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }

                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }
            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}