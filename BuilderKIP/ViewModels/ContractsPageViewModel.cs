using BuilderKIP.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;

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


        public ContractsPageViewModel(Client client)
        {
            Contracts = new(API.Client.Get<Contract>().Where(x => x.Client.Id == client.Id));

        }
    }
}
