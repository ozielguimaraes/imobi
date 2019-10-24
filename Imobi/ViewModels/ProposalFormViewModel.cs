using Imobi.Attributes;
using System;
using System.Linq;

namespace Imobi.ViewModels
{
    public class ProposalFormViewModel : BaseViewModel
    {
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
            set { SetProperty(ref _fullName, value); }
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

        private EnumValueDataAttribute _tipoDocumento;

        public EnumValueDataAttribute TipoDocumento
        {
            get { return _tipoDocumento; }
            set { SetProperty(ref _tipoDocumento, value); }
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

        public string ShortName => FullName?.Split(' ').FirstOrDefault() ?? string.Empty;
    }
}