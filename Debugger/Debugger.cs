using SVM;
using SVM.VirtualMachine.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Debugger
{
    public class Debugger : IDebugger
    {
        public volatile bool running = false;
        public SvmVirtualMachine vm = null;
        string[] instructions;
        string[] stackValues;

        public SvmVirtualMachine VirtualMachine 
        { /*set => throw new NotImplementedException();*/
            get { return vm; }
            set
            {
                if (null == value)
                {
                    //throw new SvmRuntimeException();
                }
                vm = value;
            }
        }
        public Debugger()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        [STAThread]
        public void Break(IDebugFrame debugFrame)
        {
            
            Thread thread = new Thread(new ThreadStart(delegate() { Application.Run(new Form1()); } ));
        }
    }
}

