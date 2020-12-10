namespace SVM.VirtualMachine
{
    #region Using directives
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
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
        public static void ValidateHashAssemblies()
        {
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;
            var keys = sAll.AllKeys;
            string[] svmAssemblies = Directory.GetFiles(Environment.CurrentDirectory, "*.dll");
            foreach (string path in svmAssemblies)
            {
                if (path.Contains("Debugger.dll") || path.Contains("SML Extensions.dll"))
                {
                    string[] pathSplit = path.Split("\\");
                    switch (pathSplit[pathSplit.Length - 1])
                    {
                        case "Debugger.dll":
                            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                if (HashFile(fs) != sAll.Get("Debugger.dll"))
                                {
                                    throw new SvmCompilationException("Debugger.dll could not be validated. Update config file.");
                                }
                            }
                            break;
                        case "SML Extensions.dll":
                            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                if (HashFile(fs) != sAll.Get("SML Extensions.dll"))
                                {
                                    throw new SvmCompilationException("Debugger.dll could not be validated. Update config file.");
                                }
                            }
                            break;
                    }
                }
            }
            Console.WriteLine("Assemblies are valid...");
        }

        public static string HashFile(FileStream stream)
        {
            StringBuilder sb = new StringBuilder();

            if (stream != null)
            {
                stream.Seek(0, SeekOrigin.Begin);

                MD5 md5 = MD5CryptoServiceProvider.Create();
                byte[] hash = md5.ComputeHash(stream);
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));

                stream.Seek(0, SeekOrigin.Begin);
            }

            return sb.ToString();
        }
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
            string[] svmAssemblies = Directory.GetFiles(Environment.CurrentDirectory);
            string[] runtimeAssemblies = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll");

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
                            foreach (Type type in assembly.GetTypes())
                            {
                                try
                                {
                                    /* TODO: JON: SML EXTENTSIONS DLL PRODUCING "THIS PE IS NOT A MANAGED EXECUTABLE"... VARIOUS ROUTES EXPLORED... CORFLAGS TO ASSESS 64/32bit, REBUILDING WITH OG FILES :(
                                     - IT CAN BE LOADED INTO MLC BUT METADATA SUCH AS: ISCLASS, BASETYPE, PRODUCES 'SYSTEM.BADIMAGEFORMATEXCEPTION' :((((
                                     - DLL CANNOT BE LOADED BY: ASSEMBLY.LOAD(ASSEMBLY.FULLNAME) -> CANNOT LOCATE IT??? EVEN THOUGH FULLNAME CAN BE ACCESSED AND THE PATH IS IN THE PATH RESOLVER */
                                    #region SML EXTENSIONS HARD CODE
                                    if (assembly.FullName.Contains("SML Extensions"))
                                    {
                                        Assembly loadedAssembly = Assembly.LoadFrom(path);
                                        foreach (Type loadedType in loadedAssembly.GetTypes())
                                        {
                                            if (loadedType.GetInterface("IInstruction") != null)
                                            {
                                                types.Add(loadedType);
                                            }
                                        }
                                    }
                                    #endregion
                                    if (type.IsClass && type.GetInterface("IInstruction") != null) // Condition for assessing load
                                    {
                                        // Loop loaded assembly types.
                                        Assembly loadedAssembly = Assembly.Load(assembly.FullName);
                                        foreach (Type loadedType in loadedAssembly.GetTypes())
                                        {
                                            if (loadedType.GetInterface("IInstruction") != null)
                                            {
                                                types.Add(loadedType);
                                            }
                                        }
                                        break;
                                    }
                                }
                                catch (Exception) { }
                            }
                        }
                        catch (ReflectionTypeLoadException ex)
                        {
                            foreach (Type type in ex.Types)
                            {
                                try
                                {
                                    if (type.IsClass && type.GetInterface("IInstruction") != null) // Condition for assessing load
                                    {
                                        // Loop loaded assembly types.
                                        Assembly loadedAssembly = Assembly.Load(assembly.FullName);
                                        foreach (Type loadedType in loadedAssembly.GetTypes())
                                        {
                                            if (loadedType.GetInterface("IInstruction") != null)
                                            {
                                                types.Add(loadedType);
                                            }
                                        }
                                        break;
                                    }
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                    catch (Exception) { /*bad file path */}
                }  
            }
            return types; // TODO: if null throw exception
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
                            //(IInstruction)Activator.CreateInstance(Type.GetType(types[type].AssemblyQualifiedName))
                            return instantiatedTypes.Last();
                        }
                    }
                    else
                    {
                        throw new SvmCompilationException("Type could not be loaded for instruction: " + opcode);
                    }
                }
                catch (Exception e)
                {
                    throw new SvmCompilationException("Type could not be loaded for instruction: " + opcode, e);
                }
            }
            #endregion
            if (instruction == null)
            {
                throw new SvmCompilationException("Instruction could not be loaded: " + opcode);
            }
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
            if (instruction == null)
            {
                throw new SvmCompilationException("Instruction could not be loaded: " + opcode);
            }
            return instruction; // null
        }
    }
}