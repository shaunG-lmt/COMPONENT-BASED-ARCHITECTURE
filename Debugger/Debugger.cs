using SVM;
using SVM.VirtualMachine.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Debugger
{
    public class Debugger : IDebugger
    {
        public SvmVirtualMachine VirtualMachine { set => throw new NotImplementedException(); }

        public Debugger()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        [STAThread]
        public void Break(IDebugFrame debugFrame)
        {
            Application.Run(new Form1());
        }
    }
}

