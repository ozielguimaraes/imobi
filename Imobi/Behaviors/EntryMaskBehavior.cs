using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Imobi.Enums;
using Xamarin.Forms;

namespace Imobi.Behaviors
{
    public class EntryMaskBehavior : Behavior<Entry>
    {
        public int MaxLength { get; set; }
        public bool Formatted { get; set; }
        internal const int LENGTH_CPF = 11;
        internal const int LENGTH_CNPJ = 14;
        internal const int LENGTH_DATE = 8;
        internal const int LENGTH_DATE_CARTAO_CREDITO = 6;
        internal const int LENGTH_PHONE_SEM_MASCARA_10 = 10;
        internal const int LENGTH_PHONE_SEM_MASCARA_11 = 11;
        internal const int LENGTH_PHONE_SEM_MASCARA_14 = 14;
        internal const int LENGTH_DECIMAL = 3;

        private string _mask = string.Empty;

        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                SetPositions();
            }
        }

        public static readonly BindableProperty TypeProperty =
     BindableProperty.Create(nameof(Type), typeof(BehaviorTypeEnum), typeof(EntryMaskBehavior), BehaviorTypeEnum.None, propertyChanged: (bindableObject, oldValue, newValue) =>
     {
         var customView = bindableObject as EntryMaskBehavior;
         customView.Type = (BehaviorTypeEnum)newValue;
     });

        public BehaviorTypeEnum Type
        {
            get { return (BehaviorTypeEnum)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private IDictionary<int, char> _positions;

        private void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != 'X') list.Add(i, Mask[i]);

            _positions = list;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var entryText = args?.NewTextValue;
            var entryLength = entryText?.Length;
            var text = entry?.Text;

            decimal entryVal;
            switch (Type)
            {
                case BehaviorTypeEnum.CPFCNPJ:
                    if (text?.Length < 14)
                    {
                        text = text?.Replace("/", "");
                        text = text?.Replace("-", "");
                        text = text?.Replace(".", "");

                        _mask = "XXX.XXX.XXX-XX";
                        SetPositions();
                    }

                    //Check if it is CNPJ and change the mask
                    if (text?.Length == 18)
                    {
                        text = text?.Replace("-", "");
                        text = text?.Replace(".", "");
                        _mask = "XX.XXX.XXX/XXXX-XX";
                        SetPositions();
                    }
                    break;

                case BehaviorTypeEnum.CPF:
                    text = text?.Replace("/", "");
                    text = text?.Replace("-", "");
                    text = text?.Replace(".", "");

                    _mask = "XXX.XXX.XXX-XX";
                    SetPositions();

                    break;

                case BehaviorTypeEnum.CNPJ:
                    if (entryLength == LENGTH_CNPJ && !Formatted)
                    {
                        entryVal = Convert.ToUInt64(entryText);
                        entryText = entryVal.ToString(@"00\.000\.000\/0000\-00");
                        Formatted = true;
                    }
                    else if (entryText?.Length > MaxLength)
                    {
                        entryText = entryText.Remove(entryText.Length - 1);
                    }
                    else if (entryText?.Length < MaxLength && Formatted)
                    {
                        Formatted = false;
                    }

                    entry.Text = entryText;
                    entry.TextColor = entry.Text?.Length < MaxLength ? Color.Red : Color.Black;

                    break;

                case BehaviorTypeEnum.Phone:
                    if ((entryLength == LENGTH_PHONE_SEM_MASCARA_11 || entryLength == LENGTH_PHONE_SEM_MASCARA_14))
                    {
                        entryVal = Convert.ToUInt64(entryText);
                        entryText = string.Format("{0:(##) ####-####}", entryVal);
                        Formatted = true;
                    }
                    else if (entryLength == 15)
                    {
                        entryVal = Convert.ToUInt64(entryText);
                        entryText = string.Format("{0:(##) #####-####}", entryVal);
                        Formatted = true;
                    }
                    else if (entryText?.Length > MaxLength)
                    {
                        entryText = entryText.Remove(entryText.Length - 1);
                    }
                    else if (entryText?.Length < LENGTH_PHONE_SEM_MASCARA_14 && Formatted)
                    {
                        Formatted = false;
                    }

                    entry.Text = entryText;
                    entry.TextColor = entry.Text?.Length < LENGTH_PHONE_SEM_MASCARA_11 ? Color.Red : Color.Black;

                    break;

                case BehaviorTypeEnum.Date:
                    if (entryLength == LENGTH_DATE && !Formatted)
                    {
                        entryText = Convert.ToUInt64(entryText).ToString(@"00/00/0000");
                        Formatted = true;
                    }
                    else if (entryText?.Length > MaxLength)
                    {
                        entryText = entryText.Remove(entryText.Length - 1);
                    }
                    else if (entryText?.Length < MaxLength && Formatted)
                    {
                        Formatted = false;
                    }

                    entry.Text = entryText;
                    entry.TextColor = entry.Text?.Length < MaxLength ? Color.Red : Color.Black;

                    break;

                case BehaviorTypeEnum.CreditCardExpirationDate:
                    if (entryLength == LENGTH_DATE_CARTAO_CREDITO && !Formatted)
                    {
                        entryText = Convert.ToUInt64(entryText).ToString(@"00/0000");
                        Formatted = true;
                    }
                    else if (entryText?.Length > MaxLength)
                    {
                        entryText = entryText.Remove(entryText.Length - 1);
                    }
                    else if (entryText?.Length < MaxLength && Formatted)
                    {
                        Formatted = false;
                    }

                    entry.Text = entryText;
                    entry.TextColor = entry.Text?.Length < MaxLength ? Color.Red : Color.Black;

                    break;

                case BehaviorTypeEnum.NumbersOnly:
                    if (entryText != args.OldTextValue)
                    {
                        entry.Text = Regex.Replace(entryText, Constants.Constants.Expressions.NumbersOnly, string.Empty);
                    }
                    break;

                case BehaviorTypeEnum.Decimal:
                    if (entryText != args.OldTextValue)
                    {
                        if (string.IsNullOrEmpty(entryText) || string.IsNullOrEmpty(args.OldTextValue)) return;
                        var nvalue = Convert.ToDouble(entryText, CultureInfo.InvariantCulture);
                        var ovalue = Convert.ToDouble(args.OldTextValue, CultureInfo.InvariantCulture) / 100;
                        if (nvalue == ovalue) return;
                        var valor = Convert.ToDecimal(Regex.Replace(entryText, Constants.Constants.Expressions.NumbersOnly, string.Empty), CultureInfo.InvariantCulture);
                        valor /= 100;
                        entry.Text = valor.ToString($"n{LENGTH_DECIMAL}");
                    }

                    break;

                case BehaviorTypeEnum.ZipCode:
                    _mask = "XXXXX-XXX";
                    SetPositions();

                    break;
            }

            if (_mask == "XX.XXX.XXX/XXXX-XX")
            {
                if (text?.Length >= 9 && text?.Length <= 14)
                {
                    text = text.Replace("/", "");
                    text = text.Replace("-", "");
                    text = text.Replace(".", "");
                    _mask = "XXX.XXX.XXX-XX";
                    SetPositions();
                }
            }

            //Check if it is CNPJ and change the mask
            if (_mask == "XXX.XXX.XXX-XX")
            {
                if (text?.Length == 15)
                {
                    text = text.Replace("-", "");
                    text = text.Replace(".", "");
                    _mask = "XX.XXX.XXX/XXXX-XX";
                    SetPositions();
                }
            }

            //Mask for monetary values
            if (_mask == "XXXXXXXXXX")
            {
                if (text is null) return;

                text = CurrencyNumberValueConverter(text);
            }

            if (string.IsNullOrWhiteSpace(text) || _positions is null) return;

            if (text.Length > _mask.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;
        }

        private static string CurrencyNumberValueConverter(string text)
        {
            var numbers = Regex.Replace(text, @"\D", "");
            numbers = string.Format(new System.Globalization.CultureInfo("pt-BR"), "{0:N}", Convert.ToDecimal(numbers) / 100);
            return numbers;
        }
    }
}