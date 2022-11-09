using BuilderKIP.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace BuilderKIP.ViewModels.Production
{
    public class ProductionDiaryStagesWindowViewModel : ReactiveObject, IScreen
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

        public ICommand Action { get; private set; }

        private ObservableCollection<Stage> _stages = new ObservableCollection<Stage>();

        public ObservableCollection<Stage> Stages
        {
            get => _stages;
            set => this.RaiseAndSetIfChanged(ref _stages, value);
        }

        private Stage _selectedStage;

        public Stage SelectedStage
        { 
            get => _selectedStage;
            set => this.RaiseAndSetIfChanged(ref _selectedStage, value);
        }

        public ProductionDiaryStagesWindowViewModel(Contract contract)
        {
            if (contract.BuildingServiceContract.Stages != null)
            {
                foreach (var item in contract.BuildingServiceContract.Stages)
                    Stages.Add(item);
            }

            Action = ReactiveCommand.Create(() =>
            {
                if(SelectedStage != null)
                {
                    SelectedStage.Status = "Выполнен";
                    SelectedStage.Ellipse = "green";

                    API.Client.Update(SelectedStage);
                }
            });
        }

    }
}
