using BuilderKIP.API;
using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuilderKIP.ViewModels
{
    [DataContract]
    public class TechnicalEmployeeHomeUserControlViewModel : ReactiveObject, IRoutableViewModel
    {
        #region ReactiveObject
        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/TechnicalEmployeeHome";

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

        public Interaction<ProjectDocumentationWindowViewModel, Contract?> ShowDialog { get; }

        #endregion

        #region ICommand  
        public ICommand ClickOpen { get; private set; }

        #endregion

        public TechnicalEmployeeHomeUserControlViewModel(Employee employee, IWindowContainer container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;
            ShowDialog = new();

            Contracts = new(API.Client.Get<Contract>().Where(x => x.ContractStatus == ContractStatus.NEW));


            ClickOpen = ReactiveCommand.Create(async ()  =>
            {
                if(SelectedContract != null)
                {
                    var store = new ProjectDocumentationWindowViewModel(SelectedContract);
                    var contract = await ShowDialog.Handle(store);
                    if(contract != null)
                    {
                        foreach (var item in contract.BuildingServiceContract.Materials)
                        {
                            item!.Material = null;
                            item!.BuildingServiceContract = null;
                            API.Client.Create(item);
                        }

                        contract.BuildingServiceContract.TypeClimaticCondition = null;
                        contract.BuildingServiceContract.TypeGround = null;
                        contract.BuildingServiceContract.TypeRelief = null;
                        contract.BuildingServiceContract.Materials = null;
                        contract.BuildingServiceContract.ContractId = contract.Id;

                        contract.ContractStatus = ContractStatus.NOT_ACCEPT;

                        API.Client.Update(contract.BuildingServiceContract);
                        API.Client.Update(contract);
                    }
                }
            });
        }
    }
}
