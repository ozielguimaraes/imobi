using System;

namespace Imobi.ViewModels
{
    public class ProposalFlowViewModel : BaseViewModel
    {
        private int _installmentsAmount;

        public int InstallmentsAmount
        {
            get { return _installmentsAmount; }
            set { SetProperty(ref _installmentsAmount, value); }
        }

        private decimal _value;

        public decimal Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private DateTime _firstExpirationDate;

        public DateTime FirstExpirationDate
        {
            get { return _firstExpirationDate; }
            set { SetProperty(ref _firstExpirationDate, value); }
        }

        private DateTime _lastExpirationDate;

        public DateTime LastExpirationDate
        {
            get { return _lastExpirationDate; }
            set { SetProperty(ref _lastExpirationDate, value); }
        }
    }
}