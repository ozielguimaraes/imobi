using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class ProposalFlowViewModel : BaseViewModel
    {
        public ICommand LoadPreKeysCommand => new Command(async () => await LoadPreKeys());
        public ICommand NewPreKeyCommand => new Command(async () => await AddNewPreKey());
        public ICommand DeletePreKeyCommand => new Command<PreKeyViewModel>(async (item) => await DeletePreChaveAsync(item));

        private ObservableCollection<PreKeyViewModel> _preKeys = new ObservableCollection<PreKeyViewModel>();

        public ObservableCollection<PreKeyViewModel> PreKeys
        {
            get { return _preKeys; }
            set
            {
                _preKeys = value;
                RaisePropertyChanged(() => PreKeys);
            }
        }

        private string _venture = "Selecione ...";

        public string Venture
        {
            get { return _venture; }
            set { SetProperty(ref _venture, value); }
        }

        private ObservableCollection<string> _ventureList;

        public ObservableCollection<string> VentureList
        {
            get { return _ventureList; }
            set { SetProperty(ref _ventureList, value); }
        }

        private string _tower;

        public string Tower
        {
            get { return _tower; }
            set { SetProperty(ref _tower, value); }
        }

        private ObservableCollection<string> _towerList;

        public ObservableCollection<string> TowerList

        {
            get { return _towerList; }
            set { SetProperty(ref _towerList, value); }
        }

        private int _unity;

        public int Unity
        {
            get { return _unity; }
            set { SetProperty(ref _unity, value); }
        }

        private ObservableCollection<int> _unitList;

        public ObservableCollection<int> UnitList
        {
            get { return _unitList; }
            set { SetProperty(ref _unitList, value); }
        }

        private decimal _saleValue;

        public decimal SaleValue
        {
            get { return _saleValue; }
            set { SetProperty(ref _saleValue, value); }
        }

        private PreKeyViewModel _newPreKey = new PreKeyViewModel();

        public PreKeyViewModel NewPreKey
        {
            get { return _newPreKey; }
            set { SetProperty(ref _newPreKey, value); }
        }

        private async Task LoadPreKeys()
        {
            if (IsBusy) return;

            IsBusy = true;
            await Task.Delay(500);

            try
            {
                PreKeys.Clear();

                foreach (var item in MockValues())
                {
                    PreKeys.Add(item);
                }
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex, nameof(ProposalFlowViewModel), nameof(LoadPreKeys));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddNewPreKey()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                if (NewPreKey.NumberOfInstallments <= 0)
                {
                    await MessageService.ShowAsync("Campo quantidade é obrigatório");
                    return;
                }
                if (NewPreKey.Value.Value == 0)
                {
                    await MessageService.ShowAsync("Campo valor é obrigatório");
                    return;
                }
                PreKeys.Add(NewPreKey);
                NewPreKey = new PreKeyViewModel();
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex, nameof(ProposalFlowViewModel), nameof(AddNewPreKey));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeletePreChaveAsync(PreKeyViewModel item)
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                //TODO Deletar do servidor/api?
                PreKeys.Remove(item);

                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex, nameof(ProposalFlowViewModel), nameof(DeletePreChaveAsync));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private ObservableCollection<PreKeyViewModel> MockValues()
        {
            VentureList = new ObservableCollection<string>
            {
                "Prime Village",
                "Jardim América",
                "Green Palace"
            };
            return new ObservableCollection<PreKeyViewModel>
            {
                //new PreKeyViewModel
                //{
                //    NumberOfInstallments = 180,
                //    Value = 180000,
                //    FirstExpirationDate = DateTime.Now
                //}
            };
        }
    }
}