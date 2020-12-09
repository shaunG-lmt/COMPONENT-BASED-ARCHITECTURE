using SVM.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SVM.SimpleMachineLanguage
{
    class Notequ : BaseInstructionWithOperand
    {
        #region Constants
        #endregion

        #region Fields
        private int stackValue;
        private int inputValue;
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public methods
        #endregion

        #region Non-public methods
        #endregion

        #region System.Object overrides
        /// <summary>
        /// Determines whether the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object">Object</see> to compare with the current <see cref="System.Object">Object</see>.</param>
        /// <returns><b>true</b> if the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for this type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="System.Object">Object</see>.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.
        /// </summary>
        /// <returns>A <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion
        /// <summary>
        /// Jumps to the SML instruction with a given label when the top two values (any type) on the stack are not equal.
        /// </summary>
        public override void Run()
        {
            try
            {
                string topStackValue = VirtualMachine.Stack.Pop().ToString();
                if (topStackValue.Equals(VirtualMachine.Stack.Peek().ToString()))
                {
                    VirtualMachine.Stack.Push(topStackValue);
                    return;
                }
                else
                {
                    VirtualMachine.Stack.Push(topStackValue);
                    VirtualMachine.ExecuteBranching(Operands[0]);
                }
            }
            catch
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.StackUnderflowMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }
        }
    }
}
