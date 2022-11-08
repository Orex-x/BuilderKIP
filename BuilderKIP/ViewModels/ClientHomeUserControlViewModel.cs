using Avalonia;
using Avalonia.Controls;
using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Contract = BuilderKIP.Models.Contract;

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

       
        public ClientHomeUserControlViewModel(Client client, IWindowContainer container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;
            ClientContractsPageViewModel = new ContractsPageViewModel(client);
            ClientNewContractPageViewModel = new ClientNewContractPageViewModel(client);
        }

    }
}
