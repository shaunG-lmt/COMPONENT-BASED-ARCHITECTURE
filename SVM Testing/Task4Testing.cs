using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVM;
using SVM.VirtualMachine;
using SVM.SimpleMachineLanguage;
using Moq;
using System.Collections;
using System;
//ADDED VIRTUAL TO BASEINSTRUCTION FOR ALLOW MOCK VIRTUAL MACHINE
namespace SVM_Testing
{
    [TestClass]
    public class Task4Tesing
    {
        Mock<IVirtualMachine> mockVm = new Mock<IVirtualMachine>(MockBehavior.Strict);
        [TestMethod]
        public void IncrTest_ValidInput()
        {
            #region Validation Values
            Stack validationStack = new Stack();
            validationStack.Push(7);
            #endregion

            #region Mock Setup
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Mock<Incr> mockIncr = new Mock<Incr>();
            mockIncr.Setup(p => p.VirtualMachine).Returns(mockVm.Object);
            mockIncr.CallBase = true;
            #endregion


            try
            {
                mockIncr.Object.Run();
            }
            catch
            {
                Assert.Fail("Unexpected Exception");
            }
            Assert.AreEqual(mockIncr.Object.VirtualMachine.Stack.Peek(), validationStack.Peek());
        }
        [TestMethod()]
        public void IncrTest_InvalidValue()
        {
            #region Mock Setup
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push("String");
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Mock<Incr> mockIncr = new Mock<Incr>();
            mockIncr.Setup(p => p.VirtualMachine).Returns(mockVm.Object);
            mockIncr.CallBase = true;
            #endregion


            try
            {
                mockIncr.Object.Run();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. The operand on the stack is of the wrong type. (at [line IncrProxy] 1 )");
                return;
            }
            Assert.Fail("An exception should have been thrown...");
        }
        [TestMethod()]
        public void IncrTest_InvalidStack()
        {
            #region Mock Setup
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Mock<Incr> mockIncr = new Mock<Incr>();
            mockIncr.Setup(p => p.VirtualMachine).Returns(mockVm.Object);
            mockIncr.CallBase = true;
            #endregion

            try
            {
                mockIncr.Object.Run();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. A stack underflow error has occurred. ( at [line IncrProxy] 1)");
                return;
            }
            Assert.Fail("An exception should have been thrown...");
        }
    }
}
//A stack underflow error has occurred. ( at [line {0}] {1})
