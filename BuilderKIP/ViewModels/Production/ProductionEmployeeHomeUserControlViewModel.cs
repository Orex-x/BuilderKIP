using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace BuilderKIP.ViewModels.Production
{
    [DataContract]
    public class ProductionEmployeeHomeUserControlViewModel : ReactiveObject, IRoutableViewModel
    {
        #region ReactiveObject
        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/ProductionEmployeeHome";

        public IWindowContainer Container { get; private set; }
        #endregion

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

        public Interaction<ProductionDiaryStagesWindowViewModel, Contract?> ShowDialog { get; }



        #region ICommand  
        public ICommand ClickOpen { get; private set; }
        public ICommand ClickLogOut { get; private set; }

        #endregion

       

        public ProductionEmployeeHomeUserControlViewModel(Employee employee, IWindowContainer container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;

            ShowDialog = new();

            Contracts = new(API.Client.Get<Contract>().Where(x => x.ContractStatus == ContractStatus.PENDING));

            ClickLogOut = ReactiveCommand.Create(async () =>
            {
                container.GoBack();
            });

            ClickOpen = ReactiveCommand.Create(async () =>
            {
                if (SelectedContract != null)
                {
                    var store = new ProductionDiaryStagesWindowViewModel(SelectedContract);
                    var contract = await ShowDialog.Handle(store);
                    if (contract != null)
                    {

                    }
                }
            });
        }

    }
}
