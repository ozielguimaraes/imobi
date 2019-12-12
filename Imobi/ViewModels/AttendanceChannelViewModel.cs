using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Imobi.ViewModels
{
    public class AttendanceChannelViewModel : BaseViewModel
    {
        #region Public Properties

        public ObservableCollection<string> Questions
        {
            get { return _questions; }
            set => SetProperty(ref _questions, value);
        }

        #endregion Public Properties



        #region Private Fields + Structs

        private ObservableCollection<string> _questions;

        #endregion Private Fields + Structs

        #region Public Constructors + Destructors

        public AttendanceChannelViewModel()
        {
            Questions = new ObservableCollection<string>();
        }

        #endregion Public Constructors + Destructors



        #region Private Methods

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            await Task.Delay(150);
            LoadItems();
            IsBusy = false;
        }

        private void LoadItems()
        {
            Questions.Add("Como realizar o login?");
            Questions.Add("Como recuperar a senha?");
            Questions.Add("Como solicito reembolso de consulta médica?");
            Questions.Add("Como solicito reembolso de internação?");
            Questions.Add("Como solicito o reembolso de Exames, procedimentos e terapias?");
        }

        #endregion Private Methods
    }
}