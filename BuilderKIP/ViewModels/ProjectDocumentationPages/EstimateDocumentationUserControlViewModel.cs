using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuilderKIP.ViewModels.ProjectDocumentationPages
{
    [DataContract]
    public class EstimateDocumentationUserControlViewModel : ReactiveObject, IRoutableViewModel
    {

        #region IRoutableViewModel
        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/EstimateDocumentation";

        public IWindowContainerProjectDocumentation Container { get; private set; }

        #endregion



        #region ICommand
        public ICommand OnClickNext { get; private set; }
        public ICommand OnClickAddEstimateViewModel { get; private set; }
        #endregion


        private ObservableCollection<EstimateViewModel> _estimateViewModels = new ObservableCollection<EstimateViewModel>();

        public ObservableCollection<EstimateViewModel> EstimateViewModels
        {
            get => _estimateViewModels;
            set => this.RaiseAndSetIfChanged(ref _estimateViewModels, value);
        }


        public EstimateDocumentationUserControlViewModel(Contract contract, IWindowContainerProjectDocumentation container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;

           /* var checkNext = this.WhenAnyValue(
                x => x.EstimateViewModels.Where(z => z.SelectedMaterial != null).ToArray().Length == x.EstimateViewModels.Count);
*/

          
            Boot.Materials = new(API.Client.Get<Material>());

            OnClickNext = ReactiveCommand.Create(() =>
            {

                foreach(var item in EstimateViewModels)
                {
                    if(item.SelectedMaterial != null)
                    {
                        var material = new Material()
                        {
                            Name = item.SelectedMaterial.Name,
                            Price = item.SelectedMaterial.Price,
                            BuildingServiceContractId = contract.Id
                        };
                        API.Client.Create(material);
                    }
                }

                if (Container != null)
                    Container.Save(contract);
            });

            OnClickAddEstimateViewModel = ReactiveCommand.Create(() =>
            {
                EstimateViewModels.Add(new EstimateViewModel()
                {
                    Count = 1
                });
            });

        }
    }
}
