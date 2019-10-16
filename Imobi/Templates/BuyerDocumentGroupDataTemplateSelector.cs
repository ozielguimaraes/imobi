using Imobi.Views.ContentViews;
using Xamarin.Forms;

namespace Imobi.Templates
{
    public class BuyerDocumentGroupDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FourColumns { get; private set; }

        public BuyerDocumentGroupDataTemplateSelector()
        {
            FourColumns = new DataTemplate(typeof(BuyerDocumentGroupFourColumnContentView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return FourColumns;
        }
    }
}