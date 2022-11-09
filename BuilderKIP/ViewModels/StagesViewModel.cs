
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
        public ICommand Action { get; private set; }

        private Stage _stage;

        public Stage Stage
        {
            get => _stage;
            set
            {
                this.RaiseAndSetIfChanged(ref _stage, value);
            }
        }

        
        public StagesViewModel(Stage stage)
        {
            Stage = stage;
            Action = ReactiveCommand.Create(() =>
            {
                Stage.Status = "Выполнен";
                Stage.Ellipse = "green";

                API.Client.Update(Stage);
            });
        }
    }
}
