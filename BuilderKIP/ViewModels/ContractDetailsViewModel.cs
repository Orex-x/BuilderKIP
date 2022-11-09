using Avalonia.Controls.Shapes;
using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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

        private ObservableCollection<StagesViewModel> _stagesViewModels = new ObservableCollection<StagesViewModel>();

        public ObservableCollection<StagesViewModel> Stages
        {
            get => _stagesViewModels;
            set => this.RaiseAndSetIfChanged(ref _stagesViewModels, value);
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

        public ContractDetailsViewModel(Contract contract)
        {
            if(contract.BuildingServiceContract.Materials != null)
            {
                foreach (var item in contract.BuildingServiceContract.Materials)
                    Materials.Add(item);
            }

            if (contract.BuildingServiceContract.Stages != null)
            {
                foreach (var item in contract.BuildingServiceContract.Stages)
                    Stages.Add(new StagesViewModel(item));
            }



            AcceptContract = ReactiveCommand.Create(() =>
            {
                if(contract.ContractStatus == ContractStatus.NOT_ACCEPT)
                {
                    contract.ContractStatus = ContractStatus.ACCEPT;
                    API.Client.Update(contract);
                }
            });

            ContractInfo = contract.GetContractInfo();
        }
    }
}
