using BuilderKIP.Models;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace BuilderKIP.ViewModels.Engineer
{

    public class EngineerCreateStagesWindowViewModel : ReactiveObject, IScreen
    {
        #region IWindowContainerProjectDocumentation

        private RoutingState _router = new RoutingState();
        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        #endregion

        public ReactiveCommand<Unit, Contract?> SaveCommand { get; }

        public ICommand OnClickAddStagesViewModel { get; private set; }
        
        private ObservableCollection<Stage> _stages = new ObservableCollection<Stage>();

        public ObservableCollection<Stage> Stages
        {
            get => _stages;
            set => this.RaiseAndSetIfChanged(ref _stages, value);
        }


        public EngineerCreateStagesWindowViewModel(Contract contract)
        {
            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                return contract;
            });

            OnClickAddStagesViewModel = ReactiveCommand.Create(() =>
            {
                var stage = new Stage()
                {
                    ActionName = "Отметить как выполненое",
                    Ellipse = "red",
                    Status = "Не выполнен",
                    BuildingServiceContractId = contract.BuildingServiceContract.Id
                };

                if (contract.BuildingServiceContract.Stages == null) 
                    contract.BuildingServiceContract.Stages = new List<Stage>();

                contract.BuildingServiceContract.Stages.Add(stage);

                Stages.Add(stage);
            });
        }
    }
}
