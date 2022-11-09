using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BuilderKIP.ViewModels;
using BuilderKIP.ViewModels.Engineer;
using BuilderKIP.ViewModels.ProjectDocumentationPages;
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
            Locator.CurrentMutable.Register<IViewFor<ClientHomeUserControlViewModel>>(() => new ClientHomeUserControl());
            Locator.CurrentMutable.Register<IViewFor<TechnicalEmployeeHomeUserControlViewModel>>(() => new TechnicalEmployeeHomeUserControl());
            Locator.CurrentMutable.Register<IViewFor<BuildingServiceStoreWindowViewModel>>(() => new BuildingServiceStoreWindow());
            Locator.CurrentMutable.Register<IViewFor<ProjectDocumentationWindowViewModel>>(() => new ProjectDocumentationWindow());
            Locator.CurrentMutable.Register<IViewFor<MainDocumentationUserControlViewModel>>(() => new MainDocumentationUserControl());
            Locator.CurrentMutable.Register<IViewFor<EstimateDocumentationUserControlViewModel>>(() => new EstimateDocumentationUserControl());
            Locator.CurrentMutable.Register<IViewFor<EngineerEmployeeHomeUserControlViewModel>>(() => new EngineerEmployeeHomeUserControl());


            // Получаем корневую модель представления и инициализируем контекст данных.
            new MainWindow { DataContext = Locator.Current.GetService<IScreen>() }.Show();

            base.OnFrameworkInitializationCompleted();
        }
    }
}
