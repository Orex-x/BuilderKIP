using BuilderKIP.Models;
using BuilderKIP.ViewModels.ProjectDocumentationPages;
using ReactiveUI;
using System.Diagnostics.Metrics;
using System.Reactive;
using System.Runtime.Serialization;

namespace BuilderKIP.ViewModels
{
    public interface IWindowContainerProjectDocumentation
    {
        public void GoToEstimate(Contract contract);
        public void GoToStart(Contract contract);
        public void Save(Contract contract);
    }



    [DataContract]
    public class ProjectDocumentationWindowViewModel : ReactiveObject, IScreen, IWindowContainerProjectDocumentation
    {

        #region Fields
        private RoutingState _router = new RoutingState();
        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        public ReactiveCommand<Unit, Contract?> SaveCommand { get; }

        public Contract? Contract { get; set; }

        public bool _routerVisible;
        public bool RouterVisible 
        {
            get => _routerVisible;
            set => this.RaiseAndSetIfChanged(ref _routerVisible, value);
        }
        #endregion

        #region IWindowContainerProjectDocumentation

        public void GoToEstimate(Contract contract)
        {
            Router.Navigate.Execute(new EstimateDocumentationUserControlViewModel(contract, this));
        }

        public void GoToStart(Contract contract)
        {
            Router.Navigate.Execute(new MainDocumentationUserControlViewModel(contract, this));
        }

        public void Save(Contract contract)
        {
            RouterVisible = false;
            Contract = contract;
        }
        #endregion


        public ProjectDocumentationWindowViewModel(Contract contract)
        {
            RouterVisible = true;
            GoToStart(contract);

            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                return Contract;
            });
        }
    }
}
