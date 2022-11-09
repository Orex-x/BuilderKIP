using BuilderKIP.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

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

        public ICommand OpenDetailsContract { get; private set; }

        private Contract _selectedContract;

        public Contract SelectedContract
        {
            get => _selectedContract;
            set => this.RaiseAndSetIfChanged(ref _selectedContract, value);
        }

        public Interaction<ContractDetailsViewModel, Contract?> ShowDialog { get; }

        public ContractsPageViewModel(Client client)
        {
            Contracts = new(API.Client.Get<Contract>().Where(x => x.Client.Id == client.Id));

            ShowDialog = new();

            OpenDetailsContract = ReactiveCommand.Create(async () =>
            {
                if (SelectedContract != null)
                {
                    var store = new ContractDetailsViewModel(SelectedContract, client);
                    var result = await ShowDialog.Handle(store);
                    if (result != null)
                    {

                    }
                }
            });

        }
    }
}
