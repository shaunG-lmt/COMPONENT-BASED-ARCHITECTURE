using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM.VirtualMachine.Debug;
using SVM.VirtualMachine;
using SVM.SimpleMachineLanguage;
using SVM;

namespace Debuggers
{
    public class Debugger : IDebugger
    {
        #region TASK 5 - TO BE IMPLEMENTED BY THE STUDENT
        #endregion
        public SvmVirtualMachine VirtualMachine { set => throw new NotImplementedException(); }

        public void Break(IDebugFrame debugFrame)
        {
            
            throw new NotImplementedException();
        }
    }
}

