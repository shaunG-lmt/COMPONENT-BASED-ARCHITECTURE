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
        private static List<IInstruction> instantiatedTypes;
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
                    try
                    {
                        Type.GetType(SVMtypes[i].AssemblyQualifiedName).GetInterface("IInstruction");
                        // If instruction instance exists...
                        instruction = (IInstruction)Activator.CreateInstance(Type.GetType(SVMtypes[i].AssemblyQualifiedName));
                    }
                    catch
                    {
                        Console.WriteLine("Invalid instruction...... exception message.....");
                    }
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
 
                    Object obj = Activator.CreateInstance(type);
                    PropertyInfo property = type.GetProperty("Operands");
                    property.SetValue(obj, operands);
                    return (IInstructionWithOperand) obj;
                }
            }
            return instruction;
            #endregion
        }
    }
}