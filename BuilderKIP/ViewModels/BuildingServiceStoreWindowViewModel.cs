using BuilderKIP.Models;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;

namespace BuilderKIP.ViewModels
{
    public class BuildingServiceStoreWindowViewModel : ViewModelBase
    {

        private ObservableCollection<BuildingService> _buildingServices = new ObservableCollection<BuildingService>();

        public ObservableCollection<BuildingService> BuildingServices
        {
            get => _buildingServices;
            set => this.RaiseAndSetIfChanged(ref _buildingServices, value);
        }

        private BuildingService _selectedBuildingService;

        public BuildingService SelectedBuildingService
        {
            get => _selectedBuildingService;
            set => this.RaiseAndSetIfChanged(ref _selectedBuildingService, value);
        }
        public ReactiveCommand<Unit, BuildingService?> SaveCommand { get; }
     
        public BuildingServiceStoreWindowViewModel()
        {

            var list = API.Client.Get<BuildingService>();

            BuildingServices = new(list);

          /*  BuildingServices.Add(new BuildingService{
                Name = "Построить дом",
                Price = 100000,
                Description = "Строительство котеджа",
                BuildPhases = new List<BuildPhase>
                {
                    new BuildPhase(){Name = "Заливка фундамента"},
                    new BuildPhase(){Name = "Возведение каркаса"},
                    new BuildPhase(){Name = "Постойка крыши"},
                    new BuildPhase(){Name = "Утепление"},
                    new BuildPhase(){Name = "Внешняя отделка"},
                }
            });
            BuildingServices.Add(new BuildingService{Name = "Посторить баню"});
            BuildingServices.Add(new BuildingService{Name = "Построить сарай"});*/


            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                return SelectedBuildingService;
            });
        }
    }
}
