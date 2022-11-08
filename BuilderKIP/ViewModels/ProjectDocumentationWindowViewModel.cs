using BuilderKIP.Models;
using BuilderKIP.ViewModels.ProjectDocumentationPages;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Reactive;
using System.Runtime.Serialization;

namespace BuilderKIP.ViewModels
{
    public interface IWindowContainerProjectDocumentation
    {
        public void GoToEstimate(Contract contract);
        public void GoToStart(Contract contract);
        public void Save(Contract contract);
    }



    [DataContract]
    public class ProjectDocumentationWindowViewModel : ReactiveObject, IScreen, IWindowContainerProjectDocumentation
    {
        #region IWindowContainerProjectDocumentation

        public void GoToEstimate(Contract contract)
        {
            Router.Navigate.Execute(new EstimateDocumentationUserControlViewModel(contract, this));
        }

        public void GoToStart(Contract contract)
        {
            Router.Navigate.Execute(new MainDocumentationUserControlViewModel(contract, this));
        }

        public void Save(Contract contract)
        {
            Contract = contract;

            TextSum = $"Итог: ${contract.BuildingServiceContract.PriceEstimate}.00";
            foreach (var item in contract.BuildingServiceContract.Materials)
            {
                Materials.Add(item);
            }
            TextTypeClimaticCondition =  $"Тип климата: {contract.BuildingServiceContract?.TypeClimaticCondition?.Name}";
            TextTypeGround =  $"Тип грунта: {contract.BuildingServiceContract?.TypeGround?.Name}";
            TextTypeRelief =  $"Тип рельефа: {contract.BuildingServiceContract?.TypeRelief?.Name}";

            RouterVisible = false;
        }
        #endregion

        #region Fields
        
        
        
        
        private RoutingState _router = new RoutingState();
        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        public ReactiveCommand<Unit, Contract?> SaveCommand { get; }

        public Contract? Contract { get; set; }

        public bool _routerVisible;
        public bool RouterVisible 
        {
            get => _routerVisible;
            set => this.RaiseAndSetIfChanged(ref _routerVisible, value);
        }


        public string _textTypeRelief;
        public string TextTypeRelief
        {
            get => _textTypeRelief;
            set => this.RaiseAndSetIfChanged(ref _textTypeRelief, value);
        } 
        
        public string _textTypeGround;
        public string TextTypeGround
        {
            get => _textTypeGround;
            set => this.RaiseAndSetIfChanged(ref _textTypeGround, value);
        } 
        
        public string _textTypeClimaticCondition;
        public string TextTypeClimaticCondition
        {
            get => _textTypeClimaticCondition;
            set => this.RaiseAndSetIfChanged(ref _textTypeClimaticCondition, value);
        }

        private ObservableCollection<BuildingServiceMaterial> _materials = new ObservableCollection<BuildingServiceMaterial>();

        public ObservableCollection<BuildingServiceMaterial> Materials
        {
            get => _materials;
            set => this.RaiseAndSetIfChanged(ref _materials, value);
        }

        private string _textSum;

        public string TextSum
        {
            get => _textSum;
            set => this.RaiseAndSetIfChanged(ref _textSum, value);
        }

        #endregion


        public ProjectDocumentationWindowViewModel(Contract contract)
        {
            RouterVisible = true;
            GoToStart(contract);

            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                return Contract;
            });
        }
    }
}
