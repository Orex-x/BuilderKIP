using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BuilderKIP.ViewModels;
using BuilderKIP.Views;
using ReactiveUI;
using Splat;

namespace BuilderKIP
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Locator.CurrentMutable.RegisterConstant<IScreen>(new MainWindowViewModel());
            Locator.CurrentMutable.Register<IViewFor<AuthorizationUserControlViewModel>>(() => new AuthorizationUserControl());
            Locator.CurrentMutable.Register<IViewFor<RegistrationUserControlViewModel>>(() => new RegistrationUserControl());

            // Получаем корневую модель представления и инициализируем контекст данных.
            new MainWindow { DataContext = Locator.Current.GetService<IScreen>() }.Show();

            base.OnFrameworkInitializationCompleted();
        }
    }
}
