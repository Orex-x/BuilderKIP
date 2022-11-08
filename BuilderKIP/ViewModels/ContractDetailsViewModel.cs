using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BuilderKIP.ViewModels
{

    [DataContract]
    public class ContractDetailsViewModel : ReactiveObject, IScreen
    {

        private RoutingState _router = new RoutingState();
        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        public ReactiveCommand<Unit, Contract?> SaveCommand { get; }

        private ObservableCollection<StagesViewModel> _stagesViewModels = new ObservableCollection<StagesViewModel>();

        public ObservableCollection<StagesViewModel> Stages
        {
            get => _stagesViewModels;
            set => this.RaiseAndSetIfChanged(ref _stagesViewModels, value);
        }

        public ContractDetailsViewModel(Contract contract)
        {

            var svm = new StagesViewModel()
            {
                Name = "Этап 1",
                ActionName = "Принять",
                Ellipse = "green"
            };

            svm.Action = ReactiveCommand.Create(() =>
            {
                svm.Ellipse = "red";
            });

            Stages.Add(svm);

            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                return contract;
            });
        }

      
    }
}
