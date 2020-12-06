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
        private volatile static bool _isContinuePressed;
        private bool running = false;
        private SvmVirtualMachine vm = null;
        static string[] instructions = { "test", "test 3", "test 6" };
        static string[] stackValues = { "test", "test 3", "test 6" };
        public static Form form = new Form1(instructions, stackValues, "test 3" );
        Thread thread = new Thread(new ThreadStart(delegate () { Application.Run(form); }));
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
        public static bool IsContinuePressed
        {
            get { return _isContinuePressed; }
            set { _isContinuePressed = value;  }
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
            if (!running)
            {
                running = true;
                thread.Start();
            }
            else
            {
                IsContinuePressed = false;
                // Create instance of form before adding to thread... run thread... on cont. invoke form instance to update values
                //Form1.invoke
                //form.Invoke(form.);
            }
            while (!_isContinuePressed) { }

            return;
        }
    }
}

