namespace SVM.VirtualMachine
{
    #region Using directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
        private static List<IInstruction> instantiatedTypes = new List<IInstruction>();
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
                if (opcode.Equals(SVMtypes[i].Name.ToString(), StringComparison.InvariantCultureIgnoreCase))
                { 
                    try
                    {
                        Type.GetType(SVMtypes[i].AssemblyQualifiedName).GetInterface("IInstruction"); // TODO FIX: If this fails -> exception.
                        // Check for existing instance -> exists -> reuse.
                        foreach (IInstruction instance in instantiatedTypes)
                        {
                            if (instance.ToString().Split(" ")[0] == Type.GetType(SVMtypes[i].AssemblyQualifiedName).Name)
                            {
                                return instance;
                            }
                        }
                        // Instance not found -> add new.
                        instantiatedTypes.Add((IInstruction)Activator.CreateInstance(Type.GetType(SVMtypes[i].AssemblyQualifiedName)));
                        return instantiatedTypes.Last();
                    }
                    catch
                    {
                        // throw SVMexception.
                        Console.WriteLine("Invalid instruction interface...... exception message.....");
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
                if (opcode.Equals(SVMtypes[i].Name.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    try
                    {
                        Type.GetType(SVMtypes[i].AssemblyQualifiedName).GetInterface("IInstruction"); // If this fails -> exception.
                        #region JON PLS EXPLAIN HOW TO REPLACE THE OPERANDS OF AN INSTANCED TYPE I CANNOT.
                        // ??? BECAUSE INSTANCE STILL NEEDS TO BE USED LATER ON IN THE CODE ITS NOT POSSIBLE OR WOULD BREAK ANYWAYS ???
                        //foreach (IInstruction instance in instantiatedTypes)
                        //{
                        //    if (instance.ToString().Split(" ")[0] == "*") // Change to any basic instruction. Set to * for task 5.
                        //    {
                        //        if (instance.ToString().Split(" ")[1] == Type.GetType(SVMtypes[i].AssemblyQualifiedName).Name)
                        //        {
                        //            PropertyInfo property = Type.GetType(SVMtypes[i].AssemblyQualifiedName).GetProperty("Operands");
                        //            property.SetValue(instance, operands);
                        //            return instance;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (instance.ToString().Split(" ")[0] == Type.GetType(SVMtypes[i].AssemblyQualifiedName).Name)
                        //        {
                        //            PropertyInfo property = instance.GetType().GetProperty("Operands");
                        //           /* IMPOSSIBLE JON PLS
                        //            property.GetType().GetProperty("Item").SetValue(instance, operands, new object[] { 0 });
                        //                //.GetValue(instance, null);
                        //            //var why = test.GetType().GetProperty("Item");
                        //                //.SetValue(instance, operands, new object[] { (int) 0 });
                        //            //property.SetValue(instance, operands);
                                       
                        //            //.SetValue(x, operands, new object[] { (int)0 });
                        //            //property.SetValue();
                        //            */
                        //            return instance;
                        //        }
                        //    }
                        //    break;
                            
                        //}
                        //// Instance not found -> add new.
                        //var newInstance = (IInstruction)Activator.CreateInstance(Type.GetType(SVMtypes[i].AssemblyQualifiedName));
                        //PropertyInfo newProperty = Type.GetType(SVMtypes[i].AssemblyQualifiedName).GetProperty("Operands");
                        //newProperty.SetValue(newInstance, operands);
                        //instantiatedTypes.Add(newInstance);
                        //return instantiatedTypes.Last();
                        #endregion
                    }
                    catch
                    {
                        // throw SVMexception.
                        Console.WriteLine("Invalid instruction interface...... exception message.....");
                    }

                    type = Type.GetType(SVMtypes[i].AssemblyQualifiedName);
                    Object obj = Activator.CreateInstance(type);
                    PropertyInfo property = type.GetProperty("Operands");
                    property.SetValue(obj, operands);
                    return (IInstructionWithOperand)obj;
                }
            }
            return instruction;
            #endregion
        }
    }
}