using Imobi.Validations.Base;
using System;

namespace Imobi.ViewModels
{
    public class PreKeyViewModel : BaseViewModel
    {
        private string _numberOfInstallmentsString;

        public string NumberOfInstallmentsString
        {
            get { return _numberOfInstallmentsString; }
            set
            {
                _numberOfInstallmentsString = value;

                if (!string.IsNullOrEmpty(value))
                {
                    if (int.TryParse(value, out _numberOfInstallments))
                        OnPropertyChanged(nameof(NumberOfInstallments));
                }
            }
        }

        private int _numberOfInstallments;

        public int NumberOfInstallments
        {
            get { return _numberOfInstallments; }
            set { SetProperty(ref _numberOfInstallments, value); }
        }

        private ValidableObject<string> _value = new ValidableObject<string>();

        public ValidableObject<string> Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        public decimal FinalValue { get; private set; }

        private DateTime _firstExpirationDate;

        public DateTime FirstExpirationDate
        {
            get { return _firstExpirationDate; }
            set { SetProperty(ref _firstExpirationDate, value); }
        }

        private DateTime _lastExpirationDate;

        public DateTime LastExpirationDate
        {
            get { return _lastExpirationDate = CalculateLastExpirationDate(); }
            private set { SetProperty(ref _lastExpirationDate, value); }
        }

        private DateTime CalculateLastExpirationDate()
        {
            return FirstExpirationDate.AddMonths(NumberOfInstallments);
        }

        internal void FillValueProperty()
        {
            FinalValue = GetValue();
        }

        public decimal GetValue()
        {
            decimal.TryParse(Value.Value, out var value);
            return value;
        }
    }
}