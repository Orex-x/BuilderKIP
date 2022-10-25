using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace BuilderKIP.ViewModels.ProjectDocumentationPages
{
    [DataContract]
    public class MainDocumentationUserControlViewModel : ReactiveObject, IRoutableViewModel
    {

        #region IRoutableViewModel
        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/MainDocumentation";

        public IWindowContainerProjectDocumentation Container { get; private set; }

        #endregion

        #region ICommand
        public ICommand OnClickNext { get; private set; }
        public ICommand InformationTypeRelief { get; private set; }
        public ICommand InformationTypeGround { get; private set; }
        public ICommand InformationTypeClimaticCondition { get; private set; }
        #endregion

        #region Fields

        private ObservableCollection<TypeClimaticCondition> _typeClimaticConditions = new ObservableCollection<TypeClimaticCondition>();
        private ObservableCollection<TypeGround> _typeGrounds = new ObservableCollection<TypeGround>();
        private ObservableCollection<TypeRelief> _typeReliefs = new ObservableCollection<TypeRelief>();


        public ObservableCollection<TypeClimaticCondition> TypeClimaticConditions
        {
            get => _typeClimaticConditions;
            set => this.RaiseAndSetIfChanged(ref _typeClimaticConditions, value);
        }

        public ObservableCollection<TypeGround> TypeGrounds
        {
            get => _typeGrounds;
            set => this.RaiseAndSetIfChanged(ref _typeGrounds, value);
        }

        public ObservableCollection<TypeRelief> TypeReliefs
        {
            get => _typeReliefs;
            set => this.RaiseAndSetIfChanged(ref _typeReliefs, value);
        }




        private TypeClimaticCondition _selectedTypeClimaticCondition;
        public TypeClimaticCondition SelectedTypeClimaticCondition
        {
            get => _selectedTypeClimaticCondition;
            set {
                EllipseTypeClimaticCondition = (value == null) ? "Red" : "Green";
                this.RaiseAndSetIfChanged(ref _selectedTypeClimaticCondition, value);
            } 
        }



        private TypeGround _selectedTypeGround;
        public TypeGround SelectedTypeGround
        {
            get => _selectedTypeGround;
            set {
                EllipseTypeGround = (value == null) ? "Red" : "Green";
                this.RaiseAndSetIfChanged(ref _selectedTypeGround, value);
            }
        }

        private TypeRelief _selectedTypeRelief;
        public TypeRelief SelectedTypeRelief
        {
            get => _selectedTypeRelief;
            set
            {
                EllipseTypeRelief = (value == null) ? "Red" : "Green";
                this.RaiseAndSetIfChanged(ref _selectedTypeRelief, value);
            }
        }




       

  
        private string _ellipseTypeClimaticCondition;
        public string EllipseTypeClimaticCondition
        {
            get => _ellipseTypeClimaticCondition;
            set => this.RaiseAndSetIfChanged(ref _ellipseTypeClimaticCondition, value);
        }
        
        
        private string _ellipseTypeGround;
        public string EllipseTypeGround
        {
            get => _ellipseTypeGround;
            set => this.RaiseAndSetIfChanged(ref _ellipseTypeGround, value);
        }


        private string _ellipseTypeRelief;
        public string EllipseTypeRelief
        {
            get => _ellipseTypeRelief;
            set => this.RaiseAndSetIfChanged(ref _ellipseTypeRelief, value);
        }



        #endregion

        public MainDocumentationUserControlViewModel(Contract contract, IWindowContainerProjectDocumentation container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;

            var checkNext = this
               .WhenAnyValue(
                   x => x.SelectedTypeClimaticCondition,
                   x => x.SelectedTypeGround,
                   x => x.SelectedTypeRelief,
                   (s1, s2, s3) => s1 != null && s2 != null && s3 != null);

            EllipseTypeRelief = "Red";
            EllipseTypeClimaticCondition = "Red";
            EllipseTypeGround = "Red";

            TypeClimaticConditions = new(API.Client.Get<TypeClimaticCondition>());
            TypeGrounds = new(API.Client.Get<TypeGround>());
            TypeReliefs = new(API.Client.Get<TypeRelief>());

            InformationTypeRelief = ReactiveCommand.Create(() =>
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Типы рельефа", "Типом рельефа или" +
                "комплексом называется определённое сочетание форм рельефа, \n" +
                "закономерно повторяющихся на обширных пространствах поверхности " +
                "земной коры, имеющие сходное происхождение, \n" +
                "геологическое строение и историю развития.");
                messageBoxStandardWindow.Show();
            });

            InformationTypeGround = ReactiveCommand.Create(() =>
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
               .GetMessageBoxStandardWindow("Типы грунта", "Нужно провести анализ геологических проб грунта на близость грунтовых вод, \n" +
               "узнать его химические и физико-механические свойства. \n" +
               "В дальнейшем при определении конструкции фундамента дома и глубины его заложения необходимо учитывать " +
               "виды грунтов и их свойства");
                messageBoxStandardWindow.Show();
            });

            InformationTypeClimaticCondition = ReactiveCommand.Create(() =>
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
               .GetMessageBoxStandardWindow("Климатический показатель", "Учитываются климатические особенности, \n" +
               "и прежде всего температура, ветер, сочетание температурно-влажностных показателей, \n" +
               " гололедно-ветровые нагрузки на высотные сооружения и другие характеристики");
                messageBoxStandardWindow.Show();
            });

            OnClickNext = ReactiveCommand.Create(() =>
            {
                contract.BuildingService.TypeClimaticCondition = SelectedTypeClimaticCondition;
                contract.BuildingService.TypeGround = SelectedTypeGround;
                contract.BuildingService.TypeRelief = SelectedTypeRelief;
                if (Container != null)
                    Container.GoToEstimate(contract);
            }, checkNext);
        }

    }
}
