using Xamarin.Forms;

namespace Imobi.Behaviors
{
    public class ToUpperCaseBehavior : BehaviorBase<Entry>
    {
        private static readonly BindablePropertyKey IsUpperCasePropertyKey = BindableProperty.CreateReadOnly("IsUpperCase", typeof(bool), typeof(ToUpperCaseBehavior), true);

        public static readonly BindableProperty IsUpperCaseProperty = IsUpperCasePropertyKey.BindableProperty;

        //Método responsável por vincular o evento de "TextChanged" ao Entry
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Entry_TextChanged;
            base.OnAttachedTo(bindable);
        }

        //Método responsável por desvincular o evento de "TextChanged" ao Entry
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(bindable);
        }

        //Método responsável por executar a validação do campo
        private static void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue is null) return;

            if (!(sender is Entry entry)) return;
            entry.Text = e.NewTextValue.ToUpper();
        }
    }
}