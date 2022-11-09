using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using BuilderKIP.Models;
using BuilderKIP.ViewModels.Engineer;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace BuilderKIP.Views
{
    public partial class EngineerEmployeeHomeUserControl : ReactiveUserControl<EngineerEmployeeHomeUserControlViewModel>
    {
        public EngineerEmployeeHomeUserControl()
        {
            this.WhenActivated((CompositeDisposable disposable) => { });
            AvaloniaXamlLoader.Load(this);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            ((EngineerEmployeeHomeUserControlViewModel)DataContext)!.ShowDialog.RegisterHandler(DoShowDialogAsync);
        }


        private async Task DoShowDialogAsync(InteractionContext<EngineerCreateStagesWindowViewModel, Contract?> interaction)
        {
            var dialog = new EngineerCreateStagesWindow();
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
