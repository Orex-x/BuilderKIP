using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuilderKIP.ViewModels
{
    [DataContract]
    public class AuthorizationUserControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/login";

        public IWindowContainer Container { get; private set; }

        public ICommand OnClickLogIn { get; private set; }
        public ICommand OnClickGoToRegistration { get; private set; }

        private string _password;
        private string _login;
        private string _message_log;
        private string _title = "Авторизация";

        [DataMember]
        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }

        // Пароль на диск не сохраняем из соображений безопасности!
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }


        public string MessageLog
        {
            get => _message_log;
            set => this.RaiseAndSetIfChanged(ref _message_log, value);
        }

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public AuthorizationUserControlViewModel(IWindowContainer container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;

            var canLogin = this
                .WhenAnyValue(
                    x => x.Login,
                    x => x.Password,
                    (user, pass) => !string.IsNullOrWhiteSpace(user) &&
                                    !string.IsNullOrWhiteSpace(pass));

            // Привязанные к команде кнопки будут выключены, пока
            // пользовательский ввод не завершён.
            OnClickLogIn = ReactiveCommand.Create(async () =>
            {
                var user = API.Client.Authorization(Login, Password);

                if (user != null)
                {
                    var listClients = API.Client.Get<Client>();
                    var listEmployees = API.Client.Get<Employee>();

                    var client = listClients.FirstOrDefault(x => x.UserId == user.Id); 
                    var employee = listEmployees.FirstOrDefault(x => x.UserId == user.Id); 

                    if (Container != null)
                    {
                        if(client != null) Container.GoToHomeClient(client);
                        if(employee != null) Container.GoToHomeEmployee();
                    }
                }
                else
                {
                    MessageLog = "Неверные логин или пароль";
                }
            }, canLogin);

            OnClickGoToRegistration = ReactiveCommand.Create(() =>
            {
                if (Container != null)
                    Container.GoToRegistartion();
            });
        }
    }
}
