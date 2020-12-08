namespace SVM.VirtualMachine
{
    #region Using directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
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
        private static List<Type> SVMtypes = GetSVMTypes();
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static List<Type> GetSVMTypes()
        {
            List<Type> types = new List<Type>();

            // Get the array of runtime assemblies.
            string[] runtimeAssemblies = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll");
            string[] svmAssemblies = Directory.GetFiles(Environment.CurrentDirectory);
            // Create the list of assembly paths consisting of runtime assemblies and the inspected assembly.
            var paths = new List<string>(runtimeAssemblies);

            paths.AddRange(svmAssemblies);

            // Create PathAssemblyResolver that can resolve assemblies using the created list.
            var resolver = new PathAssemblyResolver(paths);

            var mlc = new MetadataLoadContext(resolver);

            using (mlc)
            {
                // Load assembly into MetadataLoadContext.
                foreach (string path in paths)
                {
                    try
                    {   //Inspect types through Metadata
                        Assembly assembly = mlc.LoadFromAssemblyPath(path);
                        try
                        {
                            foreach(Type type in assembly.GetTypes())
                            {
                                try
                                {   
                                    if (type.IsClass && type.GetInterface("IInstruction") != null) // Condition for assessing load
                                    {
                                        // Loop loaded assembly types.
                                        Assembly loadedAssembly = Assembly.Load(assembly.FullName);
                                        foreach(Type loadedType in loadedAssembly.GetTypes())
                                        {
                                            if(loadedType.GetInterface("IInstruction") != null)
                                            {
                                                types.Add(loadedType);
                                            }
                                        }
                                        break;
                                    }
                                }
                                catch (AmbiguousMatchException) { }
                            }
                        }
                        catch (ReflectionTypeLoadException) { }
                    }
                    catch (BadImageFormatException) { }
                    catch (FileNotFoundException) { }
                }  
            }
            return types; // if null throw exception
        }
        #endregion
        internal static IInstruction CompileInstruction(string opcode)
        {
            IInstruction instruction = null;

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            for (int i = 0; i < SVMtypes.Count; i++)
            {
                try
                {
                    if (Type.GetType(SVMtypes[i].AssemblyQualifiedName).GetInterface("IInstruction") != null)
                    {
                        if (opcode.Equals(SVMtypes[i].Name.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
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
                    }
                }
                catch (Exception e)
                {
                    throw new SvmCompilationException("Type could not be loaded for instruction: " + opcode, e);
                }
                
            }
            #endregion

            return instruction; // null
        }

        internal static IInstruction CompileInstruction(string opcode, params string[] operands)
        {
            IInstructionWithOperand instruction = null;

            #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
            for (int i = 0; i < SVMtypes.Count; i++)
            {
                try
                {
                    if (Type.GetType(SVMtypes[i].AssemblyQualifiedName).GetInterface("IInstruction") != null)
                    {
                        if (opcode.Equals(SVMtypes[i].Name.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            type = Type.GetType(SVMtypes[i].AssemblyQualifiedName);
                            Object obj = Activator.CreateInstance(type);
                            PropertyInfo property = type.GetProperty("Operands");
                            property.SetValue(obj, operands);
                            return (IInstructionWithOperand)obj;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new SvmCompilationException("Type could not be loaded for instruction: " + opcode, e);
                }
            }
            #endregion

            return instruction; // null
        }
    }
}