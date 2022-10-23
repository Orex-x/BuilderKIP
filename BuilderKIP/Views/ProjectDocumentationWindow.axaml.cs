using Avalonia.ReactiveUI;
using BuilderKIP.ViewModels;
using ReactiveUI;
using System;

namespace BuilderKIP.Views
{
    public partial class ProjectDocumentationWindow : ReactiveWindow<ProjectDocumentationWindowViewModel>
    {
        public ProjectDocumentationWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.SaveCommand.Subscribe(Close)));
        }
    }
}
