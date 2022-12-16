using BuilderKIP.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace BuilderKIP.ViewModels
{

    [DataContract]
    public class ContractDetailsViewModel : ReactiveObject, IScreen
    {

        private RoutingState _router = new RoutingState();
        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        private ObservableCollection<Stage> _stages = new ObservableCollection<Stage>();

        public ObservableCollection<Stage> Stages
        {
            get => _stages;
            set => this.RaiseAndSetIfChanged(ref _stages, value);
        }

        private ObservableCollection<string> _contractInfo = new ObservableCollection<string>();

        public ObservableCollection<string> ContractInfo
        {
            get => _contractInfo;
            set => this.RaiseAndSetIfChanged(ref _contractInfo, value);
        }

        private ObservableCollection<BuildingServiceMaterial> _materials = new ObservableCollection<BuildingServiceMaterial>();

        public ObservableCollection<BuildingServiceMaterial> Materials
        {
            get => _materials;
            set => this.RaiseAndSetIfChanged(ref _materials, value);
        }

        
       public ICommand AcceptContract { get; set; }
       public ICommand ToPay { get; set; }

        public ContractDetailsViewModel(Contract contract, Client client)
        {
            if(contract.BuildingServiceContract.Materials != null)
            {
                foreach (var item in contract.BuildingServiceContract.Materials)
                    Materials.Add(item);
            }

            if (contract.BuildingServiceContract.Stages != null)
            {
                foreach (var item in contract.BuildingServiceContract.Stages)
                    Stages.Add(item);
            }



            AcceptContract = ReactiveCommand.Create(() =>
            {
                if(contract.ContractStatus == ContractStatus.NOT_ACCEPT)
                {
                    contract.ContractStatus = ContractStatus.ACCEPT;
                    API.Client.Update(contract);
                }
            });

            ToPay = ReactiveCommand.Create(() =>
            {
                int sum = contract.GetSum();
                if (sum <= client.Balance)
                {
                    client.Balance = client.Balance - sum;
                    API.Client.Update(client);
                    contract.ContractStatus = ContractStatus.COMPLETED;
                    API.Client.Update(contract);
                }
            });

            ContractInfo = contract.GetContractInfo();
        }
    }
}
