using Imobi.Attributes;
using Imobi.Enums;
using Imobi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imobi.ViewModels
{
    public class ProposalFormViewModel : BaseViewModel
    {
        public ProposalFormViewModel()
        {
            //LoadPickers();
        }

        private string cpf;

        public string Cpf
        {
            get { return cpf; }
            set { SetProperty(ref cpf, value); }
        }

        private string _fullName;

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

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { SetProperty(ref _birthDate, value); }
        }

        private EnumValueDataAttribute _scholarity;

        public EnumValueDataAttribute Scholarity
        {
            get { return _scholarity; }
            set { SetProperty(ref _scholarity, value); }
        }

        private EnumValueDataAttribute _genre;

        public EnumValueDataAttribute Genre
        {
            get { return _genre; }
            set { SetProperty(ref _genre, value); }
        }

        private EnumValueDataAttribute _maritalStatus;

        public EnumValueDataAttribute MaritalStatus
        {
            get { return _maritalStatus; }
            set { SetProperty(ref _maritalStatus, value); }
        }

        private string _nationality;

        public string Nationality
        {
            get { return _nationality; }
            set { SetProperty(ref _nationality, value); }
        }

        private string _placeOfBirth;

        public string PlaceOfBirth
        {
            get { return _placeOfBirth; }
            set { SetProperty(ref _placeOfBirth, value); }
        }

        private decimal _fgtsValue;

        public decimal FgtsValue
        {
            get { return _fgtsValue; }
            set { SetProperty(ref _fgtsValue, value); }
        }

        private string _fathersName;

        public string FathersName
        {
            get { return _fathersName; }
            set { SetProperty(ref _fathersName, value); }
        }

        private string _mothersName;

        public string MothersName
        {
            get { return _mothersName; }
            set { SetProperty(ref _mothersName, value); }
        }

        private EnumValueDataAttribute _documentType;

        public EnumValueDataAttribute DocumentType
        {
            get { return _documentType; }
            set { SetProperty(ref _documentType, value); }
        }

        private string _documentNumber;

        public string DocumentNumber
        {
            get { return _documentNumber; }
            set { SetProperty(ref _documentNumber, value); }
        }

        private string _dispatchingAgency;

        public string DispatchingAgency
        {
            get { return _dispatchingAgency; }
            set { SetProperty(ref _dispatchingAgency, value); }
        }

        private DateTime _issueDate;

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set { SetProperty(ref _issueDate, value); }
        }

        private string _professionalCategory;

        public string ProfessionalCategory
        {
            get { return _professionalCategory; }
            set { SetProperty(ref _professionalCategory, value); }
        }

        private int _numberOfDependents;

        public int NumberOfDependents
        {
            get { return _numberOfDependents; }
            set { SetProperty(ref _numberOfDependents, value); }
        }

        private string _shortName;

        public string ShortName
        {
            get { return _shortName; }
            set { SetProperty(ref _shortName, value); }
        }

        private List<EnumValueDataAttribute> _maritalStatusList;

        public List<EnumValueDataAttribute> MaritalStatusList
        {
            get { return _maritalStatusList; }
            set { SetProperty(ref _maritalStatusList, value); }
        }

        private List<EnumValueDataAttribute> _documentTypeList;

        public List<EnumValueDataAttribute> DocumentTypeList
        {
            get { return _documentTypeList; }
            set { SetProperty(ref _documentTypeList, value); }
        }

        private List<EnumValueDataAttribute> _scholarityList;

        public List<EnumValueDataAttribute> ScholarityList
        {
            get { return _scholarityList; }
            set { SetProperty(ref _scholarityList, value); }
        }

        private List<EnumValueDataAttribute> _genreList;

        public List<EnumValueDataAttribute> GenreList
        {
            get { return _genreList; }
            set { SetProperty(ref _genreList, value); }
        }

        public void LoadPickers()
        {
            MaritalStatusList = EnumExtension.ConvertToList<MaritalStatusEnum>();
            DocumentTypeList = EnumExtension.ConvertToList<DocumentTypeEnum>();
            ScholarityList = EnumExtension.ConvertToList<ScholarityEnum>();
            GenreList = EnumExtension.ConvertToList<GenreEnum>();
        }
    }
}