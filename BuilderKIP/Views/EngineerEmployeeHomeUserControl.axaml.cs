using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BuilderKIP.ViewModels;
using BuilderKIP.ViewModels.Engineer;
using ReactiveUI;
using System.Reactive.Disposables;

namespace BuilderKIP.Views
{
    public partial class EngineerEmployeeHomeUserControl : ReactiveUserControl<EngineerEmployeeHomeUserControlViewModel>
    {
        public EngineerEmployeeHomeUserControl()
        {
            this.WhenActivated((CompositeDisposable disposable) => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
