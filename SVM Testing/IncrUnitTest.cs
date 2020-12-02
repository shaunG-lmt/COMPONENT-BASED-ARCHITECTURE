using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVM;
using SVM.VirtualMachine;

namespace SVM_Testing
{
    [TestClass]
    public class IncrUnitTest
    {
        [TestMethod]
        public void IncrTest_ValidInput()
        {
            IVirtualMachine virtualMachine = new SvmVirtualMachine();
            virtualMachine.Stack.Push(1);
            
        }
    }
}
