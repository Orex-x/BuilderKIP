using BuilderKIP.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows.Input;

namespace BuilderKIP.ViewModels
{
    public class ClientNewContractPageViewModel : ViewModelBase
    {

        #region Fields
        private ObservableCollection<BuildingService> _buildingServices = new ObservableCollection<BuildingService>();
        private ObservableCollection<BuildingServiceContract> _buildingServicesContract = new ObservableCollection<BuildingServiceContract>();

        public ObservableCollection<BuildingService> BuildingServices { get { return _buildingServices; } set { _buildingServices = value; } }
        public ObservableCollection<BuildingServiceContract> BuildingServicesContract { get { return _buildingServicesContract; } set { _buildingServicesContract = value; } }

        private BuildingServiceContract _selectedBuildingService;   
        public BuildingServiceContract SelectedBuildingService { get { return _selectedBuildingService; } set { _selectedBuildingService = value; } }

        private string _address;
        public string Address { get { return _address; } set { _address = value; } }

        private DateTimeOffset? _deadline;

        public DateTimeOffset? Deadline
        {
            get => _deadline;
            set => this.RaiseAndSetIfChanged(ref _deadline, value);
        }


        public Interaction<BuildingServiceStoreWindowViewModel, BuildingService?> ShowDialog { get; }
        #endregion


        public ICommand OnClickAddBuildingService { get; private set; }
        public ICommand OnClickDeleteBuildingService { get; private set; }
        public ICommand OnClickSaveContract { get; private set; }

        public ClientNewContractPageViewModel(Client client)
        {
            ShowDialog = new Interaction<BuildingServiceStoreWindowViewModel, BuildingService?>();

            OnClickAddBuildingService = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new BuildingServiceStoreWindowViewModel();

                var result = await ShowDialog.Handle(store);
                if(result != null)
                {
                    //чтобы для entity обьект был уникальным
                    BuildingServicesContract.Add(new BuildingServiceContract()
                    {
                        BuildingServiceId = result.Id,
                        BuildingService = result
                    });
                }
            });

            OnClickDeleteBuildingService = ReactiveCommand.Create(() =>
            {
                if(SelectedBuildingService != null)
                    BuildingServicesContract.Remove(SelectedBuildingService);
            });

            OnClickSaveContract = ReactiveCommand.Create(() =>
            {

                var rnd = new Random();
                string number = "Договор №" + rnd.Next(1000, 9999).ToString() + "-" + rnd.Next(1000, 9999).ToString();
                var contact = new Contract
                {
                    Address = Address,
                    ClientId = client.Id,
                    Number = number,
                    ContractStatus = ContractStatus.NEW,
                    DeadLine = ((DateTimeOffset)Deadline).DateTime
                };

                int ContractId = API.Client.Create(contact);
                foreach(var item in BuildingServicesContract)
                {
                    item!.BuildingService = null;
                    item.ContractId = ContractId;
                    API.Client.Create(item);
                }

                Address = string.Empty;
                Deadline = null;
                BuildingServicesContract.Clear();
            });
        }
    }
}
