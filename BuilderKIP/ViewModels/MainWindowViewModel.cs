using BuilderKIP.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace BuilderKIP.ViewModels
{
    public interface IWindowContainer
    {
        public void GoToRegistartion();
        public void GoToAuthorization();
        public void GoBack();
        public void GoToHome();
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

        public void GoToHome()
        {
            Router.Navigate.Execute(new ClientHomeUserControlViewModel(this));
        }
    }
}
