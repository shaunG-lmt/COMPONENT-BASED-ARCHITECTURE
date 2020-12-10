using SVM;
using SVM.VirtualMachine.Debug;
using System;
using System.Collections;
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
        private static SvmVirtualMachine vm = null;
        private static Stack ts = null;
        public static Form1 form = new Form1(debugFrame, ts);
        private static IDebugFrame debugFrame = null;

        public SvmVirtualMachine VirtualMachine 
        { 
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
                form = new Form1(debugFrame, vm.Stack);

                Thread thread = new Thread(() => Application.Run(form));
       
                thread.Start();
            }
            else
            {
                IsContinuePressed = false;
                form.Invoke(form.myDelegate, new object[] { debugFrame, vm.Stack });
            }

            while (!_isContinuePressed) { continue;  }

            return;
        }
       
    }
}

