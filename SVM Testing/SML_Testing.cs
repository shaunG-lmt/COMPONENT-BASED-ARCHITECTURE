using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVM;
using SVM.VirtualMachine;
using SVM.SimpleMachineLanguage;
using Moq;
using System.Collections;
using System;

namespace SVM_Testing
{
    [TestClass]
    public class Svm_Instruction_Testing
    {
        Mock<IVirtualMachine> mockVm = new Mock<IVirtualMachine>();
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
        public void IncrTest_EmptyStack()
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
        
        [TestMethod]
        public void DecrTest_ValidInput()
        {
            #region Validation Values
            Stack validationStack = new Stack();
            validationStack.Push(7);
            #endregion

            #region Mock Setup
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(8);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Mock<Decr> mockIncr = new Mock<Decr>();
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
        public void DecrTest_InvalidValue()
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
        public void DecrTest_EmptyStack()
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

        [TestMethod]
        public void Equint_Valid_Input()
        {
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Equint equint = new Equint();
            equint.VirtualMachine = mockVm.Object;
            equint.Operands = new string[] { "6", "testLabel" };

            try
            {
                equint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch
            {
                Assert.Fail("Unexpected Exception...");
            }
            
        }
        
        [TestMethod]
        public void Equint_Invalid_Stack_Value()
        {
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push("string");
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Equint equint = new Equint();
            equint.VirtualMachine = mockVm.Object;
            equint.Operands = new string[] { "6", "testLabel" };

            try
            {
                equint.Run();
                Assert.Fail("An exception was expected...");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. Value at the top of the stack was not an integer. Top stack value: string");
            }
        }
        
        [TestMethod]
        public void Equint_Invalid_Input_Value()
        {
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push("5");
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Equint equint = new Equint();
            equint.VirtualMachine = mockVm.Object;
            equint.Operands = new string[] { "shouldbeint", "testLabel" };

            try
            {
                equint.Run();
                Assert.Fail("An exception was expected...");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. Equint value was not an integer. Instruction given: equint shouldbeint");
            }
        }
        [TestMethod]
        public void Equint_Empty_Stack()
        {
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Equint equint = new Equint();
            equint.VirtualMachine = mockVm.Object;
            equint.Operands = new string[] { "5", "testLabel" };

            try
            {
                equint.Run();
                Assert.Fail("An exception was expected...");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. A stack underflow error has occurred. ( at [line Equint 5 testLabel] 1)");
            }
        }
        
        [TestMethod]
        public void Notequ_Valid_Input()
        {
            /// Valid as top two stack values are not equal therefore if branch executes -> pass.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            testStack.Push(7);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Notequ notequ = new Notequ();
            notequ.VirtualMachine = mockVm.Object;
            notequ.Operands = new string[] { "testLabel" };

            try
            {
                notequ.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch
            {
                Assert.Fail("Unexpected Exception...");
            }
        }
        [TestMethod]
        public void Notequ_Invalid_Input()
        {
            /// Invalid as top two stack values are equal therefore if branch executes -> fail.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Notequ notequ = new Notequ();
            notequ.VirtualMachine = mockVm.Object;
            notequ.Operands = new string[] { "testLabel" };

            try
            {
                notequ.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(0));
            }
            catch
            {
                Assert.Fail("Branch execution should have failed...");
            }
        }
        [TestMethod]
        public void Notequ_Invalid_Stack_Amount()
        {
            /// Must fail if stack is less than 2.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Notequ notequ = new Notequ();
            notequ.VirtualMachine = mockVm.Object;
            notequ.Operands = new string[] { "testLabel" };

            try
            {
                notequ.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch(Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. A stack underflow error has occurred. ( at [line Notequ testLabel] 1)");
            }
        }

        [TestMethod]
        public void Bgrint_Valid_Pass_Input()
        {
            /// Valid as input value is bigger than stack value therefore if branch executes -> pass.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bgrint bgrint = new Bgrint();
            bgrint.VirtualMachine = mockVm.Object;
            bgrint.Operands = new string[] { "7", "testLabel" };

            try
            {
                bgrint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch
            {
                Assert.Fail("Unexpected Exception");
            }
        }
        [TestMethod]
        public void Bgrint_Valid_Fail_Input()
        {
            /// Should not execute branch as input value is lower than stack value.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bgrint bgrint = new Bgrint();
            bgrint.VirtualMachine = mockVm.Object;
            bgrint.Operands = new string[] { "5", "testLabel" };

            try
            {
                bgrint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(0));
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected Exception...");
            }
        }
        [TestMethod]
        public void Bgrint_Invalid_Input()
        {
            /// Invalid as input value is string therefore if branch executes -> fail.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bgrint bgrint = new Bgrint();
            bgrint.VirtualMachine = mockVm.Object;
            bgrint.Operands = new string[] { "shouldbeint", "testLabel" };

            try 
            { 
                bgrint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. Bgrint value was not an integer. Instruction given: bgrint shouldbeint");
            }
        }
        [TestMethod]
        public void Bgrint_Stack_Empty()
        {
            /// Should fail if there is no value on the top of the stack.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bgrint bgrint = new Bgrint();
            bgrint.VirtualMachine = mockVm.Object;
            bgrint.Operands = new string[] { "7", "testLabel" };

            try
            {
                bgrint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. A stack underflow error has occurred. ( at [line Bgrint 7 testLabel] 1)");
            }
        }
        [TestMethod]
        public void Bgrint_Invalid_Stack_Value()
        {
            /// Invalid as input value is smaller than stack value therefore if branch executes -> fail.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push("string");
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bgrint bgrint = new Bgrint();
            bgrint.VirtualMachine = mockVm.Object;
            bgrint.Operands = new string[] { "7", "testLabel" };

            try
            {
                bgrint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. Value at the top of the stack was not an integer. Top stack value: string");
            }
        }

        [TestMethod]
        public void Bltint_Valid_Pass_Input()
        {
            /// Valid as input value is lower than stack value therefore if branch executes -> pass.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bltint bltint = new Bltint();
            bltint.VirtualMachine = mockVm.Object;
            bltint.Operands = new string[] { "5", "testLabel" };

            try
            {
                bltint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch
            {
                Assert.Fail("Unexpected Exception");
            }
        }
        [TestMethod]
        public void Bltint_Valid_Fail_Input()
        {
            /// Should not execute branch as input value is higher than stack value.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bltint bltint = new Bltint();
            bltint.VirtualMachine = mockVm.Object;
            bltint.Operands = new string[] { "7", "testLabel" };

            try
            {
                bltint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(0));
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected Exception...");
            }
        }
        [TestMethod]
        public void Bltint_Invalid_Input()
        {
            /// Invalid as input value is string therefore if branch executes -> fail.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push(6);
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bltint bltint = new Bltint();
            bltint.VirtualMachine = mockVm.Object;
            bltint.Operands = new string[] { "shouldbeint", "testLabel" };

            try
            {
                bltint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. Bltint value was not an integer. Instruction given: bltint shouldbeint testLabel");
            }
        }
        [TestMethod]
        public void Bltint_Stack_Empty()
        {
            /// Should fail if there is no value on the top of the stack.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bltint bltint = new Bltint();
            bltint.VirtualMachine = mockVm.Object;
            bltint.Operands = new string[] { "7", "testLabel" };

            try
            {
                bltint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. A stack underflow error has occurred. ( at [line Bltint 7 testLabel] 1)");
            }
        }
        [TestMethod]
        public void Bltint_Invalid_Stack_Value()
        {
            /// Invalid as input value is smaller than stack value therefore if branch executes -> fail.
            mockVm.Setup(p => p.ProgramCounter).Returns(1);
            Stack testStack = new Stack();
            testStack.Push("string");
            mockVm.Setup(p => p.Stack).Returns(testStack);

            Bltint bltint = new Bltint();
            bltint.VirtualMachine = mockVm.Object;
            bltint.Operands = new string[] { "7", "testLabel" };

            try
            {
                bltint.Run();
                mockVm.Verify(m => m.ExecuteBranching("testLabel"), Times.Exactly(1));
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "An error has occurred in executing the SML program. Value at the top of the stack was not an integer. Top stack value: string");
            }
        }
    }
}