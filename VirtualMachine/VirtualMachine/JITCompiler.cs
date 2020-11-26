namespace SVM.VirtualMachine
{
    #region Using directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    #endregion
    /// <summary>
    /// Utility class which generates compiles a textual representation
    /// of an SML instruction into an executable instruction instance
    /// </summary>
    internal static class JITCompiler
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public methods
        #endregion

        #region Non-public methods
        private static Assembly GetAssembly()
        {
            Assembly loadedAssembly;
            Assembly[] assems = AppDomain.CurrentDomain.GetAssemblies();
            string test = assems[1].ToString();
            foreach (Assembly assem in assems)
                if (assem.ToString().StartsWith("SVM"))
                {
                    loadedAssembly = assem;
                    return loadedAssembly;
                }
            return null;
        }
        internal static IInstruction CompileInstruction(string opcode)
        {
            IInstruction instruction = null;

            Assembly SVMAssembly = GetAssembly();
            SVMAssembly.GetTypes();
            
            
            Console.ReadKey();

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            #endregion


            return instruction;
        }

        internal static IInstruction CompileInstruction(string opcode, params string[] operands)
        {
            IInstructionWithOperand instruction = null;

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            #endregion

            return instruction;
        }
        #endregion

    }
}
