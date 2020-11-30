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
        private static Type[] SVMtypes = GetSVMTypes();
        private static Type type;
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public methods
        #endregion

        #region Non-public methods
        private static Type[] GetSVMTypes()
        {
            Assembly loadedAssembly;
            Assembly[] assems = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assem in assems)
                if (assem.ToString().StartsWith("SVM"))
                {
                    loadedAssembly = assem;
                    SVMtypes = loadedAssembly.GetTypes();
                    return SVMtypes;
                }
            return SVMtypes;    
        }
        #endregion
        internal static IInstruction CompileInstruction(string opcode)
        {
            IInstruction instruction = null;


            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            for (int i = 0; i < SVMtypes.Length; i++)
            {
                //LoadString    
                if (opcode.Equals(SVMtypes[i].Name.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    type = Type.GetType(SVMtypes[i].AssemblyQualifiedName);
                    // CHECK IMPLEMENTS IISTRUCTION BEFORE RETURN 1 MARK
                    instruction = (IInstruction)Activator.CreateInstance(type);
                    return instruction;
                }
            }
            return instruction;
            #endregion
        }

        internal static IInstruction CompileInstruction(string opcode, params string[] operands)
        {
            IInstructionWithOperand instruction = null;

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            for (int i = 0; i < SVMtypes.Length; i++)
            {
                //LoadString    
                if (opcode.Equals(SVMtypes[i].Name.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    type = Type.GetType(SVMtypes[i].AssemblyQualifiedName);
                    //PropertyInfo parameters = type.GetProperty("Operands");
                    //parameters.SetValue(SVMtypes[i].AssemblyQualifiedName, parameters, null);


                    //instruction = (IInstructionWithOperand)Activator.CreateInstance(type);


                    Object obj = Activator.CreateInstance(type);
                    Type testtype = obj.GetType();
                    PropertyInfo property = testtype.GetProperty("Operands");
                    property.SetValue(obj, operands);
                    instruction = (IInstructionWithOperand) obj;
                    return instruction;
                }
            }
            return instruction;
            #endregion
        }
    }
}