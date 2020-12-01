using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SVM.VirtualMachine
{
    public interface IVirtualMachine
    {
        Stack Stack { get; }

        int ProgramCounter { get; }

    }
}
