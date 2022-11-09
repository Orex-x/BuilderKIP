using Avalonia.Controls.Shapes;
using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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

        public ReactiveCommand<Unit, Contract?> SaveCommand { get; }

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


        public ContractDetailsViewModel(Contract contract)
        {

            var svm = new StagesViewModel()
            {
                Name = "Этап 1",
                ActionName = "Принять",
                Status = "Статус",
                Ellipse = "green"
            };

            svm.Action = ReactiveCommand.Create(() =>
            {
                svm.Ellipse = "red";
            });

            var svm2 = new StagesViewModel()
            {
                Name = "Этап 2",
                ActionName = "Принять",
                Status = "Статус",
                Ellipse = "green"
            };

            svm2.Action = ReactiveCommand.Create(() =>
            {
                svm2.Ellipse = "red";
            });


            var svm3 = new StagesViewModel()
            {
                Name = "Этап 2",
                ActionName = "Принять",
                Status = "Статус",
                Ellipse = "green"
            };

            svm3.Action = ReactiveCommand.Create(() =>
            {
                svm3.Ellipse = "red";
            });

            Stages.Add(svm);
            Stages.Add(svm2);
            Stages.Add(svm3);

            if(contract.BuildingServiceContract.Materials != null)
            {
                foreach (var item in contract.BuildingServiceContract.Materials)
                    Materials.Add(item);
            }
            
            

            ContractInfo = contract.GetContractInfo();

            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                return contract;
            });
        }
    }
}
