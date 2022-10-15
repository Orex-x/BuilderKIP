using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BuilderKIP.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace BuilderKIP.Views
{
    public partial class AuthorizationUserControl : ReactiveUserControl<AuthorizationUserControlViewModel>
    {
        public AuthorizationUserControl()
        {
            this.WhenActivated((CompositeDisposable disposable) => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
