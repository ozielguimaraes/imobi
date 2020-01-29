using Imobi.Validations.Base.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Imobi.Validations.Base
{
    public class ValidableObject<T> : ExtendedBindableObject, IValidity
    {
        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public string Placeholder
        {
            get { return _placeholder; }
            set
            {
                _placeholder = value;
                RaisePropertyChanged(() => Placeholder);
            }
        }

        public List<IValidationRule<T>> Validations { get; }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        private List<string> _errors;
        private bool _isValid;
        private bool _isVisible;
        private string _placeholder;
        private T _value;

        public ValidableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            Validations = new List<IValidationRule<T>>();
            _isVisible = false;
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            IsVisible = !IsValid;

            return this.IsValid;
        }
    }
}