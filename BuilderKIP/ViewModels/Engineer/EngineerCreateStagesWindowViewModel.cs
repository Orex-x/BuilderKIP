using Avalonia.Media.TextFormatting;
using BuilderKIP.Models;
using BuilderKIP.ViewModels.ProjectDocumentationPages;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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
        
        private ObservableCollection<StagesViewModel> _stagesViewModels = new ObservableCollection<StagesViewModel>();

        public ObservableCollection<StagesViewModel> StagesViewModels
        {
            get => _stagesViewModels;
            set => this.RaiseAndSetIfChanged(ref _stagesViewModels, value);
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

                StagesViewModels.Add(new StagesViewModel(stage));
            });
        }
    }
}
