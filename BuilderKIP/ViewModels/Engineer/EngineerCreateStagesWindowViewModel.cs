using Avalonia.Media.TextFormatting;
using BuilderKIP.Models;
using BuilderKIP.ViewModels.ProjectDocumentationPages;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BuilderKIP.ViewModels.Engineer
{

    public class EngineerCreateStagesWindowViewModel : ReactiveObject, IScreen
    {
        #region IWindowContainerProjectDocumentation

        private RoutingState _router = new RoutingState();
        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        #endregion

        public ReactiveCommand<Unit, Contract?> SaveCommand { get; }

        public EngineerCreateStagesWindowViewModel(Contract contract)
        {
            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                return contract;
            });
        }
    }
}
