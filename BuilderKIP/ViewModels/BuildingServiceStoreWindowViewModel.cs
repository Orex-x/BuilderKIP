using BuilderKIP.Models;
using ReactiveUI;
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
            BuildingServices = new(API.Client.Get<BuildingService>());

            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                return SelectedBuildingService;
            });
        }
    }
}
