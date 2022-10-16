using BuilderKIP.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderKIP.ViewModels
{
    public class ContractsPageViewModel : ViewModelBase
    {
        private ObservableCollection<Contract> _contracts = new ObservableCollection<Contract>();

        public ObservableCollection<Contract> Contracts
        {
            get => _contracts;
            set => this.RaiseAndSetIfChanged(ref _contracts, value);
        }


        public ContractsPageViewModel()
        {
            Contracts.Add(new Contract
            {
                DateTimeNew = DateTime.Now,
                Number = "Договор №123fg-a312"
            });
            Contracts.Add(new Contract
            {
                DateTimeNew = DateTime.Now,
                Number = "Договор №13298dk-lkadf2"
            });
            Contracts.Add(new Contract
            {
                DateTimeNew = DateTime.Now,
                Number = "Договор №fad9876-987asdf"
            });
            Contracts.Add(new Contract
            {
                DateTimeNew = DateTime.Now,
                Number = "Договор №bd-0-adfc546"
            });
        }
    }
}
