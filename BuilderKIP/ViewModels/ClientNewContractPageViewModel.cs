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

                //чтобы для entity обьект был уникальным
                BuildingServicesContract.Add(new BuildingServiceContract()
                {
                    BuildingServiceId = result.Id,
                    BuildingService = result
                });

            });

            OnClickDeleteBuildingService = ReactiveCommand.Create(() =>
            {
                if(SelectedBuildingService != null)
                    BuildingServicesContract.Remove(SelectedBuildingService);
            });

            OnClickSaveContract = ReactiveCommand.Create(() =>
            {
               // var list = BuildingServices = BuildingServicesContract,
                var contact = new Contract
                {
                    Address = Address,
                    ClientId = client.Id,
                    Number = "123",
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
            });
        }


        //хз, хотел сделать что то типо чтобы обьекты 
        public void Clean<T>(T obj)
        {
            var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
          
            foreach (var fld in fields)
            {
                var type = fld.FieldType.Name;
                var name = getName(fld.Name);
                object value = fld.GetValue(obj);
                
            }
        }

        public static string getName(string typeName)
        {
            string name = "";

            for (int i = 0; i < typeName.Length; i++)
            {
                if (typeName[i] == '>')
                    return name;

                if (typeName[i] != '<')
                    name += typeName[i];

            }
            return name;
        }
    }
}
