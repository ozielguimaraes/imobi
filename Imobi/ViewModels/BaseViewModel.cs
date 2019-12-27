using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Imobi.Models;
using Imobi.Services;
using System.Threading.Tasks;
using Imobi.IoC;
using Imobi.Services.Interfaces;
using Xamarin.Essentials;
using Imobi.Validations;

namespace Imobi.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Public Properties

        public string AppVersion
        {
            get { return VersionTracking.CurrentVersion; }
        }

        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public INavigationService NavigationService { get; private set; }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #endregion Public Properties



        #region Protected Properties

        protected IExceptionService ExceptionService { get; private set; }
        protected IMessageService MessageService { get; private set; }

        #endregion Protected Properties



        #region Private Fields + Structs

        private bool isBusy = false;

        private string title = string.Empty;

        #endregion Private Fields + Structs

        #region Public Constructors + Destructors

        public BaseViewModel()
        {
            ExceptionService = Bootstraper.Resolve<IExceptionService>();
            MessageService = Bootstraper.Resolve<IMessageService>();
            NavigationService = Bootstraper.Resolve<INavigationService>();
        }

        #endregion Public Constructors + Destructors



        #region Public Methods

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }

        #endregion Public Methods



        #region Protected Methods

        protected bool SetProperty<T>(ref T backingStore, T value,
                    [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion Protected Methods

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed is null) return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}