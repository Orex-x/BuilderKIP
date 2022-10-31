using Avalonia.Threading;
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
    public class EstimateDocumentationUserControlViewModel : ReactiveObject, IRoutableViewModel, IEstimateViewModel
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

        #region Fields
        private ObservableCollection<EstimateViewModel> _estimateViewModels = new ObservableCollection<EstimateViewModel>();

        public ObservableCollection<EstimateViewModel> EstimateViewModels
        {
            get => _estimateViewModels;
            set => this.RaiseAndSetIfChanged(ref _estimateViewModels, value);
        }

        private int _sum;

        private string _textSum;

        public string TextSum
        {
            get => _textSum;
            set => this.RaiseAndSetIfChanged(ref _textSum, value);
        }
        #endregion



        public EstimateDocumentationUserControlViewModel(Contract contract, IWindowContainerProjectDocumentation container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;

            /* var checkNext = this.WhenAnyValue(
                 x => x.EstimateViewModels.Where(z => z.SelectedMaterial != null).ToArray().Length == x.EstimateViewModels.Count);
 */

            _sum = 0;
            TextSum = $"Итог: ${_sum}.00";
            Boot.Materials = new(API.Client.Get<Material>());

            OnClickNext = ReactiveCommand.Create(() =>
            {

                foreach(var item in EstimateViewModels)
                {
                    if(item.SelectedMaterial != null)
                    {
                        var bsm = new BuildingServiceMaterial
                        {
                            MaterialId = item.SelectedMaterial.Id,
                            BuildingServiceContractId = contract.Id,
                            Material = item.SelectedMaterial,
                            Amount = item.Count,
                        };
                        if (contract.BuildingServiceContract.Materials == null) 
                            contract.BuildingServiceContract.Materials = new List<BuildingServiceMaterial>();

                        contract.BuildingServiceContract.Materials.Add(bsm);

                        contract.BuildingServiceContract.PriceEstimate = _sum;
                    }
                }

                if (Container != null)
                    Container.Save(contract);
            });

            OnClickAddEstimateViewModel = ReactiveCommand.Create(() =>
            {
                EstimateViewModels.Add(new EstimateViewModel(this)
                {
                    Count = 1
                });
            });

        }

        public void change()
        {
            Dispatcher.UIThread.InvokeAsync( async () => {
                int sum = 0;
                foreach (var item in EstimateViewModels)
                {
                    if(item.SelectedMaterial != null)
                        sum += item.Count * item.SelectedMaterial.Price;
                }
                _sum = sum;
                TextSum = $"Итог: ${_sum}.00";
            });
        }
    }
}
