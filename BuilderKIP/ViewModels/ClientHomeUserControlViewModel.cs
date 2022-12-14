using Avalonia;
using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace BuilderKIP.ViewModels
{
    [DataContract]
    public class ClientHomeUserControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/clientHome";

        public IWindowContainer Container { get; private set; }

        public ContractsPageViewModel ClientContractsPageViewModel { get; }
        public ClientNewContractPageViewModel ClientNewContractPageViewModel { get; }

        private Client _client;
        public Client Client
        {
            get => _client;
            set => this.RaiseAndSetIfChanged(ref _client, value);
        }
        
        private string _txtBalance;
        public string TxtBalance
        {
            get => _txtBalance;
            set => this.RaiseAndSetIfChanged(ref _txtBalance, value);
        }
        public ICommand Deposit { get; set; }
        public ICommand ClickLogOut { get; set; }
       

        public ClientHomeUserControlViewModel(Client client, IWindowContainer container, IScreen screen = null)
        {
            Client = client;
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;
            ClientContractsPageViewModel = new ContractsPageViewModel(client);
            ClientNewContractPageViewModel = new ClientNewContractPageViewModel(client);


            Deposit = ReactiveCommand.Create(() =>
            {
                client.Balance += Convert.ToInt32(TxtBalance);
                API.Client.Update(client);
            });

            ClickLogOut = ReactiveCommand.Create(() =>
            {
                container.GoBack();
            });
        }

    }
}
