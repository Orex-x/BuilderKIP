using Avalonia.Controls;
using Avalonia.ReactiveUI;
using BuilderKIP.Models;
using BuilderKIP.ViewModels;
using ReactiveUI;
using System;


namespace BuilderKIP.Views
{
    public partial class BuildingServiceStoreWindow : ReactiveWindow<BuildingServiceStoreWindowViewModel>
    {
       

        public BuildingServiceStoreWindow()
        {
            InitializeComponent();

            this.WhenActivated(d => d(ViewModel!.SaveCommand.Subscribe(Close)));
        }
    }
}
