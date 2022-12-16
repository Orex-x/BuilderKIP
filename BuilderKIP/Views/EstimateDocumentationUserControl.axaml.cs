using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BuilderKIP.ViewModels.ProjectDocumentationPages;
using ReactiveUI;
using System.Reactive.Disposables;

namespace BuilderKIP.Views
{
    public partial class EstimateDocumentationUserControl : ReactiveUserControl<EstimateDocumentationUserControlViewModel>
    {
        public EstimateDocumentationUserControl()
        {
            this.WhenActivated((CompositeDisposable disposable) => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
