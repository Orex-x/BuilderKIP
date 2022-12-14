using BuilderKIP.Models;
using BuilderKIP.ViewModels.Engineer;
using BuilderKIP.ViewModels.Production;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace BuilderKIP.ViewModels
{
    public interface IWindowContainer
    {
        public void GoToRegistartion();
        public void GoToAuthorization();
        public void GoBack();
        public void GoToHomeTechnicalEmployee(Employee employee);
        public void GoToHomeEngineerEmployee(Employee employee);
        public void GoToHomeProductionEmployee(Employee employee);
        public void GoToHomeClient(Client client);
    }

    [DataContract]
    public class MainWindowViewModel : ReactiveObject, IScreen, IWindowContainer
    {

        private RoutingState _router = new RoutingState();

        void IWindowContainer.GoBack()
        {
            Router.NavigateBack.Execute();
        }


        public static ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public static User _user;


        public MainWindowViewModel()
        {
            Router.Navigate.Execute(new AuthorizationUserControlViewModel(this));
        }

        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }


        public void GoToRegistartion()
        {
            Router.Navigate.Execute(new RegistrationUserControlViewModel(this));
        }

        public void GoToAuthorization()
        {
            Router.Navigate.Execute(new AuthorizationUserControlViewModel(this));
        }

        public void GoToHomeClient(Client client)
        {
            Router.Navigate.Execute(new ClientHomeUserControlViewModel(client, this));
        }
        
        public void GoToHomeTechnicalEmployee(Employee employee)
        {
            Router.Navigate.Execute(new TechnicalEmployeeHomeUserControlViewModel(employee, this));
        }

        public void GoToHomeEngineerEmployee(Employee employee)
        {
            Router.Navigate.Execute(new EngineerEmployeeHomeUserControlViewModel(employee, this));
        }

        public void GoToHomeProductionEmployee(Employee employee)
        {
            Router.Navigate.Execute(new ProductionEmployeeHomeUserControlViewModel(employee, this));
        }
    }
}
