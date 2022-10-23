﻿using BuilderKIP.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
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
        #endregion

        public EstimateDocumentationUserControlViewModel(Contract contract, IWindowContainerProjectDocumentation container, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Container = container;

            OnClickNext = ReactiveCommand.Create(() =>
            {
                if (Container != null)
                    Container.Save(contract);
            });

        }
    }
}