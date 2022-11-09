using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace BuilderKIP.ViewModels.Engineer
{
    [DataContract]
    public class EngineerEmployeeHomeUserControlViewModel : ReactiveObject, IRoutableViewModel
    {
        #region ReactiveObject
        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/EngineerEmployeeHome";

        public IWindowContainer Container { get; private set; }
        #endregion


        #region Fields

        private ObservableCollection<Contract> _contracts = new ObservableCollection<Contract>();

        public ObservableCollection<Contract> Contracts
        {
            get => _contracts;
            set => this.RaiseAndSetIfChanged(ref _contracts, value);
        }

        private Contract _selectedContract;

        public Contract SelectedContract
        {
            get => _selectedContract;
            set => this.RaiseAndSetIfChanged(ref _selectedContract, value);
        }

        public Interaction<EngineerCreateStagesWindowViewModel, Contract?> ShowDialog { get; }

        #endregion

        #region ICommand  
        public ICommand ClickOpen { get; private set; }

        #endregion


        public EngineerEmployeeHomeUserControlViewModel(Employee employee, IWindowContainer container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;

            ShowDialog = new();

            Contracts = new(API.Client.Get<Contract>().Where(x => x.ContractStatus == ContractStatus.ACCEPT));

            ClickOpen = ReactiveCommand.Create(async () =>
            {
                if (SelectedContract != null)
                {
                    var store = new EngineerCreateStagesWindowViewModel(SelectedContract);
                    var contract = await ShowDialog.Handle(store);
                    if (contract != null)
                    {
                        foreach (var item in contract.BuildingServiceContract.Stages)
                        {
                            item!.BuildingServiceContract = null;
                            API.Client.Create(item);
                        }
                    }
                }
            });
        }
    }
}
