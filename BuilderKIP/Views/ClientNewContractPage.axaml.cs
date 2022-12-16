using Avalonia.Controls;
using Avalonia.Media;
using BuilderKIP.Models;
using BuilderKIP.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace BuilderKIP.Views
{
    public partial class ClientNewContractPage : UserControl
    {
      
        public ClientNewContractPage()
        {
            InitializeComponent();
        }


        public override void Render(DrawingContext context)
        {
            base.Render(context);

            ((ClientNewContractPageViewModel)DataContext)!.ShowDialog.RegisterHandler(DoShowDialogAsync);
        }


        private async Task DoShowDialogAsync(InteractionContext<BuildingServiceStoreWindowViewModel, BuildingService?> interaction)
        {
            var dialog = new BuildingServiceStoreWindow();
            dialog.DataContext = interaction.Input;

            

            var result = await dialog.ShowDialog<BuildingService>(GetWindow());
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
