﻿using Imobi.Dtos;
using Imobi.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class ProposalListViewModel : BaseViewModel
    {
        public ProposalListViewModel()
        {
            Title = "Propostas";
        }

        public ObservableCollection<ProposalDto> Items { get; set; } = new ObservableCollection<ProposalDto>();
        public ICommand LoadItemsCommand => new Command(async () => await LoadItems());

        private async Task LoadItems()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var mock = new MockProposal();
                var items = await mock.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}