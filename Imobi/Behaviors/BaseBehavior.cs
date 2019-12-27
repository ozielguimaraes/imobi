using System;
using Xamarin.Forms;

namespace Imobi.Behaviors
{
    public class BaseBehavior<T> : Behavior<T> where T : BindableObject
    {
        #region Public Properties

        public T AssociatedObject { get; private set; }

        #endregion Public Properties



        #region Protected Methods

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null) BindingContext = bindable.BindingContext;

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        #endregion Protected Methods

        #region Private Methods

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        #endregion Private Methods
    }
}