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
    public class BaseViewModel : ExtendedBindableObject, INotifyPropertyChanged
    {
        public readonly INavigationService NavigationService;
        protected readonly IEventTrackerService EventTrackerService;
        protected readonly IExceptionService ExceptionService;
        protected readonly IMessageService MessageService;

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

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private bool isBusy = false;

        private string title = string.Empty;

        public BaseViewModel()
        {
            ExceptionService = Bootstraper.Resolve<IExceptionService>();
            MessageService = Bootstraper.Resolve<IMessageService>();
            NavigationService = Bootstraper.Resolve<INavigationService>();
            EventTrackerService = DependencyService.Get<IEventTrackerService>();
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }

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
    }
}