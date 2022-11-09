
using BuilderKIP.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuilderKIP.ViewModels
{
    public class StagesViewModel : ReactiveObject
    {
        public ICommand Action { get; set; }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        private string _actionName;
        public string ActionName
        {
            get => _actionName;
            set
            {
                this.RaiseAndSetIfChanged(ref _actionName, value);
            }
        } 
        
        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                this.RaiseAndSetIfChanged(ref _status, value);
            }
        }
         
        
        private string _ellipse;
        public string Ellipse
        {
            get => _ellipse;
            set
            {
                this.RaiseAndSetIfChanged(ref _ellipse, value);
            }
        }

        public StagesViewModel()
        {
           
        }
    }
}
