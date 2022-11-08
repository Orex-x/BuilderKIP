using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BuilderKIP.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace BuilderKIP.Views
{
    public partial class ClientHomeUserControl : ReactiveUserControl<ClientHomeUserControlViewModel>
    {
        public ClientHomeUserControl()
        {
            this.WhenActivated((CompositeDisposable disposable) => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
