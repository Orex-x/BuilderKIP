using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace BuilderKIP.ViewModels
{
    [DataContract]
    public class RegistrationUserControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "/registration";

        public IScreen HostScreen { get; }

        public IWindowContainer Container { get; private set; }

        #region ICommand
        public ICommand OnClickRegistration { get; private set; }
        public ICommand OnClickBack { get; private set; }

        #endregion

        #region Fields
        private string _name;
        private string _surname;
        private string _lastname;
        private string _login;
        private string _password;
        private string _confirm_password;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public string Surname
        {
            get => _surname;
            set => this.RaiseAndSetIfChanged(ref _surname, value);
        }
        public string Lastname
        {
            get => _lastname;
            set => this.RaiseAndSetIfChanged(ref _lastname, value);
        }
        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        public string ConfirmPassword
        {
            get => _confirm_password;
            set => this.RaiseAndSetIfChanged(ref _confirm_password, value);
        }
        #endregion

        public RegistrationUserControlViewModel(IWindowContainer container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;

            var checkPass = this
                .WhenAnyValue(
                    x => x.Password,
                    x => x.ConfirmPassword,
                    (pass, con_pass) => pass == con_pass);


            OnClickRegistration = ReactiveCommand.Create(async () =>
            {
                var user = new User
                {
                    Login = Login,
                    FirstName = Name,
                    LastName = Lastname,
                    MiddleName = Surname,
                    Password = Password
                };
/*
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, Password);*/


                bool ok = API.Client.Registration(user);
                if (ok)
                {
                    if (container != null)
                    {
                        container.GoToAuthorization();
                    }
                }
            }, checkPass);

            OnClickBack = ReactiveCommand.Create(() =>
            {
                if (container != null)
                {
                    container.GoToAuthorization();
                }
            });
        }

    }
}

