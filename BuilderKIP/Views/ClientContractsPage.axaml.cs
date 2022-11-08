using Avalonia.Controls;
using Avalonia.Media;
using BuilderKIP.Models;
using BuilderKIP.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace BuilderKIP.Views
{
    public partial class ClientContractsPage : UserControl
    {
        public ClientContractsPage()
        {
            InitializeComponent();
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            ((ContractsPageViewModel)DataContext)!.ShowDialog.RegisterHandler(DoShowDialogAsync);
        }


        private async Task DoShowDialogAsync(InteractionContext<ContractDetailsViewModel, Contract?> interaction)
        {
            var dialog = new ContractDetailsWindow();
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
