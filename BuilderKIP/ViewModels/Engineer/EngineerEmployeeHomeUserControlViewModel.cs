using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System.Runtime.Serialization;

namespace BuilderKIP.ViewModels.Engineer
{
    [DataContract]
    public class EngineerEmployeeHomeUserControlViewModel : ReactiveObject, IRoutableViewModel
    {
        #region ReactiveObject
        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/EngineerEmployeeHome";

        public IWindowContainer Container { get; private set; }
        #endregion


        public EngineerEmployeeHomeUserControlViewModel(Employee employee, IWindowContainer container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;
           
        }
    }
}
