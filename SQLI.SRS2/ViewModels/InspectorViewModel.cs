using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLI.SRS2.ViewModels
{
    public class InspectorViewModel : BindableBase
    {

        private DelegateCommand closeCommand;
        public DelegateCommand CloseCommand => closeCommand ??= new DelegateCommand(ExecuteCloseCommand);

        public InspectorViewModel()
        {

        }

        void ExecuteCloseCommand()
        {

        }
    }
}
