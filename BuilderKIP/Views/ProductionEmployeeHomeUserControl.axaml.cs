using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using BuilderKIP.Models;
using BuilderKIP.ViewModels.Production;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace BuilderKIP.Views
{
    public partial class ProductionEmployeeHomeUserControl : ReactiveUserControl<ProductionEmployeeHomeUserControlViewModel>
    {
        public ProductionEmployeeHomeUserControl()
        {
            this.WhenActivated((CompositeDisposable disposable) => { });
            AvaloniaXamlLoader.Load(this);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            ((ProductionEmployeeHomeUserControlViewModel)DataContext)!.ShowDialog.RegisterHandler(DoShowDialogAsync);
        }


        private async Task DoShowDialogAsync(InteractionContext<ProductionDiaryStagesWindowViewModel, Contract?> interaction)
        {
            var dialog = new ProductionDiaryStagesWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Contract>(GetWindow());
            interaction.SetOutput(result);
        }


        public Window GetWindow()
        {
            IControl window = this.Parent;
            while (!(window is Window))
            {
                window = window.Parent;
            }
            return (Window)window;
        }
    }
}
