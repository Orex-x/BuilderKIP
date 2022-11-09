using Avalonia.ReactiveUI;
using BuilderKIP.ViewModels.Engineer;
using ReactiveUI;
using System;

namespace BuilderKIP.Views
{
    public partial class EngineerCreateStagesWindow : ReactiveWindow<EngineerCreateStagesWindowViewModel>
    {
        public EngineerCreateStagesWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.SaveCommand.Subscribe(Close)));
        }
    }
}
