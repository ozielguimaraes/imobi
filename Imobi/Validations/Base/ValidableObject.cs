using Imobi.Validations.Base.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Imobi.Validations.Base
{
    public class ValidableObject<T> : ExtendedBindableObject, IValidity
    {
        #region Public Properties

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

        public List<IValidationRule<T>> Validations => _validations;

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

        #endregion Public Properties



        #region Private Fields + Structs

        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private bool _isValid;
        private bool _isVisible;
        private string _placeholder;
        private T _value;

        #endregion Private Fields + Structs

        #region Public Constructors + Destructors

        public ValidableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
            _isVisible = false;
        }

        #endregion Public Constructors + Destructors



        #region Public Methods

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            IsVisible = !IsValid;

            return this.IsValid;
        }

        #endregion Public Methods
    }
}