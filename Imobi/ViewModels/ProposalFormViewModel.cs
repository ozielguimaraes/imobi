using Imobi.Attributes;
using Imobi.Enums;
using Imobi.Extensions;
using Imobi.Validations.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imobi.ViewModels
{
    public class ProposalFormViewModel : BaseViewModel
    {
        public decimal FgtsFinalValue { get; private set; }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { SetProperty(ref _birthDate, value); }
        }

        public string BirthDateString
        {
            get { return _birthDateString; }
            set
            {
                _birthDateString = value;
                if (value?.Length == 10)
                {
                    if (DateTime.TryParse(value, out _birthDate))
                        OnPropertyChanged(nameof(BirthDate));
                }
            }
        }

        public string Cpf
        {
            get { return cpf; }
            set { SetProperty(ref cpf, value); }
        }

        public string DispatchingAgency
        {
            get { return _dispatchingAgency; }
            set { SetProperty(ref _dispatchingAgency, value); }
        }

        public string DocumentNumber
        {
            get { return _documentNumber; }
            set { SetProperty(ref _documentNumber, value); }
        }

        public EnumValueDataAttribute DocumentType
        {
            get { return _documentType; }
            set { SetProperty(ref _documentType, value); }
        }

        public List<EnumValueDataAttribute> DocumentTypeList
        {
            get { return _documentTypeList; }
            set { SetProperty(ref _documentTypeList, value); }
        }

        public string FathersName
        {
            get { return _fathersName; }
            set { SetProperty(ref _fathersName, value); }
        }

        public ValidableObject<string> FgtsValue
        {
            get { return _fgtsValue; }
            set { SetProperty(ref _fgtsValue, value); }
        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged();
                ShortName = value?.Split(' ').FirstOrDefault() ?? string.Empty;
            }
        }

        public EnumValueDataAttribute Genre
        {
            get { return _genre; }
            set { SetProperty(ref _genre, value); }
        }

        public List<EnumValueDataAttribute> GenreList
        {
            get { return _genreList; }
            set { SetProperty(ref _genreList, value); }
        }

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set { SetProperty(ref _issueDate, value); }
        }

        public string IssueDateString
        {
            get { return _issueDateString; }
            set
            {
                _issueDateString = value;
                if (value?.Length == 10)
                {
                    if (DateTime.TryParse(value, out _issueDate))
                        OnPropertyChanged(nameof(IssueDate));
                }
            }
        }

        public EnumValueDataAttribute MaritalStatus
        {
            get { return _maritalStatus; }
            set { SetProperty(ref _maritalStatus, value); }
        }

        public List<EnumValueDataAttribute> MaritalStatusList
        {
            get { return _maritalStatusList; }
            set { SetProperty(ref _maritalStatusList, value); }
        }

        public string MothersName
        {
            get { return _mothersName; }
            set { SetProperty(ref _mothersName, value); }
        }

        public string Nationality
        {
            get { return _nationality; }
            set { SetProperty(ref _nationality, value); }
        }

        public int NumberOfDependents
        {
            get { return _numberOfDependents; }
            set { SetProperty(ref _numberOfDependents, value); }
        }

        public string NumberOfDependentsString
        {
            get { return _numberOfDependentsString; }
            set
            {
                _numberOfDependentsString = value;
                if (!string.IsNullOrEmpty(_numberOfDependentsString))
                {
                    if (int.TryParse(value, out _numberOfDependents))
                        OnPropertyChanged(nameof(NumberOfDependents));
                }
            }
        }

        public string PlaceOfBirth
        {
            get { return _placeOfBirth; }
            set { SetProperty(ref _placeOfBirth, value); }
        }

        public string ProfessionalCategory
        {
            get { return _professionalCategory; }
            set { SetProperty(ref _professionalCategory, value); }
        }

        public EnumValueDataAttribute Scholarity
        {
            get { return _scholarity; }
            set { SetProperty(ref _scholarity, value); }
        }

        public List<EnumValueDataAttribute> ScholarityList
        {
            get { return _scholarityList; }
            set { SetProperty(ref _scholarityList, value); }
        }

        public string ShortName
        {
            get { return _shortName; }
            set { SetProperty(ref _shortName, value); }
        }

        private DateTime _birthDate;
        private string _birthDateString;
        private string _dispatchingAgency;
        private string _documentNumber;
        private EnumValueDataAttribute _documentType;
        private List<EnumValueDataAttribute> _documentTypeList;
        private string _fathersName;
        private ValidableObject<string> _fgtsValue = new ValidableObject<string>();
        private string _fullName;
        private EnumValueDataAttribute _genre;
        private List<EnumValueDataAttribute> _genreList;
        private DateTime _issueDate;
        private string _issueDateString;
        private EnumValueDataAttribute _maritalStatus;
        private List<EnumValueDataAttribute> _maritalStatusList;
        private string _mothersName;
        private string _nationality;
        private int _numberOfDependents;
        private string _numberOfDependentsString;
        private string _placeOfBirth;
        private string _professionalCategory;
        private EnumValueDataAttribute _scholarity;
        private List<EnumValueDataAttribute> _scholarityList;
        private string _shortName;
        private string cpf;

        public void LoadPickers()
        {
            MaritalStatusList = EnumExtension.ConvertToList<MaritalStatusEnum>();
            DocumentTypeList = EnumExtension.ConvertToList<DocumentTypeEnum>();
            ScholarityList = EnumExtension.ConvertToList<ScholarityEnum>();
            GenreList = EnumExtension.ConvertToList<GenreEnum>();
        }

        internal void FillFgtsFinalValueProperty()
        {
            FgtsFinalValue = GetFgtsValue();
        }

        public decimal GetFgtsValue()
        {
            decimal.TryParse(FgtsValue.Value, out var value);
            return value;
        }
    }
}
