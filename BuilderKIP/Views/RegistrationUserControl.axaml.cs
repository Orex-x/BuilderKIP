using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BuilderKIP.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace BuilderKIP.Views
{
    public partial class RegistrationUserControl : ReactiveUserControl<RegistrationUserControlViewModel>
    {
        public RegistrationUserControl()
        {
            this.WhenActivated((CompositeDisposable disposable) => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
